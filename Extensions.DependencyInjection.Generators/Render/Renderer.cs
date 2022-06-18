using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.CodeBlocks;

using System;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators.Render
{
    public static class Renderer
    {
        public static ISourceProvider Instance
            => new SourceProvider(
                new ISourceDecision[]
                {
                    new DecoratorSourceDecision(),
                    new InstanceOrFactorySourceDecision(),
                    new OpenGenericSourceDecision(),
                    new NoServiceTypeSourceDecision()
                },
                new Type[]
                {
                    typeof(AllGenericDecorator),
                    typeof(AllArgumentDecorator),
                    typeof(AllArgumentOfTypeRegister),
                    typeof(AllGenericTypeRegister),
                    typeof(GenericWithInstanceOrFactoryDecorator),
                    typeof(ArgumentOfImplementationTypeOnlyRegister),
                    typeof(GenericImplementationTypeOnlyRegister),
                    typeof(GenericServiceTypeWithInstanceOrFactoryRegoster),
                    typeof(ImplementationInstanceOnlyRegister)
                });
    }
}
