using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators
{
    internal class GeneratorSource
    {
        public GeneratorSource(IEnumerable<IUsing> @using, IRegister register)
        {
            Using = @using;
            Register = register;
        }

        public IEnumerable<IUsing> Using { get; }
        public IRegister Register { get; }
    }
}