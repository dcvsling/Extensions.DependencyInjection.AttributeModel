using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class ImplementationInstanceOnlyRegister : SourceProviderBase, IRegister
    {
        private readonly AttributeMetadata _metadata;
        public ImplementationInstanceOnlyRegister (AttributeMetadata metadata) 
        {
            _metadata = metadata;
        }
        public override string ToString()
        => $@"services.Add{_metadata.Lifetime}({_metadata.ImplementationType.UnwrapTypeOf()}.{_metadata.MemberName.UnwrapQuotes()});";
    }
}