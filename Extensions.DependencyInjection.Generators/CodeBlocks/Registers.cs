using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class Registers : SourceProviderCollectionBase<IRegister>, IRegister
    {
        private readonly IEnumerable<IRegister> _registers;

        public Registers(IEnumerable<IRegister> registers)
        {
            _registers = registers;
        }

        public override string ToString()
            => string.Join(Environment.NewLine + "            ", _registers.Distinct().Select(x => x.ToString()));

        public override IEnumerator<IRegister> GetEnumerator()
            => _registers.GetEnumerator();
    }
}