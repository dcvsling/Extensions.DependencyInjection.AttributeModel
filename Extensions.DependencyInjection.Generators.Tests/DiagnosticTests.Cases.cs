using Extensions.DependencyInjection.Generators.Tests.Builder;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators.Tests.Diagnostic;
public partial class DiagnosticTests
{
    public readonly static IEnumerable<object[]> DG001 = new[] { new object[]
    {
        new DiagnosticDescriptor?[] { DiagnosticDescriptors.DG001 },
        new Module[]
        {
            new Module("a.cs", "A.Models")
            {
                Classes =
                {
                    new Class("A")
                    {
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    Parameter.Singleton,
                                    Parameter.FromInstance,
                                } } }
                    }
                }
            }
        }
    }};

    public readonly static IEnumerable<object[]> DG002 = new[] { new object[]
    {
        new DiagnosticDescriptor[] { DiagnosticDescriptors.DG002 },
        new Module[]
        {
            new Module("a.cs", "A.Models")
            {
                Classes =
                {
                    new Class("NotA"),
                    new Class("A")
                    {
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    Parameter.Singleton,
                                    Parameter.FromInstance,
                                } } },
                        Members =
                        {
                            new Property("NotA", "Instance", Class.INDENT, new []{ "get" }, "public", "static")
                        }
                    }
                }
            }
        }
    }};

    public readonly static IEnumerable<object[]> DG003 = new[] { 
        new object[] {
            new DiagnosticDescriptor[] { DiagnosticDescriptors.DG003 },
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Classes =
                    {
                        new Class("A")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        Parameter.Singleton,
                                        Parameter.FromInstance,
                                    } } },
                            Members =
                            {
                                new Property("A", "Instance", Class.INDENT, new []{ "get", "set" }, "public", "static")
                            }
                        }
                    }
                }
            }            
        },
        new object[]
        {
            new DiagnosticDescriptor[] { DiagnosticDescriptors.DG003 },
            new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Classes =
                    {
                        new Class("A")
                        {
                            CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                        Parameter.Singleton,
                                        Parameter.FromInstance,
                                    } } },
                            Members =
                            {
                                new Property("A", "Instance", Class.INDENT, new []{ "get" }, "public")
                            }
                        }
                    }
                }
            }
        }
    };

    public readonly static IEnumerable<object[]> DG004 = new[] { new object[]
    {
        new DiagnosticDescriptor[] { DiagnosticDescriptors.DG004 },
        new Module[]
        {
            new Module("a.cs", "A.Models")
            {
                Classes =
                {
                    new Class("A")
                    {
                        CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
                                    Parameter.Scoped,
                                    Parameter.FromInstance,
                                } } },
                        Members =
                        {
                            Property.CreateInstance("A")
                        }
                    }
                }
            }
        }
    }};

    public readonly static IEnumerable<object[]> DG005 = new[] { new object[]
    {
        new DiagnosticDescriptor[] { DiagnosticDescriptors.DG005 },
        new Module[]
            {
                new Module("a.cs", "A.Models")
                {
                    Interfaces =
                    {
                        new Interface("IA")
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
    }};
}
