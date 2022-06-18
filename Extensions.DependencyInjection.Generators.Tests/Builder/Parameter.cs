namespace Extensions.DependencyInjection.Generators.Tests.Builder;


public abstract record Parameter(string Name, string Value)
{
    public readonly static Lifetime Singleton = new("ServiceLifetime.Singleton");
    public readonly static Lifetime Scoped = new("ServiceLifetime.Scoped");
    public readonly static Lifetime Transient = new("ServiceLifetime.Transient");

    public static ServiceType ServiceType(string serviceType) => new(serviceType);
    public static MethodParameter Create(TypeName type, string name) => new(type, name);
    public readonly static InstanceMember FromInstance = new();
    public readonly static FactoryMember FromFactory = new();
}

public record MethodParameter(TypeName type, string name) : Parameter(type.ToString(), name)
{
    public override string ToString()
        => $"{type} {name}";
}

public record Lifetime(string Value) : Parameter(nameof(AttributeMetadata.Lifetime), Value)
{
    public override string ToString()
        => $"{Name} = {Value}";
}

public record ServiceType(TypeName TypeName) : Parameter(nameof(AttributeMetadata.ServiceType), TypeName.ToString())
{
    public override string ToString()
        => $"{Name} = typeof({Value})";
}

public record InstanceMember() : Parameter(nameof(AttributeMetadata.InstanceOrFactory), "Instance")
{
    public override string ToString()
        => $"{Name} = \"{Value}\"";
}
public record FactoryMember() : Parameter(nameof(AttributeMetadata.InstanceOrFactory), "Factory")
{
    public override string ToString()
        => $"{Name} = \"{Value}\"";
}