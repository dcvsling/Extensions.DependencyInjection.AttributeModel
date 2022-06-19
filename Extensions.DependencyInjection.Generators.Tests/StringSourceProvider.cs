using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using Microsoft.CodeAnalysis;

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
    public ICollection<string> Decorator { get; } = new List<string>();
    //public override string ToString()
    //    => new ServiceRegisterSourceProvider
    //    {
    //        AttributeName = Constant.DEFAULT_REGISTER_ATTRIBUTE_NAME,
    //        InterfaceName = Constant.INJECT_ATTRIBUTE_INTERFACE_NAME,
    //        Usings = new Usings(Usings.Select(x => new Using(x))),
    //        GlobalAttribute = new GlobalAttribute(GlobalAttribute),
    //        Namespace = new Namespace(Namespace),
    //        Register = new Registers(Register.Select(x => new StringRegister(x))),
    //        Decorator = new Registers(Decorator.Select(x => new StringRegister(x)))
    //    }.Render(metadata);
    public override string ToString()
        => $@"
{new Usings(Usings.Select(x => new Using(x)))}

{new GlobalAttribute(GlobalAttribute)}

{new Namespace(Namespace)} 
{{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class {Constant.DEFAULT_REGISTER_ATTRIBUTE_NAME}Attribute : Attribute, {Constant.INJECT_ATTRIBUTE_INTERFACE_NAME}
    {{
        public IServiceCollection ConfigureService(IServiceCollection services)
        {{
            {string.Join(Environment.NewLine + "            ", Register)}
            return services;
        }}

        public IServiceCollection ConfigureDecorator(IServiceCollection services)
        {{
            {string.Join(Environment.NewLine + "            ", Decorator)}
            return services;
        }}
    }}
}}";
}
