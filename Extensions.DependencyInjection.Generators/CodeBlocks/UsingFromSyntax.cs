using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Extensions.DependencyInjection.Generators.CodeBlocks
{
    internal class UsingFromSyntax : SourceProviderBase, IUsing
    {
        private readonly UsingDirectiveSyntax _syntax;

        public UsingFromSyntax(UsingDirectiveSyntax syntax)
        {
            _syntax = syntax;
        }
        public override string ToString()
            => $@"using {_syntax.Name};";
    }
}