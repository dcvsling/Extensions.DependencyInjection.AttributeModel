using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Render
{
    internal class DecoratorSourceDecision : ISourceDecision
    {
        public IEnumerable<Type> Apply(DecisionContext context)
            => context.Metadata.ClassSyntax.Identifier.Text.Contains("Decorator")
                ? context.Candidates.Intersect(CandidateTypes)
                : context.Candidates.Except(CandidateTypes);

        private IEnumerable<Type> CandidateTypes
            => new[]{ 
                typeof(AllArgumentDecorator),
                typeof(AllGenericDecorator),
                typeof(GenericWithInstanceOrFactoryDecorator)
            };
    }
}
