using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators
{
    public class Emitter
    {
        public string HintName { get; set; } = Constant.DEFAULT_HINT_NAME;
        public ISourceProvider Namespace { get; set; } = new Namespace(Constant.DEFAULT_NAMESPACE);
        public List<ISourceProvider> Usings { get; }
            = Constant.DEFAULT_USINGS.Select(x => new UsingFromString(x))
                .OfType<ISourceProvider>()
                .ToList();
        private readonly Action<Diagnostic> _reportDiag;

        public Emitter(Action<Diagnostic> reportDiag)
        {
            _reportDiag = reportDiag;
        }
        public ISourceProvider GenerateOutput(string assemblyName, IEnumerable<AttributeMetadata> injects)
        {
            (var usings, var register) = injects.Select(Emit)
                .OfType<GeneratorSource>()
                .Aggregate(
                    (usings: Enumerable.Empty<IUsing>(), register: Enumerable.Empty<IRegister>()),
                    (ctx, content) => (usings: ctx.usings.Union(content.Using), register: ctx.register.Append<IRegister>(content.Register)));
            return new ServiceRegisterSourceProvider
            {
                Namespace = new Namespace(assemblyName),
                GlobalAttribute = new GlobalAttribute($"{assemblyName}.{Constant.DEFAULT_REGISTER_ATTRIBUTE_NAME}"),
                Usings = new Usings(usings.Distinct()),
                Register = new Registers(register.Distinct())
            };
        }
        private GeneratorSource Emit(AttributeMetadata metadata)
            => metadata.Diagnostic(ReportDiagnostic, CreateGeneratorSource);

        private GeneratorSource ReportDiagnostic(IEnumerable<Diagnostic> diags)
        {
            diags.Aggregate(_reportDiag, Reducer);
            return default(GeneratorSource);
        }
        private static Action<Diagnostic> Reducer(Action<Diagnostic> reporter, Diagnostic diagnostic)
        {
            reporter(diagnostic);
            return reporter;
        }
        private GeneratorSource CreateGeneratorSource(AttributeMetadata metadata)
            => new GeneratorSource(
                metadata.ClassSyntax.SyntaxTree
                    .GetCompilationUnitRoot()
                    .Usings
                    .Select(@using => new UsingFromSyntax(@using))
                    .OfType<IUsing>()
                    .Prepend(new UsingFromString(metadata.Namespace)),
                ConditionServiceType(metadata));


        private IRegister ConditionServiceType(AttributeMetadata metadata)
            => string.IsNullOrWhiteSpace(metadata.ServiceType)
                ? metadata.IsGenericType
                    ? new ArgumentOfImplementationTypeOnlyRegister(metadata)
                    : string.IsNullOrWhiteSpace(metadata.MemberName)
                        ? new GenericImplementationTypeOnlyRegister(metadata)
                        : (IRegister)new ImplementationInstanceOnlyRegister(metadata)
                : metadata.IsGenericType
                    ? new AllArgumentOfTypeRegister(metadata)
                    : string.IsNullOrWhiteSpace(metadata.MemberName)
                        ? new AllGenericTypeRegister(metadata)
                        : (IRegister)new GenericServiceTypeWithInstanceOrFactoryRegoster(metadata);
    }
}

