using Microsoft.CodeAnalysis;

using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators
{
    public class DiagnosticResult
    {
        public AttributeMetadata Metadata { get; set; }
        public IEnumerable<Diagnostic> Diagnostics { get; set; }
    }
}
