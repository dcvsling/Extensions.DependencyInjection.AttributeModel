
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class AttributeBaseSourceGeneratorExtensions
{
    public static IServiceCollection AddAttributeModelRegister(this IServiceCollection services)
        => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetCustomAttributes())
            .OfType<IDesignTimeServiceCollectionConfiguration>()
            .Aggregate(services, (srv, config) => config.ConfigureService(srv));
}