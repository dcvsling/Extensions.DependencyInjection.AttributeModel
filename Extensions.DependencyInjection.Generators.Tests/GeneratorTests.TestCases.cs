using Extensions.DependencyInjection.Generators.Tests.Builder;

using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators.Tests;


public partial class GeneratorTests
{
    public readonly static IEnumerable<object[]> Singleton = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Classes =
                    {
                        new Class("A")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = { AttributeParameter.Singleton } } }
                        }
                    }
                }
            },
            new GenerateContext {
                Sources = { "services.AddSingleton<A>();" },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
        }
    };

    public readonly static IEnumerable<object[]> Scoped = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Classes =
                    {
                        new Class("A")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = { AttributeParameter.Scoped } } }
                        }
                    }
                }
            },
            new GenerateContext {
                Sources = { "services.AddScoped<A>();" },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
        }
    };

    public readonly static IEnumerable<object[]> Transient = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Classes =
                    {
                        new Class("A")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = { AttributeParameter.Transient } } }
                        }
                    }
                }
            },
            new GenerateContext {
                Sources = { "services.AddTransient<A>();" },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
        }
    };

    public readonly static IEnumerable<object[]> WithServiceType = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Interfaces =
                    {
                        new Interface("A"),
                        new Interface("B"),
                        new Interface("C")
                    },
                    Classes =
                    {
                        new Class("A")
                        {
                            Interfaces = { "IA" },
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    AttributeParameter.Singleton,
                                    AttributeParameter.ServiceType("IA"),
                                } } }
                        },
                        new Class("B")
                        {
                            Interfaces = { "IB" },
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    AttributeParameter.Scoped,
                                    AttributeParameter.ServiceType("IB"),
                                } } }
                        },
                        new Class("C")
                        {
                            Interfaces = { "IC" },
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    AttributeParameter.Transient,
                                    AttributeParameter.ServiceType("IC"),
                                } } }
                        }
                    }
                }
            },
            new GenerateContext {
                Sources = {
                    "services.AddSingleton<IA, A>();",
                    "services.AddScoped<IB, B>();",
                    "services.AddTransient<IC, C>();",
                },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
        }
    };

    public readonly static IEnumerable<object[]> OpenGeneric = new[] {
        new object[]
        {
            new Module[]
            {
                new Module("a.cs", "A.Models")
        {
            Classes =
                    {
                new Class(new TypeName("A") { TypeParameters = { "T" } })
                {
                    CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        AttributeParameter.Singleton,
                                    } } }
                },
                        new Class(new TypeName("B") { TypeParameters = { "T", "T2" } })
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        AttributeParameter.Scoped,
                                    } } }
                        },
                        new Class(new TypeName("C") { TypeParameters = { "T", "T2", "T3" } })
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        AttributeParameter.Transient,
                                    } } }
                        }
                    }
                }
            },
        new GenerateContext {
                Sources = {
                    "services.AddSingleton(typeof(A<>));",
                    "services.AddScoped(typeof(B<,>));",
                    "services.AddTransient(typeof(C<,,>));",
                },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
    }
};

    public readonly static IEnumerable<object[]> OpenGenericWithServiceType = new[] {
    new object[]
    {
        new Module[]
        {
            new Module("a.cs", "A.Models")
            {
                Interfaces =
                {
                    new Interface(new TypeName("A") { TypeParameters = { "T" } }),
                    new Interface(new TypeName("B") { TypeParameters = { "T", "T2" } }),
                    new Interface(new TypeName("C") { TypeParameters = { "T", "T2", "T3" } })
                },
                Classes =
                {
                    new Class(new TypeName("A") { TypeParameters = { "T" } })
                    {
                        Interfaces = { "IA<T>" },
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    AttributeParameter.Singleton,
                                    AttributeParameter.ServiceType("IA<>")
                                } } }
                    },
                    new Class(new TypeName("B") { TypeParameters = { "T", "T2" } })
                    {
                        Interfaces = { "IB<T, T2>" },
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    AttributeParameter.Scoped,
                                    AttributeParameter.ServiceType("IB<,>")
                                } } }
                    },
                    new Class(new TypeName("C") { TypeParameters = { "T", "T2", "T3" } })
                    {
                        Interfaces = { "IC<T, T2, T3>" },
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    AttributeParameter.Transient,
                                    AttributeParameter.ServiceType("IC<,,>")
                                } } }
                    }
                }
            }
        },
        new GenerateContext {
                Sources = {
                    "services.AddSingleton(typeof(IA<>), typeof(A<>));",
                    "services.AddScoped(typeof(IB<,>), typeof(B<,>));",
                    "services.AddTransient(typeof(IC<,,>), typeof(C<,,>));",
                },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
        }
    };

    public readonly static IEnumerable<object[]> WithInstanceOrFactory = new[] {
        new object[]
        {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Interfaces = {  new Interface("B") },
                    Classes =
                    {
                        new Class("A")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        AttributeParameter.Singleton,
                                        AttributeParameter.FromInstance,
                                    } } },
                            Members = { Property.CreateInstance("A") }
                        },
                        new Class("B")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        AttributeParameter.Scoped,
                                        AttributeParameter.ServiceType("IB"),
                                        AttributeParameter.FromFactory,
                                    } } },
                            Interfaces = { "IB" },
                            Members = { Method.CreateFactory("IB") }
                        },
                        new Class("C")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        AttributeParameter.Transient,
                                        AttributeParameter.FromFactory,
                                    } } },
                            Members = { Method.CreateFactory("C") }
                        }
                    }
                }
            },
            new GenerateContext {
                Sources = {
                    "services.AddSingleton(A.Instance);",
                    "services.AddScoped<IB>(B.Factory);",
                    "services.AddTransient(C.Factory);",
                },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
        }
    };

    public readonly static IEnumerable<object[]> LifetimeAttribute = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Interfaces =
                    {
                        new Interface("A"),
                        new Interface("B"),
                        new Interface("C")
                    },
                    Classes =
                    {
                        new Class("A")
                        {
                            Interfaces = { "IA" },
                            CustomAttributes = { new CustomAttribute("Singleton") { Parameters = {
                                    AttributeParameter.ServiceType("IA"),
                                } } }
                        },
                        new Class("B")
                        {
                            Interfaces = { "IB" },
                            CustomAttributes = { new CustomAttribute("Scoped") { Parameters = {
                                    AttributeParameter.ServiceType("IB"),
                                } } }
                        },
                        new Class("C")
                        {
                            Interfaces = { "IC" },
                            CustomAttributes = { new CustomAttribute("Transient") { Parameters = {
                                    AttributeParameter.ServiceType("IC"),
                                } } }
                        }
                    }
                }
            },
            new GenerateContext {
                Sources = {
                    "services.AddSingleton<IA, A>();",
                    "services.AddScoped<IB, B>();",
                    "services.AddTransient<IC, C>();",
                },
                Usings = { "A.Models" },
                Namespace = "A",
                HintName = "a.cs"
            }
        }
    };
}
