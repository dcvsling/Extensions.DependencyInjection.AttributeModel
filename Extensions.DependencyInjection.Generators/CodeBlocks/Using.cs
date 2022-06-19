using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class Using : SourceProviderBase, IUsing
    {
        private readonly string _path;

        public Using(string path)
        {
            _path = path;
        }
        public override string ToString()
            => $@"using {_path};";
    }
}