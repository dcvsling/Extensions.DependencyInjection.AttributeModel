using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests;
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var syntax = CSharpSyntaxTree.ParseText(@"
namespace Extensions.DependencyInjection.Generators;
public class Test { }");
        var visitor = new FindClassVisitor();
        visitor.Visit(syntax.GetRoot());
        var symbol = CSharpCompilation.Create("mytest").GetBestTypeByMetadataName("Extensions.DependencyInjection.Generators.Test");
        Assert.NotNull(symbol);
        
    }
}

public class FindClassVisitor : CSharpSyntaxVisitor
{
    public List<ClassDeclarationSyntax> Syntaxs = new List<ClassDeclarationSyntax>();

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        base.VisitClassDeclaration(node);
        Syntaxs.Add(node);
    }

}