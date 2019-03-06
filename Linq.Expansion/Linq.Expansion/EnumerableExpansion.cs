using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.Expansion
{
    public static partial class LinqExpansion
    {
        private static Random RandomInstance = null;

        public static bool Empty<T>(this IEnumerable<T> @this)
        {
            return !@this.Any();
        }

#if USE_TUPLE
        public static IEnumerable<(int Index, T Value)> WithIndex<T>(this IEnumerable<T> @this)
        {
            return @this.Select((e, i) => (i, e));
        }
#else
        public static IEnumerable<KeyValuePair<int, T>> WithIndex<T>(this IEnumerable<T> @this)
        {
            return @this.Select((e, i) => new KeyValuePair<int, T>(i, e));
        }
#endif

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> @this, int size)
        {
            while (@this.Any())
            {
                yield return @this.Take(size);
                @this = @this.Skip(size);
            }
        }

        public static T Sample<T>(this IEnumerable<T> @this, Random random = null)
        {
            if (random == null)
            {
                RandomInstance = RandomInstance ?? new Random();
                random = RandomInstance;
            }

            var index = random.Next(@this.Count());
            return @this.ElementAt(index);
        }

        public static string JoinString<T>(this IEnumerable<T> @this, string separator)
        {
            return string.Join(separator, @this);
        }

        public static string JoinComma<T>(this IEnumerable<T> @this)
        {
            return @this.JoinString(",");
        }

    }
}
