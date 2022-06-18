using Extensions.DependencyInjection.Generators;
using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections.Generic;
using System.Linq;



namespace Extensions.DependencyInjection.Generators.Render
{
    internal class SourceProvider : ISourceProvider
    {
        private readonly IEnumerable<ISourceDecision> _decisions;
        private readonly IEnumerable<Type> _sourceTypes;

        public SourceProvider(IEnumerable<ISourceDecision> decisions, IEnumerable<Type> sourceTypes)
        {
            _decisions = decisions;
            _sourceTypes = sourceTypes;
        }
        
        public ISource Get(AttributeMetadata metadata)
            => (ISource)Activator.CreateInstance(_decisions.Aggregate(
                new DecisionContext { Metadata = metadata, Candidates = _sourceTypes.AsEnumerable() },
                    (ctx, decision) => new DecisionContext { Metadata = metadata, Candidates = decision.Apply(ctx) })
                    .Candidates
                    .FirstOrDefault(),
                metadata);
    }
}
