using Microsoft.CodeAnalysis;

namespace Extensions.DependencyInjection.Generators;

[Generator]
internal class DependencyRegisterGenerator : IIncrementalGenerator
{
    
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                Generators.HasLifetimeAttrbute, 
                Generators.FindAttributeSyntaxs)
            .Where(static m => m is not null);
        var complierWithClass = context.CompilationProvider.Combine(classDeclarations.Collect());
        context.RegisterSourceOutput(complierWithClass, Generators.GenerateOutput);
    }
}
