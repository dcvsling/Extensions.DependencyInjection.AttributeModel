using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public static class Diagnostics
    {
        public static IDiagnosticProcessor Processor
            = new DefaultDiagnosticProcessor(new IDiagnosticHandler[]
            {
                new DG001(),
                new DG002(),
                new DG003(),
                new DG004(),
                new DG005()
            });
    }
}
