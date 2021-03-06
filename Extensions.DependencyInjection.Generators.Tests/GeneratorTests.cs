using Extensions.DependencyInjection.Generators.Abstractions;
using Extensions.DependencyInjection.Generators.Tests.Builder;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using SourceGenerators.Tests;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests;
public partial class GeneratorTests
{
    [Theory]
    [MemberData(nameof(Singleton))]
    public void Generate_Singelton(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);

    [Theory]
    [MemberData(nameof(Scoped))]
    public void Generate_Scoped(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData(nameof(Transient))]
    public void Generate_Transient(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData(nameof(WithServiceType))]
    public void Generate_WithServiceType(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData(nameof(OpenGeneric))]
    public void Generate_OpenGeneric(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData(nameof(OpenGenericWithServiceType))]
    public void Generate_OpenGenericWithServiceType(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);

    [Theory]
    [MemberData(nameof(LifetimeAttribute))]
    public void Generate_LifetimeAttribute(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);

    [Theory]
    [MemberData(nameof(ExternalUsingDirectives))]
    public void External_Using_Directives(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);


    private static void TestBase(IEnumerable<Module> modules, StringSourceProvider expect)
    {
        var trees = modules.Select(x => CSharpSyntaxTree.ParseText(x));
        var compiler = CSharpCompilation.Create(expect.Namespace, trees);
        var generator = new DependencyRegisterGenerator();
        (var diags, var result) = RoslynTestUtils.RunGenerator(compiler, generator);
        Assert.Empty(diags);
        Assert.Equal(expect.ToString(), result[0].SourceText.ToString());
        //CompileSource(trees.Concat(result.Select(x => CSharpSyntaxTree.ParseText(x.SourceText))));
    }
}
