using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopologicalSorter
{
    class NodeList
    {
        public List<Node> Nodes { get; }
        public bool HasDependents { get; set; }

        public NodeList(bool hasDependents)
        {
            Nodes = new List<Node>();
            this.HasDependents = hasDependents;
        }

        public void Add(Node node)
        {
            node.SortStatus = Usage.Unsorted;
            Nodes.Add(node);
        }
    }
}
