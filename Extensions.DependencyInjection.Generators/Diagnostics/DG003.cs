using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public class DG003
    {
        public static bool Valid(InjectMetadata metadata, Action<Diagnostic> callback)
           => ShouldValid(metadata)
               && !InternalValid(metadata)
               && SetDiagnostic(metadata, callback);
        private static bool ShouldValid(InjectMetadata metadata)
            => metadata.Lifetime == "Singleton"
                && !string.IsNullOrWhiteSpace(metadata.MemberName)
                && metadata.ClassSyntax.GetMemberByName(metadata.MemberName) != null;

        private static bool SetDiagnostic(InjectMetadata metadata, Action<Diagnostic> callback)
        {
            callback(Diagnostic.Create(
                DiagnosticDescriptors.DG003,
                metadata.ClassSyntax.GetMemberByName(metadata.MemberName).GetLocation()));
            return true;
        }
        private static bool InternalValid(InjectMetadata metadata)
            => metadata.ClassSyntax.GetMemberByName(metadata.MemberName) is var syntax
                && syntax is PropertyDeclarationSyntax property
                && property.Modifiers.HasModifierByKinds(SyntaxKind.StaticKeyword)
                && !property.AccessorList.Accessors.Any(SyntaxKind.SetKeyword);
    }
}
