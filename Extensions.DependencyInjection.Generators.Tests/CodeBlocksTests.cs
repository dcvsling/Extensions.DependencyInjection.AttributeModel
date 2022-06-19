using Extensions.DependencyInjection.Generators.CodeBlocks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Extensions.DependencyInjection.Generators.Tests;
public class CodeBlocksTests
{
    [Fact]
    public void TestNamespace()
        => TestBase(new Namespace("A.B.C"), "namespace A.B.C");
    
    internal void TestBase(object codeBlock, string content)
        => Assert.Equal(content, codeBlock.ToString());
}
