using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class UsingFromString : SourceProviderBase, IUsing
    {
        private readonly string _path;

        public UsingFromString(string path)
        {
            _path = path;
        }
        public override string ToString()
            => $@"using {_path};";
    }
}