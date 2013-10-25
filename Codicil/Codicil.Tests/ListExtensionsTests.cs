using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codicil.Tests
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void should_return_true_for_an_empty_list()
        {
            var list= new List<int>();
            Assert.IsTrue(list.IsNullOrEmptyList(), "list is not empty");
        }

        [TestMethod]
        public void should_return_true_for_a_null_list()
        {
            List<int> list = null;
            Assert.IsTrue(list.IsNullOrEmptyList(), "list is not null");
        }
    }
}