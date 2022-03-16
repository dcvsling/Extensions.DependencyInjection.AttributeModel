using Microsoft.CodeAnalysis;

namespace Extensions.DependencyInjection.Generators
{
    public static class DiagnosticDescriptors
    {

        public static DiagnosticDescriptor DG000 = new DiagnosticDescriptor(
            id: "DG000",
            title: "Internal Error",
            messageFormat: "there must be something wrong, pleace report follow erro message: {0}",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DG001 = new DiagnosticDescriptor(
            id: "DG001",
            title: "missing member",
            messageFormat: "must declare an static member named {0}",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DG002 = new DiagnosticDescriptor(
            id: "DG002",
            title: "wrong member return type",
            messageFormat: "{0} return type should be {1}",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DG003 = new DiagnosticDescriptor(
            id: "DG003",
            title: "member should be static property",
            messageFormat: "member should be static property when lifetime is Singleton",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DG004 = new DiagnosticDescriptor(
            id: "DG004",
            title: "member should be static method with argument is IServiceProvider",
            messageFormat: "member should be static method with single argument is IServiceProvider",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
    }
}
