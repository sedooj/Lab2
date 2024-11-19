using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Tests
{
    [TestClass]
    public class CustomSetTests
    {
        [TestMethod]
        public void Add_UniqueItem_AddsItem()
        {
            var set = new CustomSet<int>();

            set.Add(1);

            Assert.AreEqual(1, set.Count);
            Assert.AreEqual(1, set.ToList()[0]);
        }

        [TestMethod]
        public void Add_DuplicateItem_DoesNotAddItem()
        {
            var set = new CustomSet<int>();
            set.Add(1);

            set.Add(1);

            Assert.AreEqual(1, set.Count);
        }

        [TestMethod]
        public void Copy_ReturnsTrueIfItemIsTheSame()
        {
            var set = new CustomSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(3);
            var copiedSet = set.Copy();
            Assert.AreEqual(set, copiedSet);
            Assert.AreNotSame(set, copiedSet);
        }


        [TestMethod]
        public void AddAll_ReturnsSuccessIfItemIsTheSame()
        {
            var setToAdd = new CustomSet<int> { 1, 2, 3 };
            var set = new CustomSet<int> { 4, 5, 6 };
            var expected = new CustomSet<int> { 1, 2, 3, 4, 5, 6 };

            set.AddAll(setToAdd);

            Assert.AreEqual(expected, set);
        }

        [TestMethod]
        public void Remove_ExistingItem_RemovesItem()
        {
            var set = new CustomSet<int>();
            set.Add(1);
            bool result = set.Remove(1);
            Assert.IsTrue(result);
            Assert.AreEqual(0, set.Count);
        }

        [TestMethod]
        public void OperatorPlus_ReturnsSuccessIfItemIsTheSame()
        {
            var firstSet = new CustomSet<int> { 1, 2, 3 };
            var secondSet = new CustomSet<int> { 4, 5, 6 };
            var expected = new CustomSet<int> { 1, 2, 3, 4, 5, 6 };
            var set = firstSet + secondSet;
            Assert.AreEqual(expected, set);
        }

        [TestMethod]
        public void OperatorPlus_AddToEmptySet_ReturnsNewSetWithItem()
        {
            var set = new CustomSet<int>();

            var result = set + 42;

            Assert.IsTrue(result.Contains(42));
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Contains_ReturnsTrueIfItemIsExist_True()
        {
            var set = new CustomSet<int> { 1, 2, 3 };
            Assert.IsTrue(set.Contains(3));
        }

        [TestMethod]
        public void Contains_ReturnsTrueIfItemIsExist_False()
        {
            var set = new CustomSet<int> { 1, 2, 3 };
            Assert.IsFalse(set.Contains(4));
        }

        [TestMethod]
        public void Remove_NonExistingItem_ReturnsFalse()
        {
            var set = new CustomSet<int>();

            bool result = set.Remove(1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OperatorMultiply_ReturnsSuccess()
        {
            var firstSet = new CustomSet<int> { 1, 2, 3 };
            var secondSet = new CustomSet<int> { 1, 4, 3 };

            var set = new CustomSet<int> { 1, 3 };

            Assert.AreEqual(set, firstSet * secondSet);
        }

        [TestMethod]
        public void OperatorMultiply_ReturnsEmpty()
        {
            var firstSet = new CustomSet<int> { 1, 2, 3 };
            var secondSet = new CustomSet<int> { 4, 5, 6 };

            var set = new CustomSet<int>();

            Assert.AreEqual(set, firstSet * secondSet);
        }

        [TestMethod]
        public void OperatorInt_ReturnsSuccess()
        {
            var set = new CustomSet<int> { 1, 2, 3 };
            Assert.AreEqual(3, (int)set);
        }

        [TestMethod]
        public void RemoveAt_ValidIndex_RemovesItem()
        {
            var set = new CustomSet<int>();
            set.Add(1);
            set.Add(2);

            set.RemoveAt(0);

            Assert.AreEqual(1, set.Count);
            Assert.AreEqual(2, set.ToList()[0]);
        }

        [TestMethod]
        public void RemoveAt_ReferenceType_ClearsElement()
        {
            var set = new CustomSet<string> { "A", "B", "C" };

            set.RemoveAt(1); // Removes "B".

            Assert.AreEqual(2, set.Count); // Ensure the size is updated.
            Assert.IsTrue(set.Contains("A")); // Ensure "A" is still in the set.
            Assert.IsTrue(set.Contains("C")); // Ensure "C" is still in the set.
            Assert.IsFalse(set.Contains("B")); // Ensure "B" is removed.
        }


        [TestMethod]
        public void RemoveAt_InvalidIndex_ThrowsException()
        {
            var set = new CustomSet<int>();

            Assert.ThrowsException<IndexOutOfRangeException>(() => set.RemoveAt(0));
        }

        [TestMethod]
        public void Clear_RemovesAllItems()
        {
            var set = new CustomSet<int>();
            set.Add(1);
            set.Add(2);

            set.Clear();

            Assert.AreEqual(0, set.Count);
        }

        [TestMethod]
        public void Clear_ReferenceType_ClearsArray()
        {
            var set = new CustomSet<string> { "A", "B", "C" };

            set.Clear();

            Assert.AreEqual(0, set.Count);
        }


        [TestMethod]
        public void Enumerator_ToListConverter()
        {
            var set = new CustomSet<int>();
            set.Add(1);
            set.Add(2);

            var result = set.ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(1));
            Assert.IsTrue(result.Contains(2));
        }

        [TestMethod]
        public void Equals_IdenticalSets_ReturnsTrue()
        {
            var set1 = new CustomSet<int>();
            var set2 = new CustomSet<int>();
            set1.Add(1);
            set1.Add(2);
            set2.Add(1);
            set2.Add(2);

            bool result = set1.Equals(set2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_DifferentSets_ReturnsFalse()
        {
            var set1 = new CustomSet<int>();
            var set2 = new CustomSet<int>();
            set1.Add(1);
            set2.Add(2);

            bool result = set1.Equals(set2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentTypes_ReturnsFalse()
        {
            var set1 = new CustomSet<int>();

            bool result = set1.Equals(2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_ReturnsFalse_IfSizesDiffer()
        {
            var set1 = new CustomSet<int> { 1, 2, 3 };
            var set2 = new CustomSet<int> { 1, 2 };

            Assert.IsFalse(set1.Equals(set2));
        }


        [TestMethod]
        public void ToString_ReturnsExpectedString()
        {
            var set = new CustomSet<int>();
            set.Add(1);
            set.Add(2);

            string result = set.ToString();

            Assert.AreEqual("{ 1, 2 }", result);
        }

        [TestMethod]
        public void Constructor_WithItems_AddsAllItems()
        {
            var items = new List<int> { 1, 2, 3 };
            var set = new CustomSet<int>(items);

            Assert.AreEqual(3, set.Count);
            Assert.IsTrue(set.Contains(1));
            Assert.IsTrue(set.Contains(2));
            Assert.IsTrue(set.Contains(3));
        }

        [TestMethod]
        public void Capacity_SetLessThanSize_ThrowsException()
        {
            var set = new CustomSet<int> { 1 };
            Assert.ThrowsException<IndexOutOfRangeException>(() => set.Capacity = 0);
        }

        [TestMethod]
        public void SetCapacity_ToLargerValue_ExpandsArray()
        {
            var set = new CustomSet<int> { 1, 2 };

            set.Capacity = 10;

            Assert.IsTrue(set.Capacity >= 10); // Capacity should meet or exceed the requested value.
            Assert.AreEqual(2, set.Count); // Existing elements should remain unaffected.
        }

        [TestMethod]
        public void SetCapacity_ToZero_ResetsItemsArray()
        {
            var set = new CustomSet<int>();
            
            set.Capacity = 0;
            
            Assert.AreEqual(0, set.Count); 
            Assert.AreEqual(0, set.Capacity);
        }

        [TestMethod]
        public void Capacity_IncreasesCapacitySuccessfully()
        {
            var set = new CustomSet<int> { 1 };
            set.Capacity = 10;

            Assert.AreEqual(10, set.Capacity);
            Assert.IsTrue(set.Contains(1));
        }

        [TestMethod]
        public void Capacity_SetValidValue_UpdatesCapacity()
        {
            var set = new CustomSet<int> { 1, 2, 3 };
            set.Capacity = 10;

            Assert.AreEqual(10, set.Capacity);
        }

        [TestMethod]
        public void Capacity_SetInvalidValue_ThrowsException()
        {
            var set = new CustomSet<int> { 1, 2, 3 };
            Assert.ThrowsException<IndexOutOfRangeException>(() => set.Capacity = 2);
        }

        [TestMethod]
        public void OperatorTrueFalse_ReturnsTrueeIfSetIsNotEmpty()
        {
            var set = new CustomSet<int> { 1, 2 };

            var ans = false;
            if (set)
            {
                ans = true;
            }

            Assert.IsTrue(ans);
        }

        [TestMethod]
        public void Enumerator_MoveNext_InvalidVersion_ThrowsException()
        {
            var set = new CustomSet<int> { 1, 2 };
            using var enumerator = set.GetEnumerator();

            set.Add(3); // Modifies the set

            Assert.ThrowsException<InvalidOperationException>(() => enumerator.MoveNext());
        }

        [TestMethod]
        public void Enumerator_Current_BeforeMoveNext_ThrowsException()
        {
            var set = new CustomSet<int> { 1, 2 };
            using var enumerator = set.GetEnumerator();

            Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        }

        [TestMethod]
        public void Enumerator_Reset_InvalidVersion_ThrowsException()
        {
            var set = new CustomSet<int> { 1, 2 };
            using var enumerator = set.GetEnumerator();

            set.Add(3);
            Assert.ThrowsException<InvalidOperationException>(() => enumerator.Reset());
        }
    }
}