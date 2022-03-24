using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators
{
    public class DependencyContent
    {
        public DependencyContent(IEnumerable<string> UsingContent, string register) : this(UsingContent, register.Emit()) { }
        public DependencyContent(string UsingContent, IEnumerable<string> RegisterContent) : this(UsingContent.Emit(), RegisterContent) { }
        public DependencyContent(string @using, string register) : this(@using.Emit(), register.Emit()) { }

        public DependencyContent(IEnumerable<string> UsingContent, IEnumerable<string> RegisterContent)
        {
            this.UsingContent = UsingContent;
            this.RegisterContent = RegisterContent;
        }

        public IEnumerable<string> UsingContent { get; }
        public IEnumerable<string> RegisterContent { get; }
    }
}