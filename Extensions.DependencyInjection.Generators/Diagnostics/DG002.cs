using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public class DG002 : IDiagnosticHandler
    {
        public ValidateResult Valid(AttributeMetadata metadata)
           => new ValidateResult
           {
               Metadata = metadata,
               Diagnostic = ShouldValid(metadata) && !InternalValid(metadata)
                ? SetDiagnostic(metadata)
                : default
           };
           

        private static bool ShouldValid(AttributeMetadata metadata)
            => !string.IsNullOrWhiteSpace(metadata.InstanceOrFactory)
                && metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory) != null;

        private static bool InternalValid(AttributeMetadata metadata)
            => GetMemberTypeName(metadata) == metadata.GetRegisterName();

        public static Diagnostic SetDiagnostic(AttributeMetadata metadata)
            => Diagnostic.Create(
                DiagnosticDescriptors.DG002,
                metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory).GetLocation(),
                metadata.InstanceOrFactory,
                metadata.GetRegisterName());
        private static string GetMemberTypeName(AttributeMetadata metadata)
            => metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory) is var syntax
                ? syntax is PropertyDeclarationSyntax property
                    ? property.Type.ToFullString().Trim()
                    : syntax is MethodDeclarationSyntax method
                        ? method.GetName()
                        : default
                : default;
    }
}
