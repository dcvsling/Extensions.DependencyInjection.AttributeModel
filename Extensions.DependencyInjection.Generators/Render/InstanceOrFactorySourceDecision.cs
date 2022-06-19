using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Render
{
    internal class InstanceOrFactorySourceDecision : ISourceDecision
    {
        public IEnumerable<Type> Apply(DecisionContext context)
            => string.IsNullOrWhiteSpace(context.Metadata.InstanceOrFactory)
                ? context.Candidates.Except(CandidateTypes)
                : context.Candidates.Intersect(CandidateTypes);

        private IEnumerable<Type> CandidateTypes
            => new[]{
                typeof(GenericServiceTypeWithInstanceOrFactoryRegoster),
                typeof(GenericWithInstanceOrFactoryDecorator) 
            };
    }
}
