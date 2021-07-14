using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
