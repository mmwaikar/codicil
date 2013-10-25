using System;

namespace Codicil
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime value, DateTime start, DateTime end)
        {
            return value > start && value < end;
        }

        public static bool IsBetweenInclusive(this DateTime value, DateTime start, DateTime end)
        {
            return value >= start && value <= end;
        }
    }
}