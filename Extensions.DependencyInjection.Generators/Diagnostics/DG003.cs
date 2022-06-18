using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public class DG003 : IDiagnosticHandler
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
            => metadata.Lifetime == "Singleton"
                && !string.IsNullOrWhiteSpace(metadata.InstanceOrFactory)
                && metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory) != null;

        private static Diagnostic SetDiagnostic(AttributeMetadata metadata)
            => Diagnostic.Create(
                DiagnosticDescriptors.DG003,
                metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory).GetLocation());
        private static bool InternalValid(AttributeMetadata metadata)
            => metadata.ClassSyntax.GetMemberByName(metadata.InstanceOrFactory) is var syntax
                && syntax is PropertyDeclarationSyntax property
                && property.Modifiers.HasModifierByKinds(SyntaxKind.StaticKeyword)
                && property.AccessorList.Accessors.Count == 1
                && property.AccessorList.Accessors[0].Keyword.IsKind(SyntaxKind.GetKeyword);
    }
}
