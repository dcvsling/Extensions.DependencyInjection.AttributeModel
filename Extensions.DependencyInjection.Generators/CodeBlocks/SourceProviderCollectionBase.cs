using Extensions.DependencyInjection.Generators.Abstractions;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal abstract class SourceProviderCollectionBase<T> : IEnumerable<T> where T : class, ISourceProvider
    {
        public override bool Equals(object obj)
           => obj.GetHashCode() == GetHashCode();
        public abstract IEnumerator<T> GetEnumerator();
        public override int GetHashCode()
            => this.Select(x => x.GetHashCode()).Aggregate(0, (l, r) => l ^ r);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}