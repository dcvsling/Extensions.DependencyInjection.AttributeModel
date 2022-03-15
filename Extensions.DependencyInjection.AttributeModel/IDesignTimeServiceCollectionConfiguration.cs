
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.DependencyInjection.AttributeModel;

public interface IDesignTimeServiceCollectionConfiguration
{
    IServiceCollection ConfigureService(IServiceCollection services);
}