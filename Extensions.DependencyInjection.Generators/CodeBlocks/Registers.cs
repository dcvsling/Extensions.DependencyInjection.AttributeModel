using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class Registers : SourceProviderCollectionBase<ISource>, ISource
    {
        private readonly IEnumerable<ISource> _registers;

        public Registers(IEnumerable<ISource> registers)
        {
            _registers = registers;
        }

        public override string ToString()
            => string.Join(Environment.NewLine + "            ", _registers.Distinct());

        public override IEnumerator<ISource> GetEnumerator()
            => _registers.GetEnumerator();
    }
}