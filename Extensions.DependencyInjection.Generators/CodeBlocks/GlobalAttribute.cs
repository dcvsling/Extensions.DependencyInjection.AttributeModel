using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class GlobalAttribute : ISourceProvider
    {
        private readonly string _attributeFullName;

        public GlobalAttribute(string attributeFullName)
        {
            _attributeFullName = attributeFullName;
        }
        
        public override string ToString()
            => $@"[assembly: {_attributeFullName}]";
    }
}
