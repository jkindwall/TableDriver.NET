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
            Assert.AreEqual("=", fieldCondition.Operation);
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
            Assert.AreEqual("=", fieldCondition.Operation);
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
            Assert.AreEqual("=", fieldCondition.Operation);
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
            Assert.AreEqual("=", fieldCondition.Operation);
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

        [TestMethod]
        public void FieldCondition_NotEqual()
        {
            string conditionString = @"\!foo!=ba\=r";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("!foo", fieldCondition.Field);
            Assert.AreEqual("!=", fieldCondition.Operation);
            Assert.AreEqual("ba=r", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldCondition_LessThan()
        {
            string conditionString = @"\5<noo\<dle";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("5", fieldCondition.Field);
            Assert.AreEqual("<", fieldCondition.Operation);
            Assert.AreEqual("noo<dle", fieldCondition.Value);
            Assert.IsTrue(fieldCondition.FieldIndex.HasValue);
            Assert.AreEqual(5, fieldCondition.FieldIndex.Value);
            Assert.IsTrue(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldCondition_LessThanOrEqual()
        {
            string conditionString = @"f\<o\=o<=22";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("f<o=o", fieldCondition.Field);
            Assert.AreEqual("<=", fieldCondition.Operation);
            Assert.AreEqual("22", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldCondition_GreaterThan()
        {
            string conditionString = @"\18\>>19";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("18>", fieldCondition.Field);
            Assert.AreEqual(">", fieldCondition.Operation);
            Assert.AreEqual("19", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldCondition_GreaterThanOrEqual()
        {
            string conditionString = @"\>\=foo>=99";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual(">=foo", fieldCondition.Field);
            Assert.AreEqual(">=", fieldCondition.Operation);
            Assert.AreEqual("99", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldCondition_StartsWith()
        {
            string conditionString = @"\24^=bar\^\=";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("24", fieldCondition.Field);
            Assert.AreEqual("^=", fieldCondition.Operation);
            Assert.AreEqual("bar^=", fieldCondition.Value);
            Assert.IsTrue(fieldCondition.FieldIndex.HasValue);
            Assert.AreEqual(24, fieldCondition.FieldIndex.Value);
            Assert.IsTrue(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        public void FieldCondition_Contains()
        {
            string conditionString = @"f\*o\=o*=b\=a\*r";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);

            Assert.AreEqual("f*o=o", fieldCondition.Field);
            Assert.AreEqual("*=", fieldCondition.Operation);
            Assert.AreEqual("b=a*r", fieldCondition.Value);
            Assert.IsFalse(fieldCondition.FieldIndex.HasValue);
            Assert.IsFalse(fieldCondition.IsFieldByIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FieldCondition_IncompleteNotEqualOperator()
        {
            string conditionString = @"foo!bar";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FieldCondition_IncompleteStartsWithOperator()
        {
            string conditionString = @"foo^bar";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FieldCondition_IncompleteContainsOperator()
        {
            string conditionString = @"foo*bar";
            FieldCondition fieldCondition = FieldCondition.Parse(conditionString);
        }
    }
}
