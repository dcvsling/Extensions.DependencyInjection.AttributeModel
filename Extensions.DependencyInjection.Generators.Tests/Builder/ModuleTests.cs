
using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;
public class ModuleTests
{
    [Fact]
    public void TestCaseBuilder_Test()
        => Assert.Equal(_generatedModule, _module.ToString());

    private readonly static Module _module = new("Model1.cs", "MyLib.Models.Model1")
    {
        Classes = {
                new("UnitTest1")
                {
                    CustomAttributes =
                    {
                        new ("Inject") {
                            Parameters = {
                                AttributeParameter.Singleton
                            }
                        }
                    }
                }
            }
    };

    private const string _generatedModule = @"
using System;
using Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MyLib.Models.Model1;



[Inject(Lifetime = ServiceLifetime.Singleton)]
public class UnitTest1
{

}";
}
