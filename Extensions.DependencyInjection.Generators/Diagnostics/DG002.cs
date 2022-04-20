using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public class DG002
    {
        public static bool Valid(AttributeMetadata metadata, Action<Diagnostic> callback)
           => ShouldValid(metadata)
               && !InternalValid(metadata)
               && SetDiagnostic(metadata, callback);

        private static bool ShouldValid(AttributeMetadata metadata)
            => !string.IsNullOrWhiteSpace(metadata.MemberName)
                && metadata.ClassSyntax.GetMemberByName(metadata.MemberName) != null;

        private static bool InternalValid(AttributeMetadata metadata)
            => GetMemberTypeName(metadata) == metadata.GetRegisterName();

        public static bool SetDiagnostic(AttributeMetadata metadata, Action<Diagnostic> setter)
        {
            setter(Diagnostic.Create(
                DiagnosticDescriptors.DG002,
                metadata.ClassSyntax.GetMemberByName(metadata.MemberName).GetLocation(),
                metadata.MemberName,
                metadata.GetRegisterName()));

            return true;
        }
        private static string GetMemberTypeName(AttributeMetadata metadata)
            => metadata.ClassSyntax.GetMemberByName(metadata.MemberName) is var syntax
                ? syntax is PropertyDeclarationSyntax property
                    ? property.Type.ToFullString().Trim()
                    : syntax is MethodDeclarationSyntax method
                        ? method.GetName()
                        : default
                : default;
    }
}
