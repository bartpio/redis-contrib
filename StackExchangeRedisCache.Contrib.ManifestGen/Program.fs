open System
open System.IO
open System.Text.Json
open Mono.Cecil
open Mono.Cecil.Cil
open Microsoft.Extensions.Caching.StackExchangeRedis
open StackExchange.Redis

[<Struct; StructuralComparison; StructuralEquality>]
type MethodSig = { Name: string; GetOp: bool }

module CecilTypes =
    let rec flatten (types: seq<TypeDefinition>) =
        seq {
            for t in types do
                yield t
                yield! flatten t.NestedTypes
        }

let collectInterfaceTree (roots: Type array) =
    let rec loop (seen: Set<string>) (work: Type list) =
        match work with
        | [] -> seen
        | t :: rest ->
            match t.FullName with
            | null -> loop seen rest
            | name when seen.Contains name -> loop seen rest
            | name ->
                loop (seen.Add name) (List.ofSeq (t.GetInterfaces()) @ rest)
    loop Set.empty (List.ofArray roots)

[<EntryPoint>]
let main _ =
    try
        let assemblyPath = typeof<RedisCache>.Assembly.Location
        let allowed =
            collectInterfaceTree [| typeof<IDatabase>; typeof<IBatch> |]

        eprintfn $"Scanning: {assemblyPath}"
        eprintfn "Allowed interface roots:"
        allowed |> Seq.sort |> Seq.iter (eprintfn "  %s")

        let resolver = new DefaultAssemblyResolver()
        match Path.GetDirectoryName assemblyPath with
        | null | "" -> ()
        | dir -> resolver.AddSearchDirectory dir

        use assembly =
            AssemblyDefinition.ReadAssembly(
                assemblyPath,
                ReaderParameters(
                    AssemblyResolver = resolver,
                    ReadSymbols = false
                )
            )

        let matches =
            assembly.Modules
            |> Seq.collect (fun (m: ModuleDefinition) -> CecilTypes.flatten m.Types)
            |> Seq.collect (fun (t: TypeDefinition) -> t.Methods)
            |> Seq.filter (fun (meth: MethodDefinition) -> meth.HasBody)
            |> Seq.collect (fun (meth: MethodDefinition) -> meth.Body.Instructions)
            |> Seq.choose (fun (instr: Instruction) ->
                match instr.OpCode.Code, instr.Operand with
                | (Code.Call | Code.Callvirt), (:? MethodReference as m) ->
                    let decl = m.DeclaringType.FullName
                    if isNull decl || not (allowed.Contains decl) then
                        None
                    else
                        match Seq.toList m.Parameters with
                        | p :: _ when p.ParameterType.Name = nameof RedisKey ->
                            Some
                                { Name = m.Name
                                  GetOp = m.Name.Contains("Get", StringComparison.Ordinal) }
                        | _ -> None
                | _ -> None)
            |> Set.ofSeq

        let json =
            JsonSerializer.Serialize(matches, JsonSerializerOptions(WriteIndented = true))

        printfn "%s" json
        0
    with ex ->
        eprintfn "%O" ex
        1