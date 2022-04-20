using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace Extensions.DependencyInjection.Generators.Abstractions
{
    public interface IGeneratorHandler<TSource>
    {
        bool SyntaxFilter(SyntaxNode node, CancellationToken cancellationToken);
        TSource SourceProvider(GeneratorSyntaxContext context, CancellationToken cancellationToken);
        void RegisterSourceOutput(SourceProductionContext context, (Compilation Left, ImmutableArray<TSource> Right) source);
    }
}
