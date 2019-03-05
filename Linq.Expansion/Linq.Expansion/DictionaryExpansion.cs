using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq.Expansion
{
    public static class DictionaryExpansion
    {
        public static bool Empty<T1, T2>(this IDictionary<T1, T2> @this)
        {
            return !@this.Any();
        }

#if USE_TUPLE
        public static IEnumerable<(int Index, T1 Key, T2 Value)> WithIndex<T1, T2>(this IDictionary<T1, T2> @this)
        {
            return @this.Select((e, i) => (i, e.Key, e.Value));
        }
#endif

        public static IEnumerable<IDictionary<T1, T2>> Chunk<T1, T2>(this IDictionary<T1, T2> @this, int size)
        {
            while (@this.Any())
            {
                yield return @this.Take(size)
                                  .ToDictionary(e => e.Key, e => e.Value);
                @this = @this.Skip(size)
                             .ToDictionary(e => e.Key, e => e.Value);
            }
        }

    }
}
