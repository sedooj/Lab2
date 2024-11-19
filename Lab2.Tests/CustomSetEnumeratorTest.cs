using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab2.Tests;

[TestClass]
public class CustomSetEnumeratorTests
{
    [TestMethod]
    public void Enumerator_MoveNext_IteratesCorrectly()
    {
        var set = new CustomSet<int>();
        set.Add(1);
        set.Add(2);
        set.Add(3);
        var enumerator = set.GetEnumerator();

        // Act & Assert
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(1, enumerator.Current);

        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(2, enumerator.Current);

        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(3, enumerator.Current);

        Assert.IsFalse(enumerator.MoveNext());
    }

    [TestMethod]
    public void Enumerator_Reset_ResetsToStart()
    {
        // Arrange
        var set = new CustomSet<int>();
        set.Add(1);
        set.Add(2);
        using var enumerator = set.GetEnumerator();

        enumerator.MoveNext();
        enumerator.Reset();

        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(1, enumerator.Current);
    }

    [TestMethod]
    public void Enumerator_InvalidCurrent_ThrowsException()
    {
        var set = new CustomSet<int>();
        set.Add(1);
        using var enumerator = set.GetEnumerator();

        Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);

        enumerator.MoveNext();
        Assert.AreEqual(1, enumerator.Current);

        enumerator.MoveNext();
        Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
    }

    [TestMethod]
    public void Enumerator_VersionMismatch_ThrowsException()
    {
        var set = new CustomSet<int>();
        set.Add(1);
        set.Add(2);
        using var enumerator = set.GetEnumerator();

        enumerator.MoveNext();
        set.Add(3);

        Assert.ThrowsException<InvalidOperationException>(() => enumerator.MoveNext());
    }

    [TestMethod]
    public void Enumerator_Dispose_NoActionRequired()
    {
        // Arrange
        var set = new CustomSet<int>();
        set.Add(1);
        var enumerator = set.GetEnumerator();

        enumerator.Dispose();
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(1, enumerator.Current);
    }

    [TestMethod]
    public void Enumerator_EmptySet_ReturnsFalseOnMoveNext()
    {
        var set = new CustomSet<int>();
        using var enumerator = set.GetEnumerator();

        Assert.IsFalse(enumerator.MoveNext());
    }
    
    [TestMethod]
    public void IEnumerableGetEnumerator_ReturnsEnumerator()
    {
        var set = new CustomSet<int> { 1, 2, 3 };
        IEnumerable enumerableSet = set;

        var enumerator = enumerableSet.GetEnumerator();

        Assert.IsNotNull(enumerator);
        Assert.IsTrue(enumerator is IEnumerator);
    
        var elements = new List<int>();
        while (enumerator.MoveNext())
        {
            elements.Add((int)enumerator.Current!);
        }
        CollectionAssert.AreEquivalent(new[] { 1, 2, 3 }, elements);
        enumerator.Reset();
    }

}