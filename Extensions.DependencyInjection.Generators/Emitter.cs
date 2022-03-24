using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Linq;

namespace Extensions.DependencyInjection.Generators
{
    public class Emitter
    {
        private readonly Action<Diagnostic> _reportDiag;

        public Emitter(Action<Diagnostic> reportDiag)
        {
            _reportDiag = reportDiag;
        }
        public DependencyContent Emit(InjectMetadata metadata)
            => new DependencyContent(
                    metadata.ClassSyntax.SyntaxTree
                        .GetCompilationUnitRoot()
                        .Usings
                        .Select(@using => @using.Name.ToString())
                        .Concat(metadata.Namespace.Emit()),
                    metadata.Diagnostic(_reportDiag) is null
                        ? string.Empty
                        : ConditionServiceType(metadata));

        private string ConditionServiceType(InjectMetadata metadata)
            => string.IsNullOrWhiteSpace(metadata.ServiceType)
                ? ConditionTypeParamaeterWithoutServiceType(metadata)
                : ConditionTypeParamaeterWithServiceType(metadata);

        private string ConditionTypeParamaeterWithoutServiceType(InjectMetadata metadata)
            => metadata.IsGenericType
                ? metadata.CreateSelfOnlyArgumentDependencyRegistry()
                : ConditionUseInstanceOrFactoryWithoutServiceType(metadata);
        private string ConditionUseInstanceOrFactoryWithoutServiceType(InjectMetadata metadata)
            => string.IsNullOrWhiteSpace(metadata.MemberName)
                ? metadata.CreateSelfOnlyGenericDependencyRegistry()
                : metadata.CreateSelfWithGetterDependencyRegistry();
        private string ConditionTypeParamaeterWithServiceType(InjectMetadata metadata)
         => metadata.IsGenericType
             ? metadata.CreateAllArgumentTypeDependencyRegistry()
             : ConditionUseInstanceOrFactoryWithServiceType(metadata);
        private string ConditionUseInstanceOrFactoryWithServiceType(InjectMetadata metadata)
            => string.IsNullOrWhiteSpace(metadata.MemberName)
                ? metadata.CreateAllGenericDependencyRegistry()
                : metadata.CreateGetterDependencyRegistry();
    }
}

