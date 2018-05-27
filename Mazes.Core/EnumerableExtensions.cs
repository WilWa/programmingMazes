using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazes.Core
{
    internal static class EnumerableExtensions
    {
        private static Random random = new Random();

        public static T Random<T>(this IEnumerable<T> source)
        {
            int count = source.Count();

            if (count == 0)
            {
                return default(T);
            }

            int index = random.Next(count);
            return source.ElementAt<T>(index);
        }
    }
}
