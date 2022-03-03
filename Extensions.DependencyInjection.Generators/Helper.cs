using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators;
internal static class Helper
{
    private static readonly string[] ATTRIBUTE_NAMES = new[] { "Inject", "InjectAttribute" };

    public static bool IsInjectAttribute(this string name)
        => ATTRIBUTE_NAMES.Contains(name);

    public static INamedTypeSymbol? GetSymbol(this Compilation compilation, ClassDeclarationSyntax syntax)
        => compilation.GetSemanticModel(syntax.SyntaxTree).GetDeclaredSymbol(syntax) is INamedTypeSymbol symbol ? symbol : null;
}
