namespace Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]

public class SingletonAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Singleton;
}
