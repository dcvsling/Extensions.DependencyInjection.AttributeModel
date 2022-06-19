using Extensions.DependencyInjection.Generators.Tests.Builder;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.DependencyInjection.Generators.Tests;
public partial class DecoratorTests
{
    public static IEnumerable<object[]> AllGenericTypeDecorator = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Interfaces =
                    {
                        new Interface("A")
                    },
                    Classes =
                    {
                        new Class("A")
                        {
                            Interfaces = { "IA" },
                            CustomAttributes = { new CustomAttribute("Singleton") {
                                Parameters =
                                {
                                    Parameter.Singleton,
                                    Parameter.ServiceType("IA")
                                }
                            } }
                        },
                        new Class("DecoratorA")
                        {
                            Interfaces = { "IA" },
                            ConstructorParameter = {
                                Parameter.Create(new TypeName("IA"), "a")
                            },
                            CustomAttributes = {
                                new CustomAttribute("Decorator") {
                                    Parameters =
                                    {
                                        Parameter.ServiceType("IA")
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = { 
                    "services.AddSingleton<IA, A>();"
                },
                Decorator =
                {
                    "services.Decorator<IA, DecoratorA>();"
                },
                Usings = { "A.Models" },
                Namespace = "A"
            }
        }
    };
    public static IEnumerable<object[]> AllGenericOfClosedGenericTypeDecorator = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Interfaces =
                    {
                        new Interface("A<T>")
                    },
                    Classes =
                    {
                        new Class("B") {},
                        new Class("A")
                        {
                            Interfaces = { "IA<B>" },
                            CustomAttributes = { new CustomAttribute("Singleton") {
                                Parameters =
                                {
                                    Parameter.Singleton,
                                    Parameter.ServiceType("IA<B>")
                                }
                            } }
                        },
                        new Class("DecoratorA")
                        {
                            Interfaces = { "IA<B>" },
                            ConstructorParameter = {
                                Parameter.Create(new TypeName("IA<B>"), "a")
                            },
                            CustomAttributes = {
                                new CustomAttribute("Decorator") {
                                    Parameters =
                                    {
                                        Parameter.ServiceType("IA<B>")
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = {
                    "services.AddSingleton<IA<B>, A>();"
                },
                Decorator =
                {
                    "services.Decorator<IA<B>, DecoratorA>();"
                },
                Usings = { "A.Models" },
                Namespace = "A"
            }
        }
    };
    public static IEnumerable<object[]> AllArgumentOfOpenGenericTypeDecorator = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Interfaces =
                    {
                        new Interface("A<T>")
                    },
                    Classes =
                    {
                        new Class("A<T>")
                        {
                            Interfaces = { "IA<T>" },
                            CustomAttributes = { new CustomAttribute("Singleton") {
                                Parameters =
                                {
                                    Parameter.Singleton,
                                    Parameter.ServiceType("IA<>")
                                }
                            } }
                        },
                        new Class("DecoratorA<T>")
                        {
                            Interfaces = { "IA<T>" },
                            ConstructorParameter = {
                                Parameter.Create(new TypeName("IA<T>"), "a")
                            },
                            CustomAttributes = {
                                new CustomAttribute("Decorator") {
                                    Parameters =
                                    {
                                        Parameter.ServiceType("IA<>")
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = {
                    "services.AddSingleton(typeof(IA<>), typeof(A<>));"
                },
                Decorator =
                {
                    "services.Decorator(typeof(IA<>), typeof(DecoratorA<>));"
                },
                Usings = { "A.Models" },
                Namespace = "A"
            }
        }
    };
}
