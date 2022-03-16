using Extensions.DependencyInjection.Generators.Diagnostics;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators
{
    public delegate bool DiagnosticDelegate(InjectMetadata metadata, Action<Diagnostic> callback);
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
        public static InjectMetadata Diagnostic(this InjectMetadata metadata, Action<Diagnostic> reportDiag)
        {
            var result = new List<Diagnostic>();
            foreach (var diag in _diags)
            {
                diag(metadata, result.Add);
            }
            if (result.Count > 0)
            {
                result.ForEach(reportDiag);
                return default;
            }
            else
                return metadata;
        }
    }
}
