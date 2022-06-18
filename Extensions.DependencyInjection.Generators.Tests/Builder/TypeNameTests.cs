using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Sdk;

namespace Extensions.DependencyInjection.Generators.Tests.Builder;
public class TypeNameTests
{
    [Theory]
    [MemberData("Data")]
    public void TestNameToTypeName(string name, TypeName typeName) 
        => Assert.Equal(typeName.ToString(), ((TypeName)name).ToString());

    [Theory]
    [MemberData("Data")]
    public void TestTypeNameToName(string name, TypeName typeName)
        => Assert.Equal(name, typeName);


    public static IEnumerable<object[]> Data()
    {
        yield return new object[] { "IA", new TypeName { Name = "IA" } };
        yield return new object[] { "A<T>", new TypeName { Name = "A", TypeParameters = { new TypeName { Name = "T" } } } };
        yield return new object[] { "A<>", new TypeName { Name="A", TypeParameters = { new TypeName { Name = string.Empty } } } };
        yield return new object[] { 
            "A<,,>", 
            new TypeName { Name = "A", TypeParameters = { 
                new TypeName { Name = string.Empty },
                new TypeName { Name = string.Empty },
                new TypeName { Name = string.Empty }
                }
            } 
        };
        yield return new object[] { 
            "A<B,C>",
            new TypeName
            {
                Name = "A",
                TypeParameters =
                {
                    new TypeName { Name = "B" },
                    new TypeName { Name = "C" }
                }
            }    
        };
        yield return new object[] { 
            "A<B<T>,C<R<T>>,D>",
            new TypeName
            {
                Name = "A",
                TypeParameters =
                {
                    new TypeName { 
                        Name = "B",
                        TypeParameters = { 
                            new TypeName { Name = "T" } 
                        } 
                    },
                    new TypeName {
                        Name = "C",
                        TypeParameters = {
                            new TypeName { 
                                Name = "R",
                                TypeParameters = { new TypeName { Name = "T" } }
                            }
                        }
                    },
                    new TypeName {
                        Name = "D"
                    },
                }
            }
        };
    }
}
