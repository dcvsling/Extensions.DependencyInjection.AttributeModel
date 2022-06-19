using Extensions.DependencyInjection.Generators.CodeBlocks;

namespace Extensions.DependencyInjection.Generators.Tests;

internal class StringRegister : IRegister
{
    private readonly string _sourceText;

    public StringRegister(string sourceText)
    {
        _sourceText = sourceText;
    }

    public string Render(AttributeMetadata _)
        => _sourceText;
}
