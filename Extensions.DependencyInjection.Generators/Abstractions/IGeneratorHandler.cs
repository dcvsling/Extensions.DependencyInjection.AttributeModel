using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Threading;

namespace Extensions.DependencyInjection.Generators.Abstractions
{
    public interface IGeneratorHandler<T>
    {
        bool SyntaxFilter(SyntaxNode node, CancellationToken cancellationToken);
        AttributeMetadata ProjectSource(GeneratorSyntaxContext context, CancellationToken cancellationToken);
        void RegisterSourceOutput(SourceProductionContext context, (Compilation Left, ImmutableArray<AttributeMetadata> Right) source);
    }
}
