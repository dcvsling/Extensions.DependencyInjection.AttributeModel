using Extensions.DependencyInjection.Generators.Tests.Builder;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using SourceGenerators.Tests;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests.Diagnostic;
public partial class NotToDiagnosticTests
{
    [Theory]
    [MemberData(nameof(DIGEN01))]
    public void Generate_DIGEN01(IEnumerable<Module> modules)
        => TestBase(modules);
    //[Theory]
    //[MemberData("DIGEN02")]
    //public void Generate_DIGEN02(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
    //    => TestBase(descs, modules);
    //[Theory]
    //[MemberData("DIGEN03")]
    //public void Generate_DIGEN03(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
    //    => TestBase(descs, modules);
    //[Theory]
    //[MemberData("DIGEN04")]
    //public void Generate_DIGEN04(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
    //    => TestBase(descs, modules);
    private static void TestBase(IEnumerable<Module> modules)
    {
        var compiler = CSharpCompilation.Create("A.dll", modules.Select(x => CSharpSyntaxTree.ParseText(x)));
        var generator = new DependencyRegisterGenerator();
        (var diags, var result) = RoslynTestUtils.RunGenerator(compiler, generator);
        Assert.Empty(diags);
    }

}
