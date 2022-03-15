
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class InjectAttribute : Attribute
{
    public virtual ServiceLifetime Lifetime { get; init; }
    public virtual Type? ServiceType { get; set; }
    public virtual string? MemberName { get; set; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]

public class SingletonAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; init; } = ServiceLifetime.Singleton;
}
public class ScopedAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; init; } = ServiceLifetime.Scoped;
}
public class TransientAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; init; } = ServiceLifetime.Transient;
}
