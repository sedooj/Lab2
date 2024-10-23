using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace Lab2.Tests;

[TestClass]
public class CustomSetEnumeratorTests
{
    [TestMethod]
    public void Enumerator_MoveNext_IteratesCorrectly()
    {
        // Arrange
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

        Assert.IsFalse(enumerator.MoveNext()); // No more items, should return false
    }

    [TestMethod]
    public void Enumerator_Reset_ResetsToStart()
    {
        // Arrange
        var set = new CustomSet<int>();
        set.Add(1);
        set.Add(2);
        var enumerator = set.GetEnumerator();

        // Act
        enumerator.MoveNext(); // Move to the first element
        enumerator.Reset(); // Reset the enumerator

        // Assert
        Assert.IsTrue(enumerator.MoveNext()); // Should start from the beginning
        Assert.AreEqual(1, enumerator.Current);
    }

    [TestMethod]
    public void Enumerator_InvalidCurrent_ThrowsException()
    {
        var set = new CustomSet<int>();
        set.Add(1);
        var enumerator = set.GetEnumerator();

        Assert.ThrowsException<InvalidOperationException>(() =>
        {
            var invalidCurrent = enumerator.Current;
        });

        enumerator.MoveNext();
        Assert.AreEqual(1, enumerator.Current);

        enumerator.MoveNext();
        Assert.ThrowsException<InvalidOperationException>(() =>
        {
            var invalidCurrent = enumerator.Current;
        });
    }

    [TestMethod]
    public void Enumerator_VersionMismatch_ThrowsException()
    {
        // Arrange
        var set = new CustomSet<int>();
        set.Add(1);
        set.Add(2);
        var enumerator = set.GetEnumerator();

        // Act
        enumerator.MoveNext(); // Start iteration
        set.Add(3); // Modify the set

        // Assert
        Assert.ThrowsException<InvalidOperationException>(() =>
        {
            enumerator.MoveNext(); // Should fail due to version mismatch
        });
    }

    [TestMethod]
    public void Enumerator_Dispose_NoActionRequired()
    {
        // Arrange
        var set = new CustomSet<int>();
        set.Add(1);
        var enumerator = set.GetEnumerator();

        // Act & Assert
        enumerator.Dispose(); // Should be a no-op, just ensure no exceptions are thrown
        Assert.IsTrue(enumerator.MoveNext()); // Enumerator should still work
        Assert.AreEqual(1, enumerator.Current);
    }

    [TestMethod]
    public void Enumerator_EmptySet_ReturnsFalseOnMoveNext()
    {
        // Arrange
        var set = new CustomSet<int>();
        var enumerator = set.GetEnumerator();

        // Act & Assert
        Assert.IsFalse(enumerator.MoveNext()); // MoveNext should return false on empty set
    }
}