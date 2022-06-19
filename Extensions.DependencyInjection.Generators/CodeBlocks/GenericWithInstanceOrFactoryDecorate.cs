namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class GenericWithInstanceOrFactoryDecorate : IDecorate
    {
        private readonly AttributeMetadata _metadata;

        public GenericWithInstanceOrFactoryDecorate(AttributeMetadata metadata)
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Decorate<{_metadata.ServiceType.UnwrapTypeOf()}>({_metadata.ImplementationType.UnwrapTypeOf()}.{_metadata.InstanceOrFactory.UnwrapQuotes()});";
    }
}