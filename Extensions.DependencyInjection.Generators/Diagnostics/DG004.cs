using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public class DG004
    {
        public static bool Valid(InjectMetadata metadata, Action<Diagnostic> callback)
           => ShouldValid(metadata)
               && !InternalValid(metadata)
               && SetDiagnostic(metadata, callback);
        private static bool ShouldValid(InjectMetadata metadata)
            => metadata.Lifetime != "Singleton"
                && !string.IsNullOrWhiteSpace(metadata.MemberName)
                && metadata.ClassSyntax.GetMemberByName(metadata.MemberName) != null;
        public static bool SetDiagnostic(InjectMetadata metadata, Action<Diagnostic> callback)
        {
            callback(Diagnostic.Create(
                DiagnosticDescriptors.DG004,
                metadata.ClassSyntax.GetMemberByName(metadata.MemberName).GetLocation()));
            return true;
        }
        private static bool InternalValid(InjectMetadata metadata)
            => metadata.ClassSyntax.GetMemberByName(metadata.MemberName) is var syntax
                && syntax is MethodDeclarationSyntax method
                && method.Modifiers.HasModifierByKinds(SyntaxKind.StaticKeyword)
                && method.ParameterList.Parameters.Count == 1
                && method.ParameterList.Parameters[0].Type.ToFullString().Trim() == nameof(IServiceProvider);
    }
}
