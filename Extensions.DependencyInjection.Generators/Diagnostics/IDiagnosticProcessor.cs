namespace Extensions.DependencyInjection.Generators
{
    public interface IDiagnosticProcessor
    {
        DiagnosticResult Diagnostic(AttributeMetadata metadata);
    }
}
