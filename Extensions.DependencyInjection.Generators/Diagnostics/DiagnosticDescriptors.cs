using Microsoft.CodeAnalysis;

namespace Extensions.DependencyInjection.Generators
{
    public static class DiagnosticDescriptors
    {

        public static DiagnosticDescriptor DIGEN00 = new DiagnosticDescriptor(
            id: "DIGEN00",
            title: "Internal Error",
            messageFormat: "there must be something wrong, pleace report follow erro message: {0}",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DIGEN01 = new DiagnosticDescriptor(
            id: "DIGEN01",
            title: "missing member",
            messageFormat: "must declare an static member named {0}",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DIGEN02 = new DiagnosticDescriptor(
            id: "DIGEN02",
            title: "wrong member return type",
            messageFormat: "{0} return type should be {1}",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DIGEN03 = new DiagnosticDescriptor(
            id: "DIGEN03",
            title: "wrong member type",
            messageFormat: "member should be static property when lifetime is Singleton",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor DIGEN04 = new DiagnosticDescriptor(
            id: "DIGEN04",
            title: "wrong member type",
            messageFormat: "member should be static method with single parameter is IServiceProvider",
            category: "DIGenerator",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);
    }
}
