namespace Microsoft.Extensions.DependencyInjection;

public class TransientAttribute : InjectAttribute
{
    public override ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;
}
