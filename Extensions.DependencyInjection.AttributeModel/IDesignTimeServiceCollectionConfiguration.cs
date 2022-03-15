
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.DependencyInjection;

public interface IDesignTimeServiceCollectionConfiguration
{
    IServiceCollection ConfigureService(IServiceCollection services);
}