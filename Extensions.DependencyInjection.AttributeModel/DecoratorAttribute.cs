namespace Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class DecoratorAttribute : Attribute
{
    public virtual Type? ServiceType { get; set; }
    public virtual string? InstanceOrFactory { get; set; }
}