using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class Namespace : ISourceProvider
    {
        private readonly string _namespace;

        public Namespace(string @namespace)
        {
            _namespace = @namespace;
        }
        public override string ToString()
            => $@"namespace {_namespace}"; 
    }
}
