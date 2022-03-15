namespace Extensions.DependencyInjection.Generators.Tests.Builder;


public abstract record AttributeParameter(string Name, string Value)
{
    public readonly static Lifetime Singleton = new("ServiceLifetime.Singleton");
    public readonly static Lifetime Scoped = new("ServiceLifetime.Scoped");
    public readonly static Lifetime Transient = new("ServiceLifetime.Transient");

    public static ServiceType ServiceType(string serviceType) => new(serviceType);

    public readonly static InstanceMember FromInstance = new();
    public readonly static FactoryMember FromFactory = new();
}

public record Lifetime(string Value) : AttributeParameter("Lifetime", Value)
{
    public override string ToString()
        => $"{Name} = {Value}";
}

public record ServiceType(TypeName TypeName) : AttributeParameter("ServiceType", TypeName.ToString())
{
    public override string ToString()
        => $"{Name} = typeof({Value})";
}

public record InstanceMember() : AttributeParameter("MemberName", "Instance")
{
    public override string ToString()
        => $"{Name} = \"{Value}\"";
}
public record FactoryMember() : AttributeParameter("MemberName", "Factory")
{
    public override string ToString()
        => $"{Name} = \"{Value}\"";
}