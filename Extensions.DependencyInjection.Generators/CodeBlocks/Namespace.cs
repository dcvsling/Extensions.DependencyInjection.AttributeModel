using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class Namespace : ISource
    {
        private readonly string _namespace;

        public Namespace(string @namespace)
        {
            _namespace = @namespace;
        }
        public string Render(AttributeMetadata _)
            => $@"namespace {_namespace}"; 
    }
}
