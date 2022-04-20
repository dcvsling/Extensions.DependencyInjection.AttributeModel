using Extensions.DependencyInjection.Generators.Abstractions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Immutable;
using System.Text;
using System.Threading;

namespace Extensions.DependencyInjection.Generators
{
    internal class DependencyInjectionRegisterHandler : IGeneratorHandler<AttributeMetadata>
    {
        public void RegisterSourceOutput(SourceProductionContext context, (Compilation Left, ImmutableArray<AttributeMetadata> Right) source)
        {
            try
            {
                if (source.Right.Length == 0) return;
                context.AddSource(
                    Constant.DEFAULT_HINT_NAME, 
                    SourceText.From(
                        new Emitter(context.ReportDiagnostic)
                            .GenerateOutput(source.Left.Assembly.Name, source.Right)
                            .ToString(), 
                        Encoding.UTF8));
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.DG000, null, ex));
            }
        }

        public AttributeMetadata SourceProvider(GeneratorSyntaxContext context, CancellationToken _)
            => new AttributeMetadata((AttributeSyntax)context.Node);

        public bool SyntaxFilter(SyntaxNode syntax, CancellationToken _)
            => syntax is AttributeSyntax attr && attr.Name.ToFullString().Trim().IsInjectAttribute();
    }
}



