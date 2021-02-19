using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TableDriver.RowQuery;

namespace TableDriver.Tests
{
    [TestClass]
    public class FieldConditionTests
    {
        [TestMethod]
        public void FieldConditionParse_Basic()
        {
            string conditionString = @"foo=bar";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("foo", fieldCondition.Field);
            Assert.AreEqual("bar", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldConditionParse_FieldNotByIndex()
        {
            string conditionString = @"365=bar";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("365", fieldCondition.Field);
            Assert.AreEqual("bar", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldConditionParse_FieldByIndex()
        {
            string conditionString = @"\365=bar";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("365", fieldCondition.Field);
            Assert.AreEqual("bar", fieldCondition.Value);
            Assert.IsTrue(fieldCondition.FieldIndex.HasValue);
            Assert.AreEqual(365, fieldCondition.FieldIndex.Value);
            Assert.IsTrue(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FieldCondition_NoOperator()
        {
            string conditionString = @"foobar";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);
        }

        [TestMethod]
        public void FieldCondition_EscapedChars()
        {
            string conditionString = @"\foo\=bar\\\&\|\\=b\ar\=foo\\\|\&";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual(@"foo=bar\&|\", fieldCondition.Field);
            Assert.AreEqual(@"bar=foo\|&", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FieldCondition_EscapedCharsNoOperator()
        {
            string conditionString = @"\foo\=bar\\\&\|\\\=b\ar\=foo\\\|\&";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);
        }
    }
}
