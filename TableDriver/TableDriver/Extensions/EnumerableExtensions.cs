using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableDriver.Extensions
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> AppendEnumerable<T>(this T item, IEnumerable<T> enumerable)
        {
            List<T> list = new List<T>() { item };
            return list.Concat(enumerable);
        }

        public static IEnumerable<T> AppendItem<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.Concat(new List<T>() { item });
        }
    }
}
