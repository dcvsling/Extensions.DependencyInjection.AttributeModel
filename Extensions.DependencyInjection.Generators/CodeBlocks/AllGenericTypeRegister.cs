using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class AllGenericTypeRegister : SourceProviderBase, IRegister
    {
        private readonly AttributeMetadata _metadata;
        public AllGenericTypeRegister (AttributeMetadata metadata) 
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Add{_metadata.Lifetime}<{_metadata.ServiceType.UnwrapTypeOf()}, {_metadata.ImplementationType.UnwrapTypeOf()}>();";
    }
}