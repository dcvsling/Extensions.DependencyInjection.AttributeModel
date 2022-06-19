namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class AllArgumentDecorate : IDecorate
    {
        private readonly AttributeMetadata _metadata;

        public AllArgumentDecorate(AttributeMetadata metadata)
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Decorate({_metadata.ServiceType.WrapTypeOf()}, {_metadata.ImplementationType.WrapTypeOf()});";
    }
}