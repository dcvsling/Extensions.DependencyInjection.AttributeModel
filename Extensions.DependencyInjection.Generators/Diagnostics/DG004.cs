using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public class DG004 : IDiagnosticHandler
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
            => metadata.Lifetime != "Singleton"
                && !string.IsNullOrWhiteSpace(metadata.InstanceOrFactory)
                && metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory) != null;
        public static Diagnostic SetDiagnostic(AttributeMetadata metadata)
           => Diagnostic.Create(
                    DiagnosticDescriptors.DG004,
                    metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory).GetLocation());
        
        private static bool InternalValid(AttributeMetadata metadata)
            => metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory) is var syntax
                && syntax is MethodDeclarationSyntax method
                && method.Modifiers.HasModifierByKinds(SyntaxKind.StaticKeyword)
                && method.ParameterList.Parameters.Count == 1
                && method.ParameterList.Parameters[0].Type.ToFullString().Trim() == nameof(IServiceProvider)
                && method.ReturnType.ToFullString().Trim() == "object";
    }
}
