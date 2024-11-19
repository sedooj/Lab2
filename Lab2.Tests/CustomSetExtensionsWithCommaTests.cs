using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab2.Tests;

[TestClass]
public class CustomSetExtensionsWithCommaTests
{
    [TestMethod]
    public void WithCommas_EmptySet_ReturnsEmptyString()
    {
        var set = new CustomSet<string>();

        var result = set.WithCommas();

        Assert.AreEqual(string.Empty, result);
    }
    
    [TestMethod]
    public void WithCommas_SetWithOneElement_ReturnsSingleElement()
    {
        var set = new CustomSet<string> { "Hello" };

        var result = set.WithCommas();

        Assert.AreEqual("Hello", result);
    }
    [TestMethod]
    public void WithCommas_SetWithMultipleElements_ReturnsCommaSeparatedString()
    {
        var set = new CustomSet<string> { "Apple", "Banana", "Cherry" };

        var result = set.WithCommas();

        Assert.AreEqual("Apple, Banana, Cherry", result);
    }

}