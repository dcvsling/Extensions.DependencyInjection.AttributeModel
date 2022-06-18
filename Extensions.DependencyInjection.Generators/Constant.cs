namespace Extensions.DependencyInjection.Generators
{
    internal static class Constant
    {
        public const string DEFAULT_HINT_NAME = "ServiceRegistry.g.cs";
        public const string DEFAULT_NAMESPACE = "Extensions.DependencyInjection.Generators";
        public const string DEFAULT_REGISTER_ATTRIBUTE_NAME = "ServiceRegistry";
        public const string DECORATOR_REGISTER_ATTRIBUTE_NAME = "ServiceRegistryDecorator";        
        public const string INJECT_ATTRIBUTE_INTERFACE_NAME = "IDesignTimeServiceCollectionConfiguration";
        public const string DECORATOR_INJECT_ATTRIBUTE_INTERFACE_NAME = "IDesignTimeServiceCollectionDecoratorConfiguration";
        public static readonly string[] DEFAULT_USINGS = new[]
        {
            "System",
            "System.Collections.Generic",
            "System.Linq",
            "System.Reflection",
            "Microsoft.Extensions.DependencyInjection",
            "Scrutor"
        };
        public static readonly string[] INJECT_ATTRIBUTE_NAMES = new[] {
            "Inject",
            "InjectAttribute",
            "Singleton",
            "SingletonAttribute",
            "Scoped",
            "ScopedAttribute",
            "Transient",
            "TransientAttribute"
        };
        public static readonly string[] DECORATOR_ATTRIBUTE_NAMES = new[] {
            "Decorator",
            "DecoratorAttribute"
        };
    }
}