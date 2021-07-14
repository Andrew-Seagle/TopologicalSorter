using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Node> Prerequisites { get; set; }
        public List<Node> Dependents { get; set; }
        public Usage SortStatus { get; set; }
    }
}
