using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections;
using System.Linq;
namespace Extensions.DependencyInjection.Generators
{


    public class AttributeMetadata
    {
        public string Lifetime { get; set; }
        public string ServiceType { get; set; }
        public string InstanceOrFactory { get; set; }
        public string ImplementationType { get; set; }
        public string DecoratedType { get; set; }
        public string Namespace { get; set; }
        public AttributeSyntax AttributeSyntax { get; set; }
        public ClassDeclarationSyntax ClassSyntax { get; set; }
    }
}

