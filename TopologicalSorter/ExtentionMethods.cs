using System.Collections.Generic;
using System.Linq;

namespace TopologicalSorter
{
    internal static class ExtentionMethods
    {
        internal static bool NullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return true;

            return !collection.Any();
        }
    }
}
