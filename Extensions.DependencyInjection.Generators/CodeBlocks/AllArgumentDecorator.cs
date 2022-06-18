namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class AllArgumentDecorator : IDecorator
    {
        private readonly AttributeMetadata _metadata;

        public AllArgumentDecorator(AttributeMetadata metadata)
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Decorator({_metadata.ServiceType.WrapTypeOf()}, {_metadata.ImplementationType.WrapTypeOf()});";
    }
}