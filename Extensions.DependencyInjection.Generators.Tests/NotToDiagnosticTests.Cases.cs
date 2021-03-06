using Extensions.DependencyInjection.Generators.Tests.Builder;

using System.Collections.Generic;

namespace Extensions.DependencyInjection.Generators.Tests.Diagnostic;
public partial class NotToDiagnosticTests
{
    public readonly static IEnumerable<object[]> DIGEN01 = new[] { new object[]
    {
        new Module[]
        {
            new Module("a.cs", "A.Models")
            {
                Classes =
                {
                    new Class("B")
                    {
                        CustomAttributes = { new CustomAttribute("Singleton") }
                    }
                }
            }
        }
    }};

    //public readonly static IEnumerable<object[]> DIGEN02 = new[] { new object[]
    //{
    //    new DiagnosticDescriptor[] { DiagnosticDescriptors.DG002 },
    //    new Module[]
    //    {
    //        new Module("a.cs", "A.Models")
    //        {
    //            Classes =
    //            {
    //                new Class("NotA"),
    //                new Class("A")
    //                {
    //                    CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
    //                                AttributeParameter.Singleton,
    //                                AttributeParameter.FromInstance,
    //                            } } },
    //                    Members =
    //                    {
    //                        new Property("NotA", "Instance", Class.INDENT, new []{ "get" }, "public", "static")
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}};

    //public readonly static IEnumerable<object[]> DIGEN03 = new[] { new object[]
    //{
    //    new DiagnosticDescriptor[] { DiagnosticDescriptors.DG003, DiagnosticDescriptors.DG003 },
    //    new Module[]
    //    {
    //        new Module("a.cs", "A.Models")
    //        {
    //            Classes =
    //            {
    //                new Class("A")
    //                {
    //                    CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
    //                                AttributeParameter.Singleton,
    //                                AttributeParameter.FromInstance,
    //                            } } },
    //                    Members =
    //                    {
    //                        new Property("A", "Instance", Class.INDENT, new []{ "get", "set" }, "public", "static")
    //                    }
    //                },
    //                new Class("B")
    //                {
    //                    CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
    //                                AttributeParameter.Singleton,
    //                                AttributeParameter.FromInstance,
    //                            } } },
    //                    Members =
    //                    {
    //                        new Property("B", "Instance", Class.INDENT, new []{ "get"}, "public")
    //                    }
    //                }

    //            }
    //        }
    //    }
    //}};

    //public readonly static IEnumerable<object[]> DIGEN04 = new[] { new object[]
    //{
    //    new DiagnosticDescriptor[] { DiagnosticDescriptors.DG004 },
    //    new Module[]
    //    {
    //        new Module("a.cs", "A.Models")
    //        {
    //            Classes =
    //            {
    //                new Class("A")
    //                {
    //                    CustomAttributes = { new CustomAttribute("Inject") { Parameters = {
    //                                AttributeParameter.Scoped,
    //                                AttributeParameter.FromInstance,
    //                            } } },
    //                    Members =
    //                    {
    //                        Property.CreateInstance("A")
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}};
}
