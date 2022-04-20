namespace Microsoft.Extensions.DependencyInjection;

public class ScopedAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
}
