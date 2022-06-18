namespace Microsoft.Extensions.DependencyInjection;

public interface IDesignTimeServiceCollectionConfiguration
{
    
    IServiceCollection ConfigureService(IServiceCollection services);
    IServiceCollection ConfigureDecorator(IServiceCollection services);
}
