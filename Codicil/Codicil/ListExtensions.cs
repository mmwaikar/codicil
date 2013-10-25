using System.Collections.Generic;

namespace Codicil
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmptyList<T>(this IList<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}