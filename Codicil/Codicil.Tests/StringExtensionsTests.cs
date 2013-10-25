using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codicil.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void should_convert_string_to_an_integer_value()
        {
            const string s = "2";
            Assert.AreEqual(2, s.ToInt32(), "converted values don't match");
        }

        [TestMethod]
        public void should_return_zero_for_string_which_cant_be_converted()
        {
            const string s = "junk";
            Assert.AreEqual(0, s.ToInt32(), "converted values don't match");
        }

        [TestMethod]
        public void should_check_for_null_string()
        {
            const string s = null;
            Assert.IsTrue(s.IsNullOrWhiteSpace(), "string is not null");
        }

        [TestMethod]
        public void should_check_for_whitespace_string()
        {
            const string s = "    ";
            Assert.IsTrue(s.IsNullOrWhiteSpace(), "string is not whitespace");
        }

        [TestMethod]
        public void should_check_for_equality()
        {
            const string s = "hi";
            Assert.IsFalse(s.DoesNotEqual(s), "strings are not equal");
        }

        [TestMethod]
        public void should_return_a_space_seperated_string_from_camel_cased_string()
        {
            const string s = "ClassName";
            Assert.AreEqual("Class Name", s.ToSpaceSeperatedStringFromCamelCasedString(), "strings do not match");
        }
    }
}