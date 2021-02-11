using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TableDriver.RowQuery;

namespace TableDriver.Tests
{
    [TestClass]
    public class OrGroupTests
    {
        [TestMethod]
        public void OrGroup_SingleAndGroup()
        {
            string orGroupString = "foo=bar";
            OrGroup orGroup = OrGroup.Parse(orGroupString);

            Assert.IsNotNull(orGroup.AndGroups);
            Assert.AreEqual(1, orGroup.AndGroups.Count);

            Assert.AreEqual(1, orGroup.AndGroups.First().Conditions.Count);

            Assert.AreEqual("foo", orGroup.AndGroups.First().Conditions.First().Field);
            Assert.AreEqual("bar", orGroup.AndGroups.First().Conditions.First().Value);
            Assert.IsFalse(orGroup.AndGroups.First().Conditions.First().FieldIndex.HasValue);
            Assert.IsFalse(orGroup.AndGroups.First().Conditions.First().IsFieldByIndex);
        }

        [TestMethod]
        public void OrGroup_MultipleAndGroups()
        {
            string orGroupString = @"foo=bar|123=456&\789=987|\\\=\&\|=\|\&\=\\";
            OrGroup orGroup = OrGroup.Parse(orGroupString);

            Assert.IsNotNull(orGroup.AndGroups);
            Assert.AreEqual(3, orGroup.AndGroups.Count);

            Assert.AreEqual(1, orGroup.AndGroups.First().Conditions.Count);

            Assert.AreEqual("foo", orGroup.AndGroups.First().Conditions.First().Field);
            Assert.AreEqual("bar", orGroup.AndGroups.First().Conditions.First().Value);
            Assert.IsFalse(orGroup.AndGroups.First().Conditions.First().FieldIndex.HasValue);
            Assert.IsFalse(orGroup.AndGroups.First().Conditions.First().IsFieldByIndex);

            Assert.AreEqual(2, orGroup.AndGroups.Skip(1).First().Conditions.Count);

            Assert.AreEqual("123", orGroup.AndGroups.Skip(1).First().Conditions.First().Field);
            Assert.AreEqual("456", orGroup.AndGroups.Skip(1).First().Conditions.First().Value);
            Assert.IsFalse(orGroup.AndGroups.Skip(1).First().Conditions.First().FieldIndex.HasValue);
            Assert.IsFalse(orGroup.AndGroups.Skip(1).First().Conditions.First().IsFieldByIndex);

            Assert.AreEqual("789", orGroup.AndGroups.Skip(1).First().Conditions.Skip(1).First().Field);
            Assert.AreEqual("987", orGroup.AndGroups.Skip(1).First().Conditions.Skip(1).First().Value);
            Assert.IsTrue(orGroup.AndGroups.Skip(1).First().Conditions.Skip(1).First().FieldIndex.HasValue);
            Assert.AreEqual(789, orGroup.AndGroups.Skip(1).First().Conditions.Skip(1).First().FieldIndex.Value);
            Assert.IsTrue(orGroup.AndGroups.Skip(1).First().Conditions.Skip(1).First().IsFieldByIndex);

            Assert.AreEqual(1, orGroup.AndGroups.Skip(2).First().Conditions.Count);

            Assert.AreEqual(@"\=&|", orGroup.AndGroups.Skip(2).First().Conditions.First().Field);
            Assert.AreEqual(@"|&=\", orGroup.AndGroups.Skip(2).First().Conditions.First().Value);
            Assert.IsFalse(orGroup.AndGroups.Skip(2).First().Conditions.First().FieldIndex.HasValue);
            Assert.IsFalse(orGroup.AndGroups.Skip(2).First().Conditions.First().IsFieldByIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void OrGroup_MissingConditionStart()
        {
            string orGroupString = @"|foo=bar";
            OrGroup orGroup = OrGroup.Parse(orGroupString);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void OrGroup_MissingConditionEnd()
        {
            string orGroupString = @"foo=bar|";
            OrGroup orGroup = OrGroup.Parse(orGroupString);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void OrGroup_MissingConditionMiddle()
        {
            string orGroupString = @"foo=bar||bar=foo";
            OrGroup orGroup = OrGroup.Parse(orGroupString);
        }
    }
}
