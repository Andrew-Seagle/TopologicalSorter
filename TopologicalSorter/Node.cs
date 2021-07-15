using System.Collections.Generic;

namespace TopologicalSorter
{
    internal enum Usage
    {
        Unsorted,
        Sorting,
        Sorted
    }

    internal class Node
    {
        internal object Identifier { get; set; }
        internal List<Node> Neighbors { get; set; }
        internal Usage SortStatus { get; set; }

        internal Node(object identifier, bool hasDepentsList = true)
        {
            this.Identifier = identifier;
            this.Neighbors = new List<Node>();
        }
    }
}
