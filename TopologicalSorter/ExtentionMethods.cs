using System.Collections.Generic;
using System.Linq;

namespace TopologicalSorter
{
    public static class ExtentionMethods
    {
        public static bool NullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                return true;

            return !collection.Any();
        }
    }
}
