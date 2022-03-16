
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators
{
    [Generator]
    public partial class DependencyRegisterGenerator : IIncrementalGenerator
    {

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var classDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    (syntax, _) => syntax is ClassDeclarationSyntax,
                    (ctx, _) => ctx.Node is ClassDeclarationSyntax classSyntax
                        && classSyntax.GetInjectAttributeSyntax() is IEnumerable<AttributeSyntax> attrs
                            ? attrs.Select(attr => new InjectMetadata(attr, classSyntax))
                            : Enumerable.Empty<InjectMetadata>())
                    .SelectMany((x, _) => x);
            var complierWithClass = context.CompilationProvider.Combine(classDeclarations.Collect());
            context.RegisterSourceOutput(complierWithClass, (ctx, src) =>
            {
                try
                {
                    (var name, var text) = src.Right.GenerateOutput(src.Left.Assembly.Name, ctx.ReportDiagnostic);
                    ctx.AddSource(name, text);
                }
                catch (Exception ex)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.DG000, null, ex));
                }
            });
        }
    }
}

