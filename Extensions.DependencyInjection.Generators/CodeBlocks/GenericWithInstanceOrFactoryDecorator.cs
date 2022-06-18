namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class GenericWithInstanceOrFactoryDecorator : IDecorator
    {
        private readonly AttributeMetadata _metadata;

        public GenericWithInstanceOrFactoryDecorator(AttributeMetadata metadata)
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Decorator<{_metadata.ServiceType.UnwrapTypeOf()}>({_metadata.ImplementationType.UnwrapTypeOf()}.{_metadata.InstanceOrFactory.UnwrapQuotes()});";
    }
}