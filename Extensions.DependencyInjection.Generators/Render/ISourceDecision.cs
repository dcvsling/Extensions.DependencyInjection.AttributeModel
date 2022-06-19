using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators.Render
{
    public interface ISourceDecision
    {
        IEnumerable<Type> Apply(DecisionContext context);
    }
}
