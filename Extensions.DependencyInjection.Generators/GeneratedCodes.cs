
using Microsoft.CodeAnalysis.Text;

using System;
using System.Linq;
using System.Text;

namespace Extensions.DependencyInjection.Generators
{

    internal static class GeneratedCodes
    {
        public static SourceText CreateRegistryAttribute(GenerateContext context)
            => SourceText.From($@"
using System;
using Extensions.DependencyInjection.AttributeModel;
using Microsoft.Extensions.DependencyInjection;

{string.Join(Environment.NewLine, context.Usings.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().Select(x => $"using {x};"))}

[assembly: {context.Namespace}.ServiceRegistry]

namespace {context.Namespace} 
{{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class ServiceRegistryAttribute : Attribute, IDesignTimeServiceCollectionConfiguration
    {{
        public IServiceCollection ConfigureService(IServiceCollection services)
        {{
            {string.Join(Environment.NewLine + "            ", context.Sources.Distinct())}
            return services;
        }}
    }}
}}", Encoding.UTF8);
        public static string CreateSelfOnlyGenericDependencyRegistry(this InjectMetadata metadata)
            => $@"services.Add{metadata.Lifetime}<{metadata.ImplementationType.UnwrapTypeOf()}>();";
        public static string CreateSelfOnlyArgumentDependencyRegistry(this InjectMetadata metadata)
            => $@"services.Add{metadata.Lifetime}({metadata.ImplementationType.WrapTypeOf()});";
        public static string CreateSelfWithGetterDependencyRegistry(this InjectMetadata metadata)
            => $@"services.Add{metadata.Lifetime}({metadata.ImplementationType.UnwrapTypeOf()}.{metadata.MemberName.UnwrapQuotes()});";
        public static string CreateAllGenericDependencyRegistry(this InjectMetadata metadata)
            => $@"services.Add{metadata.Lifetime}<{metadata.ServiceType.UnwrapTypeOf()}, {metadata.ImplementationType.UnwrapTypeOf()}>();";
        public static string CreateAllArgumentTypeDependencyRegistry(this InjectMetadata metadata)
            => $@"services.Add{metadata.Lifetime}({metadata.ServiceType.WrapTypeOf()}, {metadata.ImplementationType.WrapTypeOf()});";
        public static string CreateGetterDependencyRegistry(this InjectMetadata metadata)
            => $@"services.Add{metadata.Lifetime}<{metadata.ServiceType.UnwrapTypeOf()}>({metadata.ImplementationType.UnwrapTypeOf()}.{metadata.MemberName.UnwrapQuotes()});";
    }
}