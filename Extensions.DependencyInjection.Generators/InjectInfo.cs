using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Extensions.DependencyInjection.Generators;

public record InjectInfo(AttributeData AttributeData, INamedTypeSymbol ClassSymbol)
{
    public string Lifetime => AttributeData.NamedArguments.First(x => x.Key == nameof(Lifetime)).Value.ToCSharpString();
    public string? ServiceType => AttributeData.NamedArguments.FirstOrDefault(x => x.Key == nameof(ServiceType)).Value.ToCSharpString();
    public string? MemberName => AttributeData.NamedArguments.FirstOrDefault(x => x.Key == nameof(MemberName)).Value.ToCSharpString();
    public string ImplementationType => ClassSymbol.IsGenericType ? ClassSymbol.ConstructUnboundGenericType().Name : ClassSymbol.Name;
    public string Namespace => ClassSymbol.ContainingNamespace.Name;
    public bool IsGenericType => ClassSymbol.IsGenericType;
}