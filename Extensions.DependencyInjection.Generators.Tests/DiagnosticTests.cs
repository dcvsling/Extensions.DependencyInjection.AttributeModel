using Extensions.DependencyInjection.Generators.Diagnostics;
using Extensions.DependencyInjection.Generators.Tests.Builder;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using SourceGenerators.Tests;

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests.Diagnostic;
public partial class DiagnosticTests
{
    [Theory]
    [MemberData(nameof(DG001))]
    public void Generate_DG001(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
        => TestBase(descs, modules);
    [Theory]
    [MemberData(nameof(DG002))]
    public void Generate_DG002(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
        => TestBase(descs, modules);
    [Theory]
    [MemberData(nameof(DG003))]
    public void Generate_DG003(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
        => TestBase(descs, modules);
    [Theory]
    [MemberData(nameof(DG004))]
    public void Generate_DG004(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
        => TestBase(descs, modules);

    [Theory]
    [MemberData(nameof(DG005))]
    public void Generate_DG005(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
        => TestBase(descs, modules);


    private static void TestBase(DiagnosticDescriptor[] descs, IEnumerable<Module> modules)
    {
        var compiler = CSharpCompilation.Create("A.dll", modules.Select(x => CSharpSyntaxTree.ParseText(x)));
        var generator = new DependencyRegisterGenerator();
        (var diags, var result) = RoslynTestUtils.RunGenerator(compiler, generator);
        Assert.NotEmpty(diags);
        
        Assert.Collection(
            diags.Select(x => x.Descriptor).Zip(descs, (actual, expect) => (actual, expect)),
            data => Assert.Equal(data.expect.Id, data.actual.Id));
    }

}
