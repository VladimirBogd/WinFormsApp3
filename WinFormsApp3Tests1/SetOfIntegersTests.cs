using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsApp3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp3.Tests
{
    [TestClass()]
    public class SetOfIntegersTests
    {
        [TestMethod()]
        public void AddNotExistElementToFirstTest() // Добавление элемента в 1-ое множество, которого там нет
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 1, 2, 3 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 10 }));
            SetOfIntegers resultSet = oneSet + otherSet;
            Assert.AreEqual("1 2 3 10", resultSet.PrintSet());
        }
        [TestMethod()]
        public void AddExistElementToFirstTest() // Добавление элемента в 1-ое множество, который там есть
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 1, 2, 3 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 3 }));
            SetOfIntegers resultSet = oneSet + otherSet;
            Assert.AreEqual("1 2 3", resultSet.PrintSet());
        }
        [TestMethod()]
        public void AddNotExistElementToSecondTest() // Добавление элемента во 2-ое множество, которого там нет
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 3 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 10, 11, 12 }));
            SetOfIntegers resultSet = otherSet + oneSet;
            Assert.AreEqual("10 11 12 3", resultSet.PrintSet());
        }
        [TestMethod()]
        public void AddExistElementToSecondTest() // Добавление элемента во 2-ое множество, который там есть
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 12 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 10, 11, 12 }));
            SetOfIntegers resultSet = otherSet + oneSet;
            Assert.AreEqual("10 11 12", resultSet.PrintSet());
        }
        [TestMethod()]
        public void RemoveExistElementFromFirstTest() // Удаление элемента из 1-го множества, который там есть
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 1, 2, 3 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 3 }));
            SetOfIntegers resultSet = oneSet - otherSet;
            Assert.AreEqual("1 2", resultSet.PrintSet());
        }
        [TestMethod()]
        public void RemoveNotExistElementFromFirstTest() // Удаление элемента из 1-го множества, которого там нет
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 1, 2, 3 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 10 }));
            SetOfIntegers resultSet = oneSet - otherSet;
            Assert.AreEqual("1 2 3", resultSet.PrintSet());
        }
        [TestMethod()]
        public void RemoveExistElementFromSecondTest() // Удаление элемента из 2-го множества, который там есть
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 10 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 11, 12, 10 }));
            SetOfIntegers resultSet = otherSet - oneSet;
            Assert.AreEqual("11 12", resultSet.PrintSet());
        }
        [TestMethod()]
        public void RemoveNotExistElementFromSecondTest() // Удаление элемента из 2-го множества, которого там нет
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 10 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 11, 12, 13 }));
            SetOfIntegers resultSet = otherSet - oneSet;
            Assert.AreEqual("11 12 13", resultSet.PrintSet());
        }
        [TestMethod()]
        public void UnionTest() // Объединение 2-х множеств
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 1, 2, 3, 4 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 4, 5, 6, 7 }));
            SetOfIntegers resultSet = oneSet + otherSet;
            Assert.AreEqual("1 2 3 4 5 6 7", resultSet.PrintSet());
        }
        [TestMethod()]
        public void DifferenceTest() // Разность 2-х множеств
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 1, 2, 3, 4, 5, 6 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 4, 5, 6, 7 }));
            SetOfIntegers resultSet = oneSet - otherSet;
            Assert.AreEqual("1 2 3", resultSet.PrintSet());
        }
        [TestMethod()]
        public void IntersectionTest() // Пересечение 2-х множеств
        {
            SetOfIntegers oneSet = new(new List<int>(new int[] { 1, 2, 3, 4, 5, 6 }));
            SetOfIntegers otherSet = new(new List<int>(new int[] { 4, 5, 6, 7 }));
            SetOfIntegers resultSet = oneSet & otherSet;
            Assert.AreEqual("4 5 6", resultSet.PrintSet());
        }
    }
}