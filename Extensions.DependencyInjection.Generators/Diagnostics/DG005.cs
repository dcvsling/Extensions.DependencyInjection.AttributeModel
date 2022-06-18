using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public class DG005 : IDiagnosticHandler
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
            => metadata.AttributeSyntax.IsNameMatch(Constant.DECORATOR_ATTRIBUTE_NAMES);

        private static Diagnostic SetDiagnostic(AttributeMetadata metadata)
            => Diagnostic.Create(
                DiagnosticDescriptors.DG005,
                (metadata.ClassSyntax.Members.OfType<ConstructorDeclarationSyntax>()
                    .SelectMany(ctor => ctor.ParameterList.Parameters.Where(p => p.Type.ToFullString().Trim() == metadata.ServiceType))
                    ?.FirstOrDefault() 
                    ?? (SyntaxNode)metadata.ClassSyntax)
                    .GetLocation(),
                    GetServiceClosedType(metadata)
                );

        private static bool InternalValid(AttributeMetadata metadata)
        {
            var serviceType = GetServiceClosedType(metadata);
            
            return metadata.ClassSyntax.Members.OfType<ConstructorDeclarationSyntax>()
                .Any(ctor => ctor.ParameterList.Parameters.Any(p => p.Type.ToFullString().Trim() == serviceType));
        }
        private static string GetServiceClosedType(AttributeMetadata metadata)
            => metadata.ClassSyntax.IsGenericType()
                ? $"{metadata.ServiceType.Split('<').First()}<{string.Join(", ", metadata.ClassSyntax.TypeParameterList.Parameters.Select(p => p.ToFullString().Trim()))}>"
                : metadata.ServiceType;
    }   
}
