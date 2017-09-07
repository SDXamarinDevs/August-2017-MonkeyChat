using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyChat.Helpers
{
    public static class LinqExtensions
    {
        public static bool Any<T>(this IEnumerable<T> collection, Func<T, bool> predicate, out T value)
        {
            try
            {
                value = collection.FirstOrDefault(predicate);
            }
            catch
            {
                value = default(T);
            }

            return !EqualityComparer<T>.Default.Equals(value, default(T));
        }
    }
}
