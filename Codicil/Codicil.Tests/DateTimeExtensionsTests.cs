using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codicil.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void should_return_true_if_a_date_is_between_two_dates()
        {
            var today = DateTime.Today;
            var yesterday = DateTime.Today.AddDays(-1);
            var tomorrow = DateTime.Today.AddDays(1);

            Assert.IsTrue(today.IsBetween(yesterday, tomorrow), "the date is not between start and end dates");
        }

        [TestMethod]
        public void should_return_true_if_a_date_is_between_two_dates_inclusive_of_start_date()
        {
            var today = DateTime.Today;
            var laterToday = DateTime.Today.AddMinutes(10);
            var tomorrow = DateTime.Today.AddDays(1);

            Assert.IsTrue(laterToday.IsBetweenInclusive(today, tomorrow), "the date is not between start and end dates");
        }
    }
}