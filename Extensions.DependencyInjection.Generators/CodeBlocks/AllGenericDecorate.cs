namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class AllGenericDecorate : IDecorate
    {
        private readonly AttributeMetadata _metadata;

        public AllGenericDecorate(AttributeMetadata metadata)
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Decorate<{_metadata.ServiceType.UnwrapTypeOf()}, {_metadata.ImplementationType.UnwrapTypeOf()}>();";
    }
}