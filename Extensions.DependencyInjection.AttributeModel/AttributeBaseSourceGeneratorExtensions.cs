
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class AttributeBaseSourceGeneratorExtensions
{
    public static IServiceCollection AddAttributeModelRegister(this IServiceCollection services)
    {
        var configs = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetCustomAttributes())
            .OfType<IDesignTimeServiceCollectionConfiguration>()
            .ToArray();

        configs.Aggregate(services, (srv, config) => config.ConfigureService(srv));
        configs.Aggregate(services, (srv, config) => config.ConfigureDecorator(srv));

        return services;
    }        
}