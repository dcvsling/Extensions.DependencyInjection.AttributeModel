
using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;

public record Class(TypeName Name)
{
    public TypeName? BaseType { get; set; }
    public ICollection<TypeName> Interfaces { get; init; } = new List<TypeName>();
    public ICollection<CustomAttribute> CustomAttributes { get; init; } = new List<CustomAttribute>();
    public ICollection<Member> Members { get; init; } = new List<Member>();
    public ICollection<Parameter> ConstructorParameter = new List<Parameter>();
    public const string INDENT = "    ";
    public override string ToString()
        => $@"
{string.Join(Environment.NewLine, CustomAttributes)}
public class {Name}{(string.Join(", ", BaseType is null ? Interfaces : Interfaces.Prepend(BaseType)) is string extends && !string.IsNullOrWhiteSpace(extends) ? $" : {extends}" : string.Empty)}
{{
{(ConstructorParameter.Count != 0 ? (INDENT + $"public {Name.Name}({string.Join(", ", ConstructorParameter)}) {{ }}") : string.Empty)}
{(Members.Count != 0 ? INDENT + string.Join(Environment.NewLine + INDENT, Members) : string.Empty)}
}}";
}
