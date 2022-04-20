using Microsoft.CodeAnalysis;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Extensions.DependencyInjection.Generators.Tests")]

namespace Extensions.DependencyInjection.Generators
{
    [Generator]
    public partial class DependencyRegisterGenerator : IIncrementalGenerator
    {

        public void Initialize(IncrementalGeneratorInitializationContext context)
            => context.Initialize(new DependencyInjectionRegisterHandler());
    }
}



