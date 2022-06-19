using Extensions.DependencyInjection.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators
{
    internal class DefaultDiagnosticProcessor : IDiagnosticProcessor
    {
        private readonly IEnumerable<IDiagnosticHandler> _handlers;

        public DefaultDiagnosticProcessor(IEnumerable<IDiagnosticHandler> handlers)
        {
            _handlers = handlers;
        }

        public DiagnosticResult Diagnostic(AttributeMetadata metadata)
            => new DiagnosticResult
            {
                Metadata = metadata,
                Diagnostics = _handlers
                    .Select(x => x.Valid(metadata))
                    .Select(x => x.Diagnostic)
                    .OfType<Diagnostic>()
            };
    }
}
