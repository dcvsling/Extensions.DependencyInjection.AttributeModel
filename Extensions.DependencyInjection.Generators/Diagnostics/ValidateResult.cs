using Microsoft.CodeAnalysis;

namespace Extensions.DependencyInjection.Generators
{
    public class ValidateResult
    {
        public AttributeMetadata Metadata { get; set; }
        public Diagnostic Diagnostic { get; set; }
    }
}
