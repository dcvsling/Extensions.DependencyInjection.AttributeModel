namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class AllArgumentOfTypeRegister : SourceProviderBase, IRegister
    {
        private readonly AttributeMetadata _metadata;
        public AllArgumentOfTypeRegister (AttributeMetadata metadata) 
        {
            _metadata = metadata;
        }
        public override string ToString()
        => $@"services.Add{_metadata.Lifetime}({_metadata.ServiceType.WrapTypeOf()}, {_metadata.ImplementationType.WrapTypeOf()});";
        
    }
}