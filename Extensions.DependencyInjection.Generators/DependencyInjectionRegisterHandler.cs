using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.Render;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Extensions.DependencyInjection.Generators
{
    internal class DependencyInjectionRegisterHandler : IGeneratorHandler<AttributeMetadata>
    {
        private static readonly char[] TRIM_CHAR = new[] { ' ', '"' };

        public void RegisterSourceOutput(SourceProductionContext context, (Compilation Left, ImmutableArray<AttributeMetadata> Right) source)
        {
            try
            {
                if (source.Right.Length == 0) return;
                context.AddSource(
                    Constant.DEFAULT_HINT_NAME, 
                    SourceText.From(
                        Emitter.Create(context)
                            .GenerateOutput(source.Left.Assembly.Name, source.Right)
                            .ToString(), 
                        Encoding.UTF8));
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.DG000, null, ex));
            }
        }

        public AttributeMetadata ProjectSource(GeneratorSyntaxContext context, CancellationToken _)
            => CreateMetadata((AttributeSyntax)context.Node, context.Node.GetParentNode<ClassDeclarationSyntax>());

        public bool SyntaxFilter(SyntaxNode syntax, CancellationToken _)
            => syntax is AttributeSyntax attr && attr.IsNameMatch(Constant.INJECT_ATTRIBUTE_NAMES.Concat(Constant.DECORATOR_ATTRIBUTE_NAMES));
        
        private AttributeMetadata CreateMetadata(AttributeSyntax attr, ClassDeclarationSyntax cs)
            => new AttributeMetadata
            {
                AttributeSyntax = attr,
                ClassSyntax = cs,
                Lifetime = (attr.Name.ToFullString().Trim().Split('.').Last().StartsWith("Inject")
                    ? attr.GetArgumentByName(nameof(AttributeMetadata.Lifetime)).Expression.ToFullString().Trim().Split(new string[] { "ServiceLifetime." }, StringSplitOptions.RemoveEmptyEntries).Last()
                    : attr.Name.ToFullString().Trim().Split('.').Last().Replace("Attribute", string.Empty)),
                ServiceType = attr.GetArgumentByName(nameof(AttributeMetadata.ServiceType))?.Expression.ToFullString().Trim().UnwrapTypeOf() ?? string.Empty,
                InstanceOrFactory = attr.GetArgumentByName(nameof(AttributeMetadata.InstanceOrFactory))?.Expression.ToFullString().Trim(TRIM_CHAR) ?? string.Empty,
                DecoratedType = attr.GetArgumentByName(nameof(AttributeMetadata.DecoratedType))?.Expression.ToFullString().Trim().UnwrapTypeOf() ?? string.Empty,
                ImplementationType = cs.Identifier.ValueText + (cs.IsGenericType() ? $"<{string.Join(string.Empty, Enumerable.Repeat(",", cs.TypeParameterList.Parameters.Count - 1))}>" : string.Empty),
                Namespace = cs.GetNamespaceSyntax().Name.ToFullString().Trim()
            };
    }
}
