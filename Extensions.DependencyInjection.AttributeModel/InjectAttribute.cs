
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.DependencyInjection.AttributeModel;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class InjectAttribute : Attribute
{
    public virtual ServiceLifetime Lifetime { get; set; }
    public virtual Type? ServiceType { get; set; }
    public virtual string? MemberName { get; set; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]

public class SingletonAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Singleton;
}
public class ScopedAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
}
public class TransientAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;
}
