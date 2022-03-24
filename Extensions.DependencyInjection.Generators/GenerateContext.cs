using Microsoft.CodeAnalysis.Text;

using System.Collections.Generic;
using System.Text;

namespace Extensions.DependencyInjection.Generators
{
    public class GenerateContext
    {
        public string HintName { get; set; } = "ServiceRegistry.g.cs";
        public string Namespace { get; set; } = "Extensions.DependencyInjection.Generators";
        public List<string> Usings { get; set; } = new List<string>();
        public List<string> Sources { get; set; } = new List<string>();
        public static implicit operator SourceText(GenerateContext context)
            => SourceText.From(context.ToString(), Encoding.UTF8);
        public void Deconstruct(out string hintName, out SourceText source)
        {
            hintName = HintName;
            source = this;
        }
        public override string ToString()
            => GeneratedCodes.CreateRegistryAttribute(this).ToString();
    }
}