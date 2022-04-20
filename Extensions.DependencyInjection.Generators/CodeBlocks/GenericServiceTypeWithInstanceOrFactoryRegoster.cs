using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class GenericServiceTypeWithInstanceOrFactoryRegoster : SourceProviderBase, IRegister
    {
        private readonly AttributeMetadata _metadata;
        public GenericServiceTypeWithInstanceOrFactoryRegoster (AttributeMetadata metadata) 
        {
            _metadata = metadata;
        }
        public override string ToString()
        => $@"services.Add{_metadata.Lifetime}<{_metadata.ServiceType.UnwrapTypeOf()}>({_metadata.ImplementationType.UnwrapTypeOf()}.{_metadata.MemberName.UnwrapQuotes()});";
    }
}