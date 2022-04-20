﻿using System.Collections.Immutable;

using Extensions.DependencyInjection.Generators.Abstractions;

using Microsoft.CodeAnalysis;

namespace Extensions.DependencyInjection.Generators
{
    public static class GeneratorExtensions
    {
        internal static void Initialize<TSource>(this IncrementalGeneratorInitializationContext context, IGeneratorHandler<TSource> handler)
            => context.RegisterSourceOutput(
                context.CompilationProvider.Combine(
                    context.SyntaxProvider.CreateSyntaxProvider(handler.SyntaxFilter, handler.SourceProvider)
                        .Collect()),
                handler.RegisterSourceOutput);
    }
}



