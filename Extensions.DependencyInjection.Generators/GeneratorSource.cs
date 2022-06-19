using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators
{
    internal class GeneratorSource
    {
        public IEnumerable<ISource> Using { get; set; }
        public ISource Register { get; set; }
    }
}