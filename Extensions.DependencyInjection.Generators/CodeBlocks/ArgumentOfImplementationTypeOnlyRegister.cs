using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class ArgumentOfImplementationTypeOnlyRegister : SourceProviderBase, IRegister
    {
        private readonly AttributeMetadata _metadata;
        public ArgumentOfImplementationTypeOnlyRegister(AttributeMetadata metadata) 
        {
            _metadata = metadata;
        }
        public override string ToString()
           => $@"services.Add{_metadata.Lifetime}({_metadata.ImplementationType.WrapTypeOf()});";
    }
}