using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;
using Extensions.DependencyInjection.Generators.Render;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators
{
    public class Emitter
    {
        private readonly SourceProductionContext _context;
        private readonly IDiagnosticProcessor _diagnostic;
        private readonly ISourceProvider _sourceProvider;
        public static Emitter Create(SourceProductionContext context) => new Emitter(context, Diagnostics.Diagnostics.Processor, Renderer.Instance);
        public Emitter(SourceProductionContext context, IDiagnosticProcessor diagnostic, ISourceProvider sourceProvider)
        {
            _context = context;
            _diagnostic = diagnostic;
            _sourceProvider = sourceProvider;
        }
        public string HintName { get; set; } = Constant.DEFAULT_HINT_NAME;
        public ISource Namespace { get; set; } = new Namespace(Constant.DEFAULT_NAMESPACE);
        public List<ISource> Usings { get; }
            = Constant.DEFAULT_USINGS.Select(x => new Using(x))
                .OfType<ISource>()
                .ToList();

        public ISource GenerateOutput(string assemblyName, IEnumerable<AttributeMetadata> injects)
        {
            (var usings, var register) = injects.Select(Emit)
                .OfType<GeneratorSource>()
                .Aggregate(
                    (usings: Enumerable.Empty<ISource>(), register: Enumerable.Empty<ISource>()),
                    (ctx, content) => (
                        usings: ctx.usings.Union(content.Using),
                        register: ctx.register.Append<ISource>(content.Register)));
            return new ServiceRegisterSourceProvider
            {
                AttributeName = Constant.DEFAULT_REGISTER_ATTRIBUTE_NAME,
                InterfaceName = Constant.INJECT_ATTRIBUTE_INTERFACE_NAME,
                Namespace = new Namespace(assemblyName),
                GlobalAttribute = new GlobalAttribute($"{assemblyName}.{Constant.DEFAULT_REGISTER_ATTRIBUTE_NAME}"),
                Usings = new Usings(usings.OfType<IUsing>().Distinct()),
                Register = new Registers(register.OfType<IRegister>().Distinct()),
                Decorator = new Registers(register.OfType<IDecorator>().Distinct())
            };
        }
        private GeneratorSource Emit(AttributeMetadata metadata)
        {
            var result = _diagnostic.Diagnostic(metadata);
            return result.Diagnostics.Any()
                ? ReportDiagnostic(result.Diagnostics)
                : CreateGeneratorSource(metadata);
        }

        private GeneratorSource ReportDiagnostic(IEnumerable<Diagnostic> diags)
        {
            diags.ToList().ForEach(_context.ReportDiagnostic);
            return default;
        }
        private GeneratorSource CreateGeneratorSource(AttributeMetadata metadata)
            => new GeneratorSource
            {
                Using = metadata.ClassSyntax.SyntaxTree
                    .GetCompilationUnitRoot()
                    .Usings
                    .Select(@using => new Using(@using.Name.ToFullString()))
                    .OfType<IUsing>()
                    .Prepend(new Using(metadata.Namespace)),
                Register = _sourceProvider.Get(metadata)
            };

        //private IRegister ConditionServiceType(AttributeMetadata metadata)
        //    => string.IsNullOrWhiteSpace(metadata.ServiceType)
        //        ? metadata.ClassSyntax.IsGenericType()
        //            ? new ArgumentOfImplementationTypeOnlyRegister()
        //            : string.IsNullOrWhiteSpace(metadata.InstanceOrFactory)
        //                ? new GenericImplementationTypeOnlyRegister()
        //                : (IRegister)new ImplementationInstanceOnlyRegister()
        //        : metadata.ClassSyntax.IsGenericType()
        //            ? new AllArgumentOfTypeRegister()
        //            : string.IsNullOrWhiteSpace(metadata.InstanceOrFactory)
        //                ? new AllGenericTypeRegister()
        //                : (IRegister)new GenericServiceTypeWithInstanceOrFactoryRegoster();
    }
}

