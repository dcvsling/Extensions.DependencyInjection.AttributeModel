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
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = { Parameter.Singleton } } }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = { "services.AddSingleton<A>();" },
                Usings = { "A.Models" },
                Namespace = "A"
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
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = { Parameter.Scoped } } }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = { "services.AddScoped<A>();" },
                Usings = { "A.Models" },
                Namespace = "A"
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
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = { Parameter.Transient } } }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = { "services.AddTransient<A>();" },
                Usings = { "A.Models" },
                Namespace = "A"
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
                                    Parameter.Singleton,
                                    Parameter.ServiceType("IA"),
                                } } }
                        },
                        new Class("B")
                        {
                            Interfaces = { "IB" },
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    Parameter.Scoped,
                                    Parameter.ServiceType("IB"),
                                } } }
                        },
                        new Class("C")
                        {
                            Interfaces = { "IC" },
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    Parameter.Transient,
                                    Parameter.ServiceType("IC"),
                                } } }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = {
                    "services.AddSingleton<IA, A>();",
                    "services.AddScoped<IB, B>();",
                    "services.AddTransient<IC, C>();"
                },
                Usings = { "A.Models" },
                Namespace = "A"
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
                                        Parameter.Singleton,
                                    } } }
                        },
                        new Class(new TypeName("B") { TypeParameters = { "T", "T2" } })
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        Parameter.Scoped,
                                    } } }
                        },
                        new Class(new TypeName("C") { TypeParameters = { "T", "T2", "T3" } })
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        Parameter.Transient,
                                    } } }
                        }
                    }
                }
            },
        new StringSourceProvider {
                Register = {
                    "services.AddSingleton(typeof(A<>));",
                    "services.AddScoped(typeof(B<,>));",
                    "services.AddTransient(typeof(C<,,>));",

                },
                Usings = { "A.Models" },
                Namespace = "A"
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
                                    Parameter.Singleton,
                                    Parameter.ServiceType("IA<>")
                                } } }
                    },
                    new Class(new TypeName("B") { TypeParameters = { "T", "T2" } })
                    {
                        Interfaces = { "IB<T, T2>" },
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    Parameter.Scoped,
                                    Parameter.ServiceType("IB<,>")
                                } } }
                    },
                    new Class(new TypeName("C") { TypeParameters = { "T", "T2", "T3" } })
                    {
                        Interfaces = { "IC<T, T2, T3>" },
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    Parameter.Transient,
                                    Parameter.ServiceType("IC<,,>")
                                } } }
                    }
                }
            }
        },
        new StringSourceProvider {
                Register = {
                    "services.AddSingleton(typeof(IA<>), typeof(A<>));",
                    "services.AddScoped(typeof(IB<,>), typeof(B<,>));",
                    "services.AddTransient(typeof(IC<,,>), typeof(C<,,>));"
                },
                Usings = { "A.Models" },
                Namespace = "A"
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
                                        Parameter.Singleton,
                                        Parameter.FromInstance,
                                    } } },
                            Members = { Property.CreateInstance("A") }
                        },
                        new Class("B")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        Parameter.Scoped,
                                        Parameter.ServiceType("IB"),
                                        Parameter.FromFactory,
                                    } } },
                            Interfaces = { "IB" },
                            Members = { Method.CreateFactory("IB") }
                        },
                        new Class("C")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        Parameter.Transient,
                                        Parameter.FromFactory,
                                    } } },
                            Members = { Method.CreateFactory("C") }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = {
                    "services.AddSingleton(A.Instance);",
                    "services.AddScoped<IB>(B.Factory);",
                    "services.AddTransient(C.Factory);"
                },
                Usings = { "A.Models" },
                Namespace = "A"
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
                                    Parameter.ServiceType("IA"),
                                } } }
                        },
                        new Class("B")
                        {
                            Interfaces = { "IB" },
                            CustomAttributes = { new CustomAttribute("Scoped") { Parameters = {
                                    Parameter.ServiceType("IB"),
                                } } }
                        },
                        new Class("C")
                        {
                            Interfaces = { "IC" },
                            CustomAttributes = { new CustomAttribute("Transient") { Parameters = {
                                    Parameter.ServiceType("IC"),
                                } } }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = {
                    "services.AddSingleton<IA, A>();",
                    "services.AddScoped<IB, B>();",
                    "services.AddTransient<IC, C>();",                    
                },
                Usings = { "A.Models" },
                Namespace = "A"
            }
        }
    };
    public static readonly IEnumerable<object[]> ExternalUsingDirectives = new[]
    {
        new object[] {
            new Module[]
            {
                new Module("b.cs", "B.Abstractions")
                {
                    Interfaces = { new Interface("B") },
                },
                new Module("a.cs", "A.Models")
                {
                    Usings = { "B.Abstractions" },
                    Classes =
                    {
                        new Class("A")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") {
                                Parameters = {
                                    Parameter.Singleton,
                                    Parameter.ServiceType("IB")
                                } } },
                            Interfaces = { "IB" }
                        }
                    }
                }
            },
            new StringSourceProvider {
                Register = { "services.AddSingleton<IB, A>();" },
                Usings = { "A.Models", "B.Abstractions" },
                Namespace = "A"
            }
        }
    };
}
