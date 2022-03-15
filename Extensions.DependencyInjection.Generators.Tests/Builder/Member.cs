using System.Diagnostics;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;

[DebuggerDisplay("{ToString()}")]
public abstract record Member();
[DebuggerDisplay("{ToString()}")]
public record Property(TypeName Type, string Name, string Indent, string[] Accessor, params string[] Modifier) : Member()
{
    public static Property CreateInstance(TypeName Type)
        => new(Type, "Instance", Class.INDENT, new[] { "get" }, "public", "static");
    public override string ToString() => $"{Indent}{string.Join(" ", Modifier)} {Type} {Name} {{ {string.Join("; ", Accessor) }; }}";
}

[DebuggerDisplay("{ToString()}")]
public record Method((TypeName type, string name)[] ParameterTypes, TypeName ReturnType, string Name, string Indent, string Expression, params string[] Modifier) : Member()
{
    public static Method CreateFactory(TypeName Type)
        => new(
            new (TypeName, string)[] { ("IServiceProvider", "_") },
            Type.TypeParameters.Count > 0 ? "object" : Type,
            "Factory",
            Class.INDENT,
            "throw new NotImplementedException();",
            "public",
            "static");
    public override string ToString()
       => $@"{Indent}{string.Join(" ", Modifier)} {ReturnType} {Name}({ string.Join(", ", ParameterTypes.Select(x => $"{x.type} {x.name}"))}) 
{{
{Indent}    {Expression}
}}";
}