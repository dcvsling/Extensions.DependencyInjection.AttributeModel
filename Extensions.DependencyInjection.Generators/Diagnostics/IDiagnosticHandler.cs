namespace Extensions.DependencyInjection.Generators.Diagnostics
{
    public interface IDiagnosticHandler
    {
        ValidateResult Valid(AttributeMetadata metadata);
    }
}
