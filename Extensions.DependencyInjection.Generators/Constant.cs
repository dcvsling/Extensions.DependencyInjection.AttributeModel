namespace Extensions.DependencyInjection.Generators
{
    internal static class Constant
    {
        public const string DEFAULT_HINT_NAME = "ServiceRegistry.g.cs";
        public const string DEFAULT_NAMESPACE = "Extensions.DependencyInjection.Generators";
        public const string DEFAULT_REGISTER_ATTRIBUTE_NAME = "ServiceRegistry";
        public static readonly string[] DEFAULT_USINGS = new[]
        {
            "System",
            "System.Collections.Generic",
            "System.Linq",
            "System.Reflection",
            "Microsoft.Extensions.DependencyInjection"
        };
        public static readonly string[] ATTRIBUTE_NAMES = new[] {
            "Inject",
            "InjectAttribute",
            "Singleton",
            "SingletonAttribute",
            "Scoped",
            "ScopedAttribute",
            "Transient",
            "TransientAttribute",
            "Decorator",
            "DecoratorAttribute"
        };
    }
}