using System;
using System.Collections.Generic;

namespace TopologicalSorter
{
    public enum Usage
    {
        Unsorted,
        Sorting,
        Sorted
    }

    class Node
    {
        public Object Identifier { get; set; }
        public List<Node> Neighbors { get; set; }
        // public List<Node> Prerequisites { get; set; }
        // public List<Node> Dependents { get; set; }
        public Usage SortStatus { get; set; }

        public Node(Object identifier, bool hasDepentsList = true)
        {
            this.Identifier = identifier;
            this.Neighbors = new List<Node>();
        }
    }
}
