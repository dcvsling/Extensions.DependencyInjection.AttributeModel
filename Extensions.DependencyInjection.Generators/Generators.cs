using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Immutable;

namespace Extensions.DependencyInjection.Generators;

internal static class Generators
{
    private const string _EMPTY = "";
    public static bool HasLifetimeAttrbute(SyntaxNode syntax, CancellationToken _ = default)
        => syntax is ClassDeclarationSyntax cs
            && cs.AttributeLists.SelectMany(x => x.Attributes, (_, attr) => attr.Name.ToFullString())
                .Any(name => name.IsInjectAttribute());

    public static ClassDeclarationSyntax FindAttributeSyntaxs(GeneratorSyntaxContext context, CancellationToken _ = default)
        =>  (ClassDeclarationSyntax)context.Node;

    public static void GenerateOutput(SourceProductionContext context, (Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classSyntaxes) sources)
    {
        var registers = new List<string>();
        var usings = new List<string>();
        sources.classSyntaxes
            .Select(syntax => sources.compilation.GetSymbol(syntax))
            .Where(symbol => symbol is not null)
            .SelectMany(symbol => symbol.Emit())
            .ToList()
            .ForEach(content =>
            {
                registers.Add(content.RegisterContent);
                usings.Add(content.UsingContent);
            });
        context.AddSource("ServiceRegistry.cs", GeneratedCodes.CreateRegistryAttribute(sources.compilation.GlobalNamespace.Name, usings, registers));
    }
    public static IEnumerable<DependencyContent> Emit(this INamedTypeSymbol @class)
        => @class.GetAttributes()
            .Where(x => x.AttributeClass?.Name.IsInjectAttribute() ?? false)
            .Select(attr => new InjectInfo(attr, @class))
            .Select(info => new DependencyContent(info.Namespace, info switch
            {
                { ServiceType: null or _EMPTY } => info switch
                {
                    { IsGenericType: true } => GeneratedCodes.CreateSelfOnlyArgumentDependencyRegistry(info.Lifetime, info.ImplementationType),
                    { IsGenericType: false, MemberName: null or _EMPTY } => GeneratedCodes.CreateSelfWithGetterDependencyRegistry(info.Lifetime, $"{info.ImplementationType}.{info.MemberName}"),
                    _ => GeneratedCodes.CreateSelfOnlyGenericDependencyRegistry(info.Lifetime, info.ImplementationType)
                },
                { IsGenericType: true } => GeneratedCodes.CreateAllArgumentTypeDependencyRegistry(info.Lifetime, info.ServiceType, info.ImplementationType),
                { MemberName: null or _EMPTY } => GeneratedCodes.CreateAllGenericDependencyRegistry(info.Lifetime, info.ServiceType, info.ImplementationType),
                _ => GeneratedCodes.CreateGetterDependencyRegistry(info.Lifetime, info.ServiceType, $"{info.ImplementationType}.{info.MemberName}")
            }));
}
