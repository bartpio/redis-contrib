using System.Reflection;
using System.Text;
using StackExchange.Redis;

static string TypeName(Type t)
{
    if (t.IsByRef)
        return TypeName(t.GetElementType()!);
    if (t.IsGenericParameter)
        return t.Name;
    if (t == typeof(void))
        return "void";
    if (t == typeof(bool))
        return "bool";
    if (t == typeof(byte))
        return "byte";
    if (t == typeof(sbyte))
        return "sbyte";
    if (t == typeof(short))
        return "short";
    if (t == typeof(ushort))
        return "ushort";
    if (t == typeof(int))
        return "int";
    if (t == typeof(uint))
        return "uint";
    if (t == typeof(long))
        return "long";
    if (t == typeof(ulong))
        return "ulong";
    if (t == typeof(float))
        return "float";
    if (t == typeof(double))
        return "double";
    if (t == typeof(decimal))
        return "decimal";
    if (t == typeof(string))
        return "string";
    if (t == typeof(object))
        return "object";
    if (t.IsArray)
        return TypeName(t.GetElementType()!) + "[]";
    if (t.IsGenericType)
    {
        var gen = t.GetGenericTypeDefinition();
        if (gen == typeof(Nullable<>))
            return TypeName(t.GetGenericArguments()[0]) + "?";
        if (gen == typeof(Task<>))
            return "Task<" + TypeName(t.GetGenericArguments()[0]) + ">";
        if (gen == typeof(ValueTask<>))
            return "ValueTask<" + TypeName(t.GetGenericArguments()[0]) + ">";
        if (gen == typeof(IEnumerable<>))
            return "IEnumerable<" + TypeName(t.GetGenericArguments()[0]) + ">";
        if (gen == typeof(IAsyncEnumerable<>))
            return "IAsyncEnumerable<" + TypeName(t.GetGenericArguments()[0]) + ">";
        if (gen == typeof(KeyValuePair<,>))
            return "KeyValuePair<" + TypeName(t.GetGenericArguments()[0]) + ", " + TypeName(t.GetGenericArguments()[1]) + ">";
        if (gen == typeof(Lease<>))
            return "Lease<" + TypeName(t.GetGenericArguments()[0]) + ">";
        var name = t.Name;
        var tick = name.IndexOf('`');
        if (tick > 0)
            name = name[..tick];
        return name + "<" + string.Join(", ", t.GetGenericArguments().Select(TypeName)) + ">";
    }
    if (t.IsNested)
        return t.DeclaringType!.Name + "." + t.Name;
    return t.Name;
}

static string FormatDefault(ParameterInfo p)
{
    if (!p.HasDefaultValue)
        return "";
    var v = p.DefaultValue;
    var t = p.ParameterType;
    if (t.IsByRef)
        t = t.GetElementType()!;
    var underlying = Nullable.GetUnderlyingType(t);
    if (underlying is not null)
    {
        if (v is null)
            return " = null";
        t = underlying;
    }
    // Optional struct parameters often surface as a null DefaultValue in reflection.
    if (v is null || v is DBNull)
    {
        if (t.IsValueType && Nullable.GetUnderlyingType(t) is null)
            return " = default";
        return " = null";
    }
    if (t == typeof(bool))
        return (bool)v ? " = true" : " = false";
    if (t == typeof(string))
        return $" = \"{v}\"";
    if (t.IsEnum)
        return $" = {t.Name}.{Enum.GetName(t, v)}";
    if (t == typeof(int) || t == typeof(long))
        return $" = {v}";
    if (t == typeof(double))
    {
        var d = (double)v;
        if (double.IsPositiveInfinity(d))
            return " = double.PositiveInfinity";
        if (double.IsNegativeInfinity(d))
            return " = double.NegativeInfinity";
        if (double.IsNaN(d))
            return " = double.NaN";
        return $" = {d}";
    }
    if (t == typeof(float))
    {
        var f = (float)v;
        if (float.IsPositiveInfinity(f))
            return " = float.PositiveInfinity";
        if (float.IsNegativeInfinity(f))
            return " = float.NegativeInfinity";
        if (float.IsNaN(f))
            return " = float.NaN";
        return $" = {f}";
    }
    if (t.IsValueType && v.Equals(Activator.CreateInstance(t)))
        return " = default";
    return $" = {v}";
}

static string Param(ParameterInfo p)
{
    var t = p.ParameterType;
    var prefix = "";
    if (t.IsByRef)
    {
        if (p.IsIn)
            prefix = "in ";
        else if (p.IsOut)
            prefix = "out ";
        else
            prefix = "ref ";
        t = t.GetElementType()!;
    }
    else if (p.GetCustomAttribute<ParamArrayAttribute>() is not null)
    {
        prefix = "params ";
    }

    var typeStr = TypeName(t);
    try
    {
        var ctx = new NullabilityInfoContext();
        var n = ctx.Create(p);
        if (n.WriteState == NullabilityState.Nullable || n.ReadState == NullabilityState.Nullable)
        {
            if (!typeStr.EndsWith('?'))
                typeStr += "?";
        }
    }
    catch
    {
        // ignore
    }

    if (p.HasDefaultValue && p.DefaultValue is null && !typeStr.EndsWith('?') && !t.IsValueType)
        typeStr += "?";

    // Do not annotate non-nullable value types as T? just because the default is default(T).
    return $"{prefix}{typeStr} {p.Name}{FormatDefault(p)}";
}

static string FormatType(Type t, NullabilityInfo? nullability)
{
    if (t.IsGenericParameter)
        return t.Name;

    if (t.IsArray)
    {
        var elem = FormatType(t.GetElementType()!, nullability?.ElementType);
        var arr = elem + "[]";
        if (nullability?.ReadState == NullabilityState.Nullable)
            arr += "?";
        return arr;
    }

    if (t.IsGenericType)
    {
        var gen = t.GetGenericTypeDefinition();
        if (gen == typeof(Nullable<>))
            return TypeName(t.GetGenericArguments()[0]) + "?";

        var args = t.GetGenericArguments();
        var nArgs = nullability?.GenericTypeArguments;
        string[] formatted = new string[args.Length];
        for (var i = 0; i < args.Length; i++)
            formatted[i] = FormatType(args[i], nArgs is { Length: > 0 } && i < nArgs.Length ? nArgs[i] : null);

        if (gen == typeof(Task<>))
            return "Task<" + formatted[0] + ">";
        if (gen == typeof(ValueTask<>))
            return "ValueTask<" + formatted[0] + ">";
        if (gen == typeof(IEnumerable<>))
            return "IEnumerable<" + formatted[0] + ">";
        if (gen == typeof(IAsyncEnumerable<>))
            return "IAsyncEnumerable<" + formatted[0] + ">";
        if (gen == typeof(KeyValuePair<,>))
            return "KeyValuePair<" + formatted[0] + ", " + formatted[1] + ">";
        if (gen == typeof(Lease<>))
        {
            var lease = "Lease<" + formatted[0] + ">";
            if (nullability?.ReadState == NullabilityState.Nullable)
                lease += "?";
            return lease;
        }

        var name = t.Name;
        var tick = name.IndexOf('`');
        if (tick > 0)
            name = name[..tick];
        var rendered = name + "<" + string.Join(", ", formatted) + ">";
        if (nullability?.ReadState == NullabilityState.Nullable)
            rendered += "?";
        return rendered;
    }

    var simple = TypeName(t);
    if (nullability?.ReadState == NullabilityState.Nullable && !simple.EndsWith('?') && simple != "void")
        simple += "?";
    return simple;
}

static string ReturnType(MethodInfo m)
{
    try
    {
        var ctx = new NullabilityInfoContext();
        return FormatType(m.ReturnType, ctx.Create(m.ReturnParameter));
    }
    catch
    {
        return TypeName(m.ReturnType);
    }
}

static bool IsPropertyMethod(MethodInfo m) => m.IsSpecialName && (m.Name.StartsWith("get_") || m.Name.StartsWith("set_") || m.Name.StartsWith("add_") || m.Name.StartsWith("remove_"));

static IEnumerable<Type> InterfaceTree(Type root)
{
    var seen = new HashSet<string>();
    var stack = new Stack<Type>();
    stack.Push(root);
    while (stack.Count > 0)
    {
        var t = stack.Pop();
        if (!seen.Add(t.FullName!))
            continue;
        yield return t;
        foreach (var i in t.GetInterfaces())
            stack.Push(i);
    }
}

static IEnumerable<PropertyInfo> AllProperties(Type root)
{
    var seen = new HashSet<string>();
    foreach (var t in InterfaceTree(root))
    {
        foreach (var p in t.GetProperties())
        {
            if (!seen.Add(p.Name + "|" + TypeName(p.PropertyType)))
                continue;
            yield return p;
        }
    }
}

static IEnumerable<MethodInfo> AllMethods(Type root)
{
    var seen = new HashSet<string>();
    foreach (var t in InterfaceTree(root))
    {
        foreach (var m in t.GetMethods())
        {
            if (IsPropertyMethod(m))
                continue;
            if (!seen.Add(m.ToString()!))
                continue;
            yield return m;
        }
    }
}

static void WriteDummy(Type iface, string className, string path)
{
    var sb = new StringBuilder(capacity: 400_000);
    sb.AppendLine("using System.Net;");
    sb.AppendLine("using StackExchange.Redis;");
    sb.AppendLine();
    sb.AppendLine("#pragma warning disable SER003, SER006 // Sourcegen template mirrors Redis experimental API surface.");
    sb.AppendLine();
    sb.AppendLine("namespace StackExchangeRedisCache.Contrib.Internal.Dummies;");
    sb.AppendLine();
    sb.AppendLine($"internal abstract partial class {className} : {iface.Name}");
    sb.AppendLine("{");

    foreach (var prop in AllProperties(iface).OrderBy(p => p.Name))
    {
        var type = TypeName(prop.PropertyType);
        try
        {
            var ctx = new NullabilityInfoContext();
            var n = ctx.Create(prop);
            if (n.ReadState == NullabilityState.Nullable && !type.EndsWith('?'))
                type += "?";
        }
        catch { }

        if (prop.GetMethod is not null && prop.SetMethod is null)
        {
            sb.AppendLine($"    public {type} {prop.Name} => throw new NotImplementedException();");
            sb.AppendLine();
        }
        else if (prop.GetMethod is not null && prop.SetMethod is not null)
        {
            sb.AppendLine($"    public {type} {prop.Name}");
            sb.AppendLine("    {");
            sb.AppendLine("        get => throw new NotImplementedException();");
            sb.AppendLine("        set => throw new NotImplementedException();");
            sb.AppendLine("    }");
            sb.AppendLine();
        }
    }

    foreach (var m in AllMethods(iface).OrderBy(m => m.Name).ThenBy(m => m.GetParameters().Length).ThenBy(m => m.ToString()))
    {
        var ret = ReturnType(m);
        var parms = string.Join(", ", m.GetParameters().Select(Param));
        var generics = "";
        var constraints = "";
        if (m.IsGenericMethodDefinition)
        {
            var tps = m.GetGenericArguments();
            generics = "<" + string.Join(", ", tps.Select(t => t.Name)) + ">";
            // Constraints are uncommon on IDatabase; skip unless present
            foreach (var tp in tps)
            {
                var parts = new List<string>();
                if (tp.GenericParameterAttributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint))
                    parts.Add("class");
                if (tp.GenericParameterAttributes.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint))
                    parts.Add("struct");
                if (tp.GenericParameterAttributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
                    parts.Add("new()");
                parts.AddRange(tp.GetGenericParameterConstraints().Select(TypeName));
                if (parts.Count > 0)
                    constraints += $" where {tp.Name} : {string.Join(", ", parts)}";
            }
        }

        sb.AppendLine($"    public {ret} {m.Name}{generics}({parms}){constraints}");
        sb.AppendLine("    {");
        sb.AppendLine("        throw new NotImplementedException();");
        sb.AppendLine("    }");
        sb.AppendLine();
    }

    sb.AppendLine("}");
    File.WriteAllText(path, sb.ToString());
    Console.WriteLine($"Wrote {path} ({AllMethods(iface).Count()} methods)");
}

static string FindRepoRoot()
{
    var dir = new DirectoryInfo(AppContext.BaseDirectory);
    while (dir is not null)
    {
        if (File.Exists(Path.Combine(dir.FullName, "StackExchangeRedisCache.Contrib.slnx")))
            return dir.FullName;
        dir = dir.Parent;
    }
    throw new InvalidOperationException("Could not locate repo root containing StackExchangeRedisCache.Contrib.slnx.");
}

var repoRoot = FindRepoRoot();
var dummiesDir = Path.Combine(repoRoot, "StackExchangeRedisCache.Contrib", "Internal", "Dummies");
WriteDummy(typeof(IDatabase), "WrappedDatabaseDummy", Path.Combine(dummiesDir, "WrappedDatabaseDummy.cs"));
WriteDummy(typeof(IBatch), "WrappedBatchDummy", Path.Combine(dummiesDir, "WrappedBatchDummy.cs"));
