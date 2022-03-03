using Microsoft.CodeAnalysis.Text;

namespace Extensions.DependencyInjection.Generators;

internal static class GeneratedCodes
{
    public static SourceText CreateRegistryAttribute(string @namespace, IEnumerable<string> usings, IEnumerable<string> sources) 
        => SourceText.From($@"
{string.Join(Environment.NewLine, usings)}
[assembly: ServiceRegistry]
namespace {@namespace};
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public class ServiceRegistryAttribute : Attribute, IDesignTimeServiceCollectionConfiguration
{{
    public IServiceCollection ConfigureService(IServiceCollection services)
    {{
        {string.Join(Environment.NewLine + "        ", sources)}
        return services;
    }}
}}");
    public static string CreateSelfOnlyGenericDependencyRegistry(string lifetime, string implType)
        => $@"services.Add{lifetime}<{implType}>();";
    public static string CreateSelfOnlyArgumentDependencyRegistry(string lifetime, string implType)
        => $@"services.Add{lifetime}(typeof({implType}));";
    public static string CreateSelfWithGetterDependencyRegistry(string lifetime, string getter)
        => $@"services.Add{lifetime}({getter});";
    public static string CreateAllGenericDependencyRegistry(string lifetime, string serviceType, string implType)
        => $@"services.Add{lifetime}<{serviceType}, {implType}>();";
    public static string CreateAllArgumentTypeDependencyRegistry(string lifetime, string serviceType, string implType)
        => $@"services.Add{lifetime}(typeof({serviceType}), typeof({implType}));";
    public static string CreateGetterDependencyRegistry(string lifetime, string serviceType, string getter)
        => $@"services.Add{lifetime}<{serviceType}>({getter});";

}
