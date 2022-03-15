using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;

public record Module(string Name, string Namespace)
{
    public ICollection<string> Usings { get; init; } = new List<string> {
        "System",
        "Extensions.DependencyInjection",
        "Microsoft.Extensions.DependencyInjection"
    };
    public ICollection<Interface> Interfaces { get; init; } = new List<Interface>();
    public ICollection<Class> Classes { get; init; } = new List<Class>();

    public override string ToString()
        => $@"
{string.Join(Environment.NewLine, Usings.Select(nm => $"using {nm};"))}

namespace {Namespace};
{string.Join(Environment.NewLine + Environment.NewLine, Interfaces)}

{string.Join(Environment.NewLine + Environment.NewLine, Classes)}";

    public static implicit operator SourceText(Module module)
        => SourceText.From(module.ToString(), Encoding.UTF8);
}

