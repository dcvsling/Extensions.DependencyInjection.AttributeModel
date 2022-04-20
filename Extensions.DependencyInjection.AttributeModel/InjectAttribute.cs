namespace Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class InjectAttribute : Attribute
{
    public virtual ServiceLifetime Lifetime { get; set; }
    public virtual Type? ServiceType { get; set; }
    public virtual string? MemberName { get; set; }
}

public class DecoratorAttribute : InjectAttribute
{
    
}