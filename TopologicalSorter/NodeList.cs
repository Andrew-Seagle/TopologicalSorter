using System.Collections.Generic;

namespace TopologicalSorter
{
    internal class NodeList
    {
        internal List<Node> Nodes { get; }
        internal bool HasDependents { get; set; }

        internal NodeList(bool hasDependents)
        {
            Nodes = new List<Node>();
            this.HasDependents = hasDependents;
        }

        internal void Add(Node node)
        {
            node.SortStatus = Usage.Unsorted;
            Nodes.Add(node);
        }
    }
}
