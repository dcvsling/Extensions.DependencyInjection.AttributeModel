using Extensions.DependencyInjection.Generators.Abstractions;

using System;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators.Render
{
    public interface ISourceProvider
    {
        ISource Get(AttributeMetadata metadata);
    }
}
