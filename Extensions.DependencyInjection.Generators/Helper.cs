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
        public static readonly string[] ATTRIBUTE_NAMES = new[] {
            "Inject",
            "InjectAttribute",
            "Singleton",
            "SingletonAttribute",
            "Scoped",
            "ScopedAttribute",
            "Transient",
            "TransientAttribute",
        };

        public static bool IsInjectAttribute(this string name)
            => ATTRIBUTE_NAMES.Contains(name);


        public static BaseNamespaceDeclarationSyntax GetNamespaceSyntax(this SyntaxNode node)
            => node.Parent is BaseNamespaceDeclarationSyntax @namespace
                ? @namespace
                : node.Parent is CompilationUnitSyntax root
                    ? root.Members.OfType<BaseNamespaceDeclarationSyntax>().FirstOrDefault()
                    : node.Parent.GetNamespaceSyntax();

        public static IEnumerable<AttributeSyntax> GetInjectAttributeSyntax(this ClassDeclarationSyntax classSyntax)
            => classSyntax.AttributeLists.SelectMany(x => x.Attributes)
                .Where(attr => attr.Name.ToFullString().Trim().IsInjectAttribute());

        public static GenerateContext GenerateOutput(this IEnumerable<InjectMetadata> injects, string assemblyName, Action<Diagnostic> reporter)
        {
            var emitter = new Emitter(reporter);
            return injects.Select(emitter.Emit)
                .Where(x => x != null)
                .Aggregate(
                    new GenerateContext() { Namespace = assemblyName },
                    (ctx, content) =>
                    {
                        ctx.Sources.AddRange(content.RegisterContent);
                        ctx.Usings.AddRange(content.UsingContent);
                        return ctx;
                    });
        }
        public static bool HasModifierByKinds(this SyntaxTokenList tokens, params SyntaxKind[] kinds)
               => tokens.Select(x => x.Kind()).Intersect(kinds).Count() == kinds.Length;
        public static IEnumerable<T> Emit<T>(this T t)
        {
            yield return t;
        }
        public static bool IsSingleton(this InjectMetadata metadata)
            => metadata.Lifetime == "Singleton";
        public static bool NotSingleton(this InjectMetadata metadata)
            => metadata.Lifetime != "Singleton";
        public static bool IsScoped(this InjectMetadata metadata)
            => metadata.Lifetime == "Scoped";
        public static bool NotScoped(this InjectMetadata metadata)
            => metadata.Lifetime != "Scoped";
        public static bool IsTransient(this InjectMetadata metadata)
            => metadata.Lifetime == "Transient";
        public static bool NotTransient(this InjectMetadata metadata)
            => metadata.Lifetime != "Transient";
        public static MemberDeclarationSyntax GetMemberByName(this ClassDeclarationSyntax syntax, string name)
            => syntax.Members.FirstOrDefault(member => member.GetName() == name);
        public static string GetRegisterName(this InjectMetadata metadata)
            => (string.IsNullOrWhiteSpace(metadata.ServiceType)
                ? metadata.ImplementationType
                : metadata.ServiceType);
        public static string GetName(this PropertyDeclarationSyntax syntax)
            => syntax.Identifier.ToFullString().Trim();

        public static string GetName(this MemberDeclarationSyntax syntax)
            => syntax is PropertyDeclarationSyntax property
                ? property.GetName()
                : syntax is MethodDeclarationSyntax method
                    ? method.Identifier.ToFullString().Trim()
                    : default;
        public static string GetName(this ClassDeclarationSyntax syntax)
            => syntax.Identifier.ValueText + (syntax.IsGenericType()
                ? $"<{string.Join(string.Empty, Enumerable.Repeat(",", syntax.TypeParameterList.Parameters.Count - 1))}>"
                : string.Empty);
        public static bool IsGenericType(this ClassDeclarationSyntax syntax)
            => (syntax.TypeParameterList?.Parameters.Count ?? 0) > 0;

        public static string GetName(this NamespaceDeclarationSyntax syntax)
            => syntax.Name.ToFullString().Trim();

        public static AttributeArgumentSyntax GetArgumentByName(this AttributeSyntax syntax, string name)
            => syntax.ArgumentList?.Arguments.FirstOrDefault(x => x.NameEquals.Name.Identifier.Text == name);
        public static string UnwrapTypeOf(this string text)
            => text.StartsWith("typeof") ? text.Substring(7, text.Length - 8) : text;

        public static string WrapTypeOf(this string text)
            => text.StartsWith("typeof") ? text : $"typeof({text})";

        public static string UnwrapNameOf(this string text)
            => text.StartsWith("nameof") ? text.Substring(7, text.Length - 8) : text;

        public static string WrapNameOf(this string text)
            => text.StartsWith("nameof") ? text : $"nameof({text})";

        public static string UnwrapQuotes(this string text)
            => text.StartsWith("\"") ? text.Substring(1, text.Length - 2) : text;
        public static string WrapQuotes(this string text)
            => text.StartsWith("\"") ? text : $"\"{text}\"";
    }
}