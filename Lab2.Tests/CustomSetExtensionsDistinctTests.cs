using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab2.Tests;

[TestClass]
public class CustomSetExtensionsDistinctTests
{
    [TestMethod]
    public void Distinct_OriginalSetUnchanged()
    {
        var set = new CustomSet<int> { 1, 2, 2, 3 };

        var result = set.Distinct();

        Assert.AreEqual(3, set.Count);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(set, result);
        Assert.AreNotSame(set, result);
    }
}