namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class GenericImplementationTypeOnlyRegister : SourceProviderBase, IRegister
    {
        private readonly AttributeMetadata _metadata;

        public GenericImplementationTypeOnlyRegister(AttributeMetadata metadata)
        {
            _metadata = metadata;
        }
        public override string ToString()
            => $@"services.Add{_metadata.Lifetime}<{_metadata.ImplementationType.UnwrapTypeOf()}>();";
    }
}