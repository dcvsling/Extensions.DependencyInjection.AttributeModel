using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;
namespace Extensions.DependencyInjection.Generators
{


    public class InjectMetadata
    {
        private string _lifetime;
        private string _serviceType;
        private string _memberName;
        private string _implementationType;
        private string _namespace;

        public InjectMetadata(AttributeSyntax AttributeSyntax, ClassDeclarationSyntax ClassSyntax)
        {
            this.AttributeSyntax = AttributeSyntax;
            this.ClassSyntax = ClassSyntax;
        }
        public string Lifetime
            => _lifetime = _lifetime ?? AttributeSyntax.GetArgumentByName(nameof(Lifetime)).Expression.ToFullString().Trim().Split(new string[] { "ServiceLifetime." }, System.StringSplitOptions.RemoveEmptyEntries).Last();
        public string ServiceType
            => _serviceType = _serviceType ?? AttributeSyntax.GetArgumentByName(nameof(ServiceType))?.Expression.ToFullString().Trim().UnwrapTypeOf() ?? string.Empty;
        public string MemberName
            => _memberName = _memberName ?? AttributeSyntax.GetArgumentByName(nameof(MemberName))?.Expression.ToFullString().Trim().Trim('"') ?? string.Empty;
        public string ImplementationType
            => _implementationType = _implementationType ?? ClassSyntax.Identifier.ValueText + (IsGenericType ? $"<{string.Join(string.Empty, Enumerable.Repeat(",", ClassSyntax.TypeParameterList.Parameters.Count - 1))}>" : string.Empty);
        public string Namespace
            => _namespace = _namespace ?? ClassSyntax.GetNamespaceSyntax().Name.ToFullString().Trim();
        public bool IsGenericType
            => ClassSyntax.IsGenericType();

        public AttributeSyntax AttributeSyntax { get; }
        public ClassDeclarationSyntax ClassSyntax { get; }
    }
}

