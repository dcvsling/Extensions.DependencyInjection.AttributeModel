using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Render
{
    internal class DecorateSourceDecision : ISourceDecision
    {
        public IEnumerable<Type> Apply(DecisionContext context)
            => context.Metadata.ClassSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(attr => attr.Name.ToFullString().Contains("Decorator"))
                ? context.Candidates.Intersect(CandidateTypes)
                : context.Candidates.Except(CandidateTypes);

        private IEnumerable<Type> CandidateTypes
            => new[]{ 
                typeof(AllArgumentDecorate),
                typeof(AllGenericDecorate),
                typeof(GenericWithInstanceOrFactoryDecorate)
            };
    }
}
