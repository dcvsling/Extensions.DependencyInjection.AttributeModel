using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators.Render
{
    public class DecisionContext
    {
        public AttributeMetadata Metadata { get; set; }
        public IEnumerable<Type> Candidates { get; set; }
    }
}
