using Extensions.DependencyInjection.Generators.Abstractions;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class ServiceRegisterSourceProvider : ISourceProvider
    {

        
        public ISourceProvider Usings { get; set; }
        public ISourceProvider GlobalAttribute { get; set; }
        public ISourceProvider Namespace { get; set; }
        public ISourceProvider Register { get; set; }


        public override string ToString()
            => $@"
{Usings}

{GlobalAttribute}

{Namespace} 
{{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public class {Constant.DEFAULT_REGISTER_ATTRIBUTE_NAME}Attribute : Attribute, IDesignTimeServiceCollectionConfiguration
    {{
        public IServiceCollection ConfigureService(IServiceCollection services)
        {{
            {Register}
            return services;
        }}
    }}
}}";
    }
}
