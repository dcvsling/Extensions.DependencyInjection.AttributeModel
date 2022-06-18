using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class ServiceRegisterSourceProvider : ISource
    {
        public string AttributeName { get; set; }
        public string InterfaceName { get; set; }
        public ISource Usings { get; set; }
        public ISource GlobalAttribute { get; set; }
        public ISource Namespace { get; set; }
        public ISource Register { get; set; }
        public ISource Decorator { get; set; }

        public override string ToString()
            => $@"
{Usings}

{GlobalAttribute}

{Namespace} 
{{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class {AttributeName}Attribute : Attribute, {InterfaceName}
    {{
        public IServiceCollection ConfigureService(IServiceCollection services)
        {{
            {Register}
            return services;
        }}

        public IServiceCollection ConfigureDecorator(IServiceCollection services)
        {{
            {Decorator}
            return services;
        }}
    }}
}}";
    }
}
