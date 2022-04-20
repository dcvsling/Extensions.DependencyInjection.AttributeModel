using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class Usings : SourceProviderCollectionBase<IUsing>, IUsing
    {
        private readonly IEnumerable<IUsing> _usings;

        public Usings(IEnumerable<IUsing> usings)
        {
            _usings = usings;
        }

        public override string ToString()
            => string.Join(Environment.NewLine, _usings.Distinct().Select(x => x.ToString()));

        public override IEnumerator<IUsing> GetEnumerator()
        {
            return _usings.GetEnumerator();
        }

    }
}