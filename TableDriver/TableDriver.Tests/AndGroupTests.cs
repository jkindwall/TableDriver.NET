using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TableDriver.RowQuery;

namespace TableDriver.Tests
{
    [TestClass]
    public class AndGroupTests
    {
        [TestMethod]
        public void AndGroup_SingleCondition()
        {
            string andGroupString = "foo=bar";
            AndGroup andGroup = AndGroup.Parse(andGroupString);

            Assert.IsNotNull(andGroup.Conditions);
            Assert.AreEqual(1, andGroup.Conditions.Count);

            Assert.AreEqual("foo", andGroup.Conditions.First().Field);
            Assert.AreEqual("bar", andGroup.Conditions.First().Value);
            Assert.IsFalse(andGroup.Conditions.First().FieldIndex.HasValue);
            Assert.IsFalse(andGroup.Conditions.First().IsFieldByIndex);
        }

        [TestMethod]
        public void AndGroup_MultipleConditions()
        {
            string andGroupString = @"foo=bar&123=456&\789=987&\\\=\&\|=\|\&\=\\";
            AndGroup andGroup = AndGroup.Parse(andGroupString);

            Assert.IsNotNull(andGroup.Conditions);
            Assert.AreEqual(4, andGroup.Conditions.Count);

            Assert.AreEqual("foo", andGroup.Conditions.First().Field);
            Assert.AreEqual("bar", andGroup.Conditions.First().Value);
            Assert.IsFalse(andGroup.Conditions.First().FieldIndex.HasValue);
            Assert.IsFalse(andGroup.Conditions.First().IsFieldByIndex);

            Assert.AreEqual("123", andGroup.Conditions.Skip(1).First().Field);
            Assert.AreEqual("456", andGroup.Conditions.Skip(1).First().Value);
            Assert.IsFalse(andGroup.Conditions.Skip(1).First().FieldIndex.HasValue);
            Assert.IsFalse(andGroup.Conditions.Skip(1).First().IsFieldByIndex);

            Assert.AreEqual("789", andGroup.Conditions.Skip(2).First().Field);
            Assert.AreEqual("987", andGroup.Conditions.Skip(2).First().Value);
            Assert.IsTrue(andGroup.Conditions.Skip(2).First().FieldIndex.HasValue);
            Assert.AreEqual(789, andGroup.Conditions.Skip(2).First().FieldIndex.Value);
            Assert.IsTrue(andGroup.Conditions.Skip(2).First().IsFieldByIndex);

            Assert.AreEqual(@"\=&|", andGroup.Conditions.Skip(3).First().Field);
            Assert.AreEqual(@"|&=\", andGroup.Conditions.Skip(3).First().Value);
            Assert.IsFalse(andGroup.Conditions.Skip(3).First().FieldIndex.HasValue);
            Assert.IsFalse(andGroup.Conditions.Skip(3).First().IsFieldByIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AndGroup_MissingConditionStart()
        {
            string andGroupString = @"&foo=bar";
            AndGroup andGroup = AndGroup.Parse(andGroupString);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AndGroup_MissingConditionEnd()
        {
            string andGroupString = @"foo=bar&";
            AndGroup andGroup = AndGroup.Parse(andGroupString);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AndGroup_MissingConditionMiddle()
        {
            string andGroupString = @"foo=bar&&bar=foo";
            AndGroup andGroup = AndGroup.Parse(andGroupString);
        }
    }
}
