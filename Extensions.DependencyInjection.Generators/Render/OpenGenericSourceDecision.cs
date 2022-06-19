using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Render
{
    internal class OpenGenericSourceDecision : ISourceDecision
    {

        public IEnumerable<Type> Apply(DecisionContext context)
            => context.Metadata.ClassSyntax.IsGenericType()
                ? context.Candidates.Intersect(CandidateTypes)
                : context.Candidates.Except(CandidateTypes);

        private IEnumerable<Type> CandidateTypes
            => new[]{
                typeof(ArgumentOfImplementationTypeOnlyRegister),
                typeof(AllArgumentDecorate),
                typeof(AllArgumentOfTypeRegister)
            };
    }
}
