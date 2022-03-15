using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;

public record TypeName(string Name)
{
    public ICollection<TypeName> TypeParameters { get; init; } = new List<TypeName>();
    public override string ToString()
        => $"{Name}{ (TypeParameters.Any() ? $"<{string.Join(", ", TypeParameters.Select(b => b.ToString()))}>" : string.Empty) }";
    public static implicit operator TypeName(string name) => new(name);
}