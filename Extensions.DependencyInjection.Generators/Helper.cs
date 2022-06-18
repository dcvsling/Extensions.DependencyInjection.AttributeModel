using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;
namespace Extensions.DependencyInjection.Generators
{

    internal static class Helper
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> seq, T t)
        {
            foreach(var x in seq)
            {
                yield return x;
            }
            yield return t;
        }
        public static IEnumerable<T> Append<T, U>(this IEnumerable<T> seq, U u)
            where U : T
        {
            foreach (var x in seq)
            {
                yield return x;
            }
            yield return u;
        }
        public static IEnumerable<T> Preppend<T>(this IEnumerable<T> seq, T t)
        {
            yield return t;
            foreach (var x in seq)
            {
                yield return x;
            }
        }
        public static IEnumerable<T> Preppend<T, U>(this IEnumerable<T> seq, U u)
            where U : T
        {
            yield return u;
            foreach (var x in seq)
            {
                yield return x;
            }
        }
        public static bool IsNameMatch(this AttributeSyntax attr, IEnumerable<string> names)
            => names.Contains(attr.Name.ToFullString().Trim());

        public static TParent GetParentNode<TParent>(this SyntaxNode syntax) where TParent : SyntaxNode
            => syntax?.Parent is TParent parent ? parent : syntax.Parent?.GetParentNode<TParent>();

        public static BaseNamespaceDeclarationSyntax GetNamespaceSyntax(this SyntaxNode node)
            => node.Parent is BaseNamespaceDeclarationSyntax @namespace
                ? @namespace
                : node.Parent is CompilationUnitSyntax root
                    ? root.Members.OfType<BaseNamespaceDeclarationSyntax>().FirstOrDefault()
                    : node.Parent.GetNamespaceSyntax();

        
        public static bool HasModifierByKinds(this SyntaxTokenList tokens, params SyntaxKind[] kinds)
            => tokens.Select(x => x.Kind()).Intersect(kinds).Count() == kinds.Length;

        public static MemberDeclarationSyntax GetMemberByName(this ClassDeclarationSyntax syntax, string name)
            => syntax.Members.FirstOrDefault(member => member.GetName() == name);
        public static string GetRegisterName(this AttributeMetadata metadata)
            => string.IsNullOrWhiteSpace(metadata.ServiceType)
                ? metadata.ImplementationType
                : metadata.ServiceType;
        public static string GetName(this PropertyDeclarationSyntax syntax)
            => syntax.Identifier.ToFullString().Trim();

        public static string GetName(this MemberDeclarationSyntax syntax)
            => syntax is PropertyDeclarationSyntax property
                ? property.GetName()
                : syntax is MethodDeclarationSyntax method
                    ? method.Identifier.ToFullString().Trim()
                    : default;

        public static bool IsGenericType(this ClassDeclarationSyntax syntax)
            => (syntax.TypeParameterList?.Parameters.Count ?? 0) > 0;

        public static AttributeArgumentSyntax GetArgumentByName(this AttributeSyntax syntax, string name)
            => syntax.ArgumentList?.Arguments.FirstOrDefault(x => x.NameEquals.Name.Identifier.Text == name);
        public static string UnwrapTypeOf(this string text)
            => text.StartsWith("typeof") ? text.Substring(7, text.Length - 8) : text;

        public static string WrapTypeOf(this string text)
            => text.StartsWith("typeof") ? text : $"typeof({text})";


        public static string UnwrapQuotes(this string text)
            => text.StartsWith("\"") ? text.Substring(1, text.Length - 2) : text;

    }
}