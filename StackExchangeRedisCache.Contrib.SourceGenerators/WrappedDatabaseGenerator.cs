using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace StackExchangeRedisCache.Contrib.SourceGenerators;

[Generator]
public sealed class WrappedDatabaseGenerator : IIncrementalGenerator
{
    private const string TemplateFileName = "WrappedDatabaseDummy.cs";
    private const string TemplateClassName = "WrappedDatabaseDummy";
    private const string TargetClassName = "WrappedDatabase";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<ClassDeclarationSyntax> targetDeclarations = context.SyntaxProvider.CreateSyntaxProvider(
            static (node, _) => node is ClassDeclarationSyntax cds && cds.Identifier.Text == TargetClassName && cds.Modifiers.Any(SyntaxKind.PartialKeyword),
            static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
        );

        IncrementalValueProvider<bool> hasTarget = targetDeclarations.Collect().Select(static (list, _) => !list.IsDefault && list.Length > 0);

        IncrementalValuesProvider<ClassDeclarationSyntax> templateDeclarations = context.SyntaxProvider.CreateSyntaxProvider(
            static (node, _) => node is ClassDeclarationSyntax cds && cds.Identifier.Text == TemplateClassName && cds.Modifiers.Any(SyntaxKind.PartialKeyword),
            static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
        );

        IncrementalValueProvider<string?> templateSource = templateDeclarations
            .Collect()
            .Select(
                static (list, ct) =>
                {
                    if (list.IsDefaultOrEmpty)
                    {
                        return null;
                    }

                    return list[0].SyntaxTree.GetText(ct).ToString();
                }
            );

        IncrementalValueProvider<(bool HasTarget, string? Template)> targetAndTemplate = hasTarget.Combine(templateSource).Select(static (tuple, _) => tuple);

        context.RegisterSourceOutput(targetAndTemplate, static (spc, tuple) =>
        {
            (bool hasTarget, string? template) = tuple;
            if (!hasTarget)
            {
                return;
            }

            if (template is null)
            {
                spc.ReportDiagnostic(
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            id: "SRCGEN002",
                            title: "WrappedDatabase template missing",
                            messageFormat: "Ensure compile source defines partial class '" + TemplateClassName + "' (e.g. 'Internal\\Dummies\\" + TemplateFileName + "').",
                            category: "StackExchangeRedisCache.Contrib.SourceGenerators",
                            defaultSeverity: DiagnosticSeverity.Error,
                            isEnabledByDefault: true
                        ),
                        Location.None
                    )
                );
                return;
            }

            ClassDeclarationSyntax? stubClass = FindStubClass(template);
            if (stubClass is null)
            {
                spc.ReportDiagnostic(
                    Diagnostic.Create(
                        new DiagnosticDescriptor(
                            id: "SRCGEN003",
                            title: "Template class not found",
                            messageFormat: "Could not find class '" + TemplateClassName + "' in compile source '" + TemplateFileName + "'.",
                            category: "StackExchangeRedisCache.Contrib.SourceGenerators",
                            defaultSeverity: DiagnosticSeverity.Error,
                            isEnabledByDefault: true
                        ),
                        Location.None
                    )
                );
                return;
            }

            IReadOnlyDictionary<string, bool> manifest = MethodManifestLoader.GetOpByMethodName;
            string generated = RenderPartial(stubClass, manifest);
            spc.AddSource($"{TargetClassName}.g.cs", SourceText.From(generated, Encoding.UTF8));
        });
    }

    private static ClassDeclarationSyntax? FindStubClass(string source)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Latest), path: TemplateFileName, encoding: Encoding.UTF8);

        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
        foreach (MemberDeclarationSyntax member in root.Members)
        {
            if (member is BaseNamespaceDeclarationSyntax ns)
            {
                ClassDeclarationSyntax? found = FindInNamespaceMembers(ns.Members);
                if (found is not null)
                {
                    return found;
                }
            }
        }

        return null;
    }

    private static ClassDeclarationSyntax? FindInNamespaceMembers(SyntaxList<MemberDeclarationSyntax> members)
    {
        foreach (MemberDeclarationSyntax m in members)
        {
            switch (m)
            {
                case ClassDeclarationSyntax cds when cds.Identifier.Text == TemplateClassName:
                    return cds;
                case BaseNamespaceDeclarationSyntax nested:
                    ClassDeclarationSyntax? inner = FindInNamespaceMembers(nested.Members);
                    if (inner is not null)
                    {
                        return inner;
                    }

                    break;
            }
        }

        return null;
    }

    private static string RenderPartial(ClassDeclarationSyntax stubClass, IReadOnlyDictionary<string, bool> manifest)
    {
        var sb = new StringBuilder(capacity: 320_000);
        sb.AppendLine("// <auto-generated />");
        sb.AppendLine("#pragma warning disable SER002, SER003, SER006 // StackExchange.Redis experimental surface");
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine("using System.Diagnostics.CodeAnalysis;");
        sb.AppendLine("using System.Net;");
        sb.AppendLine("using StackExchange.Redis;");
        sb.AppendLine("using StackExchangeRedisCache.Contrib;");
        sb.AppendLine();
        sb.AppendLine("namespace StackExchangeRedisCache.Contrib.Internal;");
        sb.AppendLine();
        sb.AppendLine($"internal sealed partial class {TargetClassName}");
        sb.AppendLine("{");

        foreach (MemberDeclarationSyntax member in stubClass.Members)
        {
            switch (member)
            {
                case PropertyDeclarationSyntax prop when IsThrowNotImplementedProperty(prop):
                    RenderProperty(sb, prop);
                    break;
                case MethodDeclarationSyntax method when IsStubMethod(method) && !IsManuallyImplementedDatabaseMethod(method):
                    RenderMethod(sb, method, manifest);
                    break;
            }
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    private static bool IsThrowNotImplementedProperty(PropertyDeclarationSyntax prop)
    {
        if (prop.ExpressionBody is null)
        {
            return false;
        }

        return IsThrowNotImplemented(prop.ExpressionBody.Expression);
    }

    private static bool IsThrowNotImplemented(ExpressionSyntax expr) =>
        expr switch
        {
            ThrowExpressionSyntax tes => IsThrowNotImplementedCreation(tes.Expression),
            _ => false,
        };

    private static bool IsThrowNotImplementedCreation(ExpressionSyntax expr) => expr is ObjectCreationExpressionSyntax { Type: IdentifierNameSyntax { Identifier.Text: "NotImplementedException" } };

    private static bool IsStubMethod(MethodDeclarationSyntax method)
    {
        if (method.Body is null || method.Body.Statements.Count != 1)
        {
            return false;
        }

        return method.Body.Statements[0] is ThrowStatementSyntax th && th.Expression is ObjectCreationExpressionSyntax { Type: IdentifierNameSyntax { Identifier.Text: "NotImplementedException" } };
    }

    /// <summary>Methods implemented in the hand-written <c>WrappedDatabase</c> partial instead of source output.</summary>
    private static bool IsManuallyImplementedDatabaseMethod(MethodDeclarationSyntax method) => method.Identifier.ValueText is "CreateBatch";

    private static void RenderProperty(StringBuilder sb, PropertyDeclarationSyntax prop)
    {
        string name = prop.Identifier.ValueText;
        sb.Append("    public ");
        sb.Append(prop.Type.ToString());
        sb.Append(' ');
        sb.Append(name);
        sb.Append(" => db.");
        sb.Append(name);
        sb.AppendLine(";");
    }

    private static void RenderMethod(StringBuilder sb, MethodDeclarationSyntax method, IReadOnlyDictionary<string, bool> manifest)
    {
        string name = method.Identifier.ValueText;
        if (manifest.TryGetValue(name, out bool getOp))
        {
            if (getOp)
            {
                sb.AppendLine("    [TweakableGetAttribute]");
            }
            else
            {
                sb.AppendLine("    [TweakableSetAttribute]");
            }

            sb.Append("    public ");
            sb.Append(method.ReturnType.ToString());
            sb.Append(' ');
            sb.Append(name);
            if (method.TypeParameterList is not null)
            {
                sb.Append(method.TypeParameterList.ToString());
            }

            sb.Append(method.ParameterList.ToString());
            if (method.ConstraintClauses.Count > 0)
            {
                foreach (TypeParameterConstraintClauseSyntax c in method.ConstraintClauses)
                {
                    sb.Append(' ');
                    sb.Append(c.ToString());
                }
            }

            sb.AppendLine();
            sb.AppendLine("    {");
            AppendTweakerAssignments(sb, method, getOp);
            AppendDbInvocationStatement(sb, method, name);
            sb.AppendLine("    }");
            return;
        }

        sb.Append("    public ");
        sb.Append(method.ReturnType.ToString());
        sb.Append(' ');
        sb.Append(name);
        if (method.TypeParameterList is not null)
        {
            sb.Append(method.TypeParameterList.ToString());
        }

        sb.Append(method.ParameterList.ToString());
        sb.Append(" => db.");
        sb.Append(name);
        if (method.TypeParameterList is not null && method.TypeParameterList.Parameters.Count > 0)
        {
            sb.Append('<');
            for (int i = 0; i < method.TypeParameterList.Parameters.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(method.TypeParameterList.Parameters[i].Identifier.ValueText);
            }

            sb.Append('>');
        }

        sb.Append('(');
        SeparatedSyntaxList<ParameterSyntax> ps = method.ParameterList.Parameters;
        for (int i = 0; i < ps.Count; i++)
        {
            if (i > 0)
            {
                sb.Append(", ");
            }

            sb.Append(PassParameter(ps[i]));
        }

        sb.AppendLine(");");
    }

    private static void AppendTweakerAssignments(StringBuilder sb, MethodDeclarationSyntax method, bool getOp)
    {
        string keyExpr = BuildRedisKeyArgumentExpression(method.ParameterList.Parameters);
        string tweakMethod = getOp ? "TweakGetType" : "TweakSetType";
        foreach (ParameterSyntax p in method.ParameterList.Parameters)
        {
            if (p.Modifiers.Any(SyntaxKind.OutKeyword))
            {
                continue;
            }

            if (!IsCommandFlagsParameter(p))
            {
                continue;
            }

            string pname = p.Identifier.ValueText;
            sb.Append("        ");
            sb.Append(pname);
            sb.Append(" = tweaker.");
            sb.Append(tweakMethod);
            sb.Append('(');
            sb.Append(pname);
            sb.Append(", ");
            sb.Append(keyExpr);
            sb.AppendLine(");");
        }
    }

    private static void AppendDbInvocationStatement(StringBuilder sb, MethodDeclarationSyntax method, string name)
    {
        bool voidReturn = method.ReturnType is PredefinedTypeSyntax pts && pts.Keyword.IsKind(SyntaxKind.VoidKeyword);
        if (!voidReturn)
        {
            sb.Append("        return ");
        }
        else
        {
            sb.Append("        ");
        }

        sb.Append("db.");
        sb.Append(name);
        if (method.TypeParameterList is not null && method.TypeParameterList.Parameters.Count > 0)
        {
            sb.Append('<');
            for (int i = 0; i < method.TypeParameterList.Parameters.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(method.TypeParameterList.Parameters[i].Identifier.ValueText);
            }

            sb.Append('>');
        }

        sb.Append('(');
        SeparatedSyntaxList<ParameterSyntax> ps = method.ParameterList.Parameters;
        for (int i = 0; i < ps.Count; i++)
        {
            if (i > 0)
            {
                sb.Append(", ");
            }

            sb.Append(PassParameter(ps[i]));
        }

        sb.AppendLine(");");
    }

    private static string BuildRedisKeyArgumentExpression(SeparatedSyntaxList<ParameterSyntax> parameters)
    {
        foreach (ParameterSyntax p in parameters)
        {
            TypeSyntax t = UnwrapNullable(p.Type!);
            if (t is ArrayTypeSyntax arr && TypeIsUnqualifiedOrGlobalName(arr.ElementType, "RedisKey"))
            {
                string n = p.Identifier.ValueText;
                return $"({n}.Length != 0 ? {n}[0] : default(RedisKey))";
            }

            if (TypeIsUnqualifiedOrGlobalName(t, "RedisKey"))
            {
                return p.Identifier.ValueText;
            }
        }

        return "default(RedisKey)";
    }

    private static bool IsCommandFlagsParameter(ParameterSyntax p)
    {
        TypeSyntax? t = p.Type;
        return t is not null && TypeIsUnqualifiedOrGlobalName(UnwrapNullable(t), "CommandFlags");
    }

    private static TypeSyntax UnwrapNullable(TypeSyntax type) => type is NullableTypeSyntax nt ? nt.ElementType : type;

    private static bool TypeIsUnqualifiedOrGlobalName(TypeSyntax type, string simpleName)
    {
        string? name = GetSimpleTypeName(type);
        return name == simpleName;
    }

    private static string? GetSimpleTypeName(TypeSyntax type)
    {
        switch (type)
        {
            case IdentifierNameSyntax id:
                return id.Identifier.Text;
            case QualifiedNameSyntax qn when qn.Right is IdentifierNameSyntax right:
                return right.Identifier.Text;
            case AliasQualifiedNameSyntax aq when aq.Name is IdentifierNameSyntax aliasRight:
                return aliasRight.Identifier.Text;
            default:
                return null;
        }
    }

    private static string PassParameter(ParameterSyntax p)
    {
        if (p.Modifiers.Any(SyntaxKind.OutKeyword))
        {
            return "out " + p.Identifier.ValueText;
        }

        if (p.Modifiers.Any(SyntaxKind.RefKeyword))
        {
            return "ref " + p.Identifier.ValueText;
        }

        if (p.Modifiers.Any(SyntaxKind.InKeyword))
        {
            return "in " + p.Identifier.ValueText;
        }

        return p.Identifier.ValueText;
    }
}
