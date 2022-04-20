using System.Linq;

using Xunit;

namespace Microsoft.Extensions.DependencyInjection.Tests;
public class AttributeModelServiceCollectionTests
{
    [Fact]
    public void Invoke_AddAttributeModelRegister_twice_should_register_once()
    {
        var collection = new ServiceCollection()
            .AddAttributeModelRegister()
            .AddAttributeModelRegister();

        Assert.Single(collection.Where(x => x.ServiceType == typeof(TestService)));
    }
}

[Singleton]
public class TestService { }
