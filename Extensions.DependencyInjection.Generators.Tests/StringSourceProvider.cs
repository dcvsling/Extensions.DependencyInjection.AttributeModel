using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators.Tests;
public class StringSourceProvider
{
    public ICollection<string> Usings { get; } = new List<string>();
    public string GlobalAttribute => $"{Namespace}.{Constant.DEFAULT_REGISTER_ATTRIBUTE_NAME}";
    public string Namespace { get; set; } = "Extensions.DependencyInjection.AttributeModel";
    public ICollection<string> Register { get; } = new List<string>();
    public override string ToString()
        => new ServiceRegisterSourceProvider
        {
            Usings = new Usings(Usings.Select(x => new UsingFromString(x))),
            GlobalAttribute = new GlobalAttribute(GlobalAttribute),
            Namespace = new Namespace(Namespace),
            Register = new Registers(Register.Select(x => new StringRegister(x)))
        }.ToString();
}

internal class StringRegister : IRegister
{
    private readonly string _sourceText;

    public StringRegister(string sourceText)
    {
        _sourceText = sourceText;
    }
    public override string ToString()
        => _sourceText;
}