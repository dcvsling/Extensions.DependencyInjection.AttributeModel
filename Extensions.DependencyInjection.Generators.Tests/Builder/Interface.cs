namespace Extensions.DependencyInjection.Generators.Tests.Builder;

public record Interface(TypeName Name)
{
    public override string ToString()
        => $@"
public interface I{Name} 
{{
}}
";
}