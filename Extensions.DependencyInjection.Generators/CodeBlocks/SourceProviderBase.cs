using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal abstract class SourceProviderBase : ISourceProvider
    {
        public override int GetHashCode()
            => ToString().GetHashCode();
        public override bool Equals(object obj)
            => (obj?.GetHashCode() ?? 0) == GetHashCode();
    }
}