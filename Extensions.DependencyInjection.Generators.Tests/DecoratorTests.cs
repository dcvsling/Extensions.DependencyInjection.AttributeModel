using Extensions.DependencyInjection.Generators.Tests.Builder;

using Microsoft.CodeAnalysis.CSharp;
using SourceGenerators.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests;
public partial class DecoratorTests
{
    [Theory]
    [MemberData(nameof(AllGenericTypeDecorator))]
    public void GenerateAllGenericTypeDecorator(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData(nameof(AllGenericOfClosedGenericTypeDecorator))]
    public void GenerateAllGenericOfClosedGenericTypeDecorator(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);
    [Theory]
    [MemberData(nameof(AllArgumentOfOpenGenericTypeDecorator))]
    public void GenerateAllArgumentOfOpenGenericTypeDecorator(IEnumerable<Module> modules, StringSourceProvider expect)
        => TestBase(modules, expect);    

    private static void TestBase(IEnumerable<Module> modules, StringSourceProvider expect)
    {
        var trees = modules.Select(x => CSharpSyntaxTree.ParseText(x));
        var compiler = CSharpCompilation.Create(expect.Namespace, trees);
        var generator = new DependencyRegisterGenerator();
        (var diags, var result) = RoslynTestUtils.RunGenerator(compiler, generator);
        Assert.Empty(diags);
        Assert.Equal(expect.ToString(), result[0].SourceText.ToString());
    }
}
