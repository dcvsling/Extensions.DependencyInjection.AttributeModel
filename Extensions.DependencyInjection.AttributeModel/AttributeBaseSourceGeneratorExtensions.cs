
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class AttributeBaseSourceGeneratorExtensions
{
    public static IServiceCollection AddAttributeModelRegister(this IServiceCollection services)
    {
        if (services.Any(x => x.ImplementationInstance?.Equals(Markup.Instance) ?? false))
            return services;
        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetCustomAttributes())
            .OfType<IDesignTimeServiceCollectionConfiguration>()
            .Aggregate(services, (srv, config) => config.ConfigureService(srv));
        services.Insert(0, ServiceDescriptor.Singleton(Markup.Instance));
        return services;
    }

    private class Markup
    {
        private Markup() { }
        public static Markup Instance { get; } = new Markup();
    }
}

