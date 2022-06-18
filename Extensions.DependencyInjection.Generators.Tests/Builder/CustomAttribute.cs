using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;

public record CustomAttribute(TypeName Name)
{
    public ICollection<Parameter> Parameters { get; init; } = new List<Parameter>();
    public override string ToString()
        => $"[{Name}{(Parameters.Any() ? $"({ string.Join(", ", Parameters.Select(x => x.ToString())) })" : string.Empty) }]";
}
