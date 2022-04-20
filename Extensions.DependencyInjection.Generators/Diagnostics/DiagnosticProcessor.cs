using Extensions.DependencyInjection.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators
{
    public delegate bool DiagnosticDelegate(AttributeMetadata metadata, Action<Diagnostic> callback);
    public static class DiagnosticProcessor
    {
        private static readonly List<DiagnosticDelegate> _diags = new List<DiagnosticDelegate>
        {
            DG001.Valid,
            DG002.Valid,
            DG003.Valid,
            DG004.Valid
        };
        public static void RegisterDiagnostic(DiagnosticDelegate @delegate)
            => _diags.Add(@delegate);
        public static TResult Diagnostic<TResult>(this AttributeMetadata metadata, Func<IEnumerable<Diagnostic>, TResult> reportDiag, Func<AttributeMetadata, TResult> func)
        {
            var result = new List<Diagnostic>();
            foreach (var diag in _diags)
            {
                diag(metadata, result.Add);
            }
            return result.Count > 0
                ? reportDiag(result)
                : func(metadata);
        }
    }
}
