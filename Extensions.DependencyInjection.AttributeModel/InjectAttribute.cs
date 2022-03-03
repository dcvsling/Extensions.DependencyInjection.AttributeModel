using Microsoft.Extensions.DependencyInjection;

namespace Extensions.DependencyInjection.AttributeModel;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class InjectAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; set; }
    public Type? ServiceType { get; set; }
    public string? MemberName { get; set; }
}