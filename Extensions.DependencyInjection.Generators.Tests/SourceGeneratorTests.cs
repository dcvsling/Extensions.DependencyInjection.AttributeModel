using Extensions.DependencyInjection.Generators.Tests.Builder;

using Microsoft.CodeAnalysis.CSharp;

using SourceGenerators.Tests;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests;
public partial class SourceGeneratorTests
{
    [Theory]
    [MemberData("Singleton")]
    public void Generate_Singelton(IEnumerable<Module> modules, GenerateContext expect)
        => TestBase(modules, expect);

    [Theory]
    [MemberData("Scoped")]
    public void Generate_Scoped(IEnumerable<Module> modules, GenerateContext expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData("Transient")]
    public void Generate_Transient(IEnumerable<Module> modules, GenerateContext expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData("WithServiceType")]
    public void Generate_WithServiceType(IEnumerable<Module> modules, GenerateContext expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData("OpenGeneric")]
    public void Generate_OpenGeneric(IEnumerable<Module> modules, GenerateContext expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData("OpenGenericWithServiceType")]
    public void Generate_OpenGenericWithServiceType(IEnumerable<Module> modules, GenerateContext expect)
        => TestBase(modules, expect);

    private static void TestBase(IEnumerable<Module> modules, GenerateContext expect)
    {
        var compiler = CSharpCompilation.Create(expect.Namespace, modules.Select(x => CSharpSyntaxTree.ParseText(x)));
        var generator = new DependencyRegisterGenerator();
        (var diags, var result) = RoslynTestUtils.RunGenerator(compiler, generator);
        Assert.Empty(diags);
        Assert.Equal(expect.ToString(), result[0].SourceText.ToString());
    }
}