using System.Collections.Immutable;

using Extensions.DependencyInjection.Generators.Abstractions;

using Microsoft.CodeAnalysis;

namespace Extensions.DependencyInjection.Generators
{
    public static class GeneratorExtensions
    {
        internal static void Initialize<T>(this IncrementalGeneratorInitializationContext context, IGeneratorHandler<T> handler)
            => context.RegisterSourceOutput(
                context.CompilationProvider.Combine(
                    context.SyntaxProvider.CreateSyntaxProvider(handler.SyntaxFilter, handler.ProjectSource)
                        .Collect()),
                handler.RegisterSourceOutput);
    }
}



