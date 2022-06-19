using Microsoft.CodeAnalysis;

using System;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    internal class DG001 : IDiagnosticHandler
    {
        public ValidateResult Valid(AttributeMetadata metadata)
            => new ValidateResult
            {
                Metadata = metadata,
                Diagnostic = ShouldValid(metadata) && !InternalValid(metadata)
                    ? SetDiagnostic(metadata)
                    : default
            };

        public static bool ShouldValid(AttributeMetadata metadata)
            => !string.IsNullOrWhiteSpace(metadata.InstanceOrFactory);


        private static bool InternalValid(AttributeMetadata metadata)
            => metadata.ClassSyntax.Members.Any(
                member => member.GetName() == metadata.InstanceOrFactory);

        private static Diagnostic SetDiagnostic(AttributeMetadata metadata)
            => Diagnostic.Create(
                DiagnosticDescriptors.DG001,
                metadata.AttributeSyntax.GetArgumentByName(nameof(AttributeMetadata.InstanceOrFactory)).GetLocation(),
                metadata.InstanceOrFactory);
    }
}
