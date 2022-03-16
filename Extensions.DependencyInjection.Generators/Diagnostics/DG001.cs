
using Microsoft.CodeAnalysis;

using System;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    internal class DG001
    {
        public static bool Valid(InjectMetadata metadata, Action<Diagnostic> callback)
            => ShouldValid(metadata)
                && !InternalValid(metadata)
                && SetDiagnostic(metadata, callback);

        public static bool ShouldValid(InjectMetadata metadata)
            => !string.IsNullOrWhiteSpace(metadata.MemberName);


        private static bool InternalValid(InjectMetadata metadata)
            => metadata.ClassSyntax.Members.Any(
                member => member.GetName() == metadata.MemberName);

        private static bool SetDiagnostic(InjectMetadata metadata, Action<Diagnostic> callback)
        {
            callback(Diagnostic.Create(
                DiagnosticDescriptors.DG001,
                metadata.AttributeSyntax.GetArgumentByName("MemberName").GetLocation(),
                metadata.MemberName));
            return true;
        }
    }
}
