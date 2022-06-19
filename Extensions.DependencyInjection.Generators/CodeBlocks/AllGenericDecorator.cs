namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class AllGenericDecorator : IDecorator
    {
        private readonly AttributeMetadata _metadata;

        public AllGenericDecorator(AttributeMetadata metadata)
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Decorator<{_metadata.ServiceType.UnwrapTypeOf()}, {_metadata.ImplementationType.UnwrapTypeOf()}>();";
    }
}