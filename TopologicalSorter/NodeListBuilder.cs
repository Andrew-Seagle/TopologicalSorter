using System.Collections.Generic;
using System.Linq;

namespace TopologicalSorter
{
    internal class NodeListBuilder
    {
        private Dictionary<object, List<object>> _sortingDict;
        private bool _hasDependents;

        internal NodeList NodeList { get; set; }

        internal NodeListBuilder(Dictionary<object, List<object>> sortingDict, bool hasDependents = true)
        {
            this._sortingDict = sortingDict;
            this._hasDependents = hasDependents;
        }
        
        internal void BuildList()
        {
            var nodeList = new NodeList(_hasDependents);

            foreach (object key in _sortingDict.Keys)
            {
                nodeList.Add(new Node(key));
            }

            foreach (object key in _sortingDict.Keys)
            {
                var neighbors = new List<Node>();

                var mainNode = nodeList.Nodes.Single(n => n.Identifier.Equals(key));

                foreach (object obj in _sortingDict[key])
                {
                    var node = nodeList.Nodes.SingleOrDefault(n => n.Identifier.Equals(obj));
                    if (node != null)
                        mainNode.Neighbors.Add(node);
                }
            }

            this.NodeList = nodeList;
        }
    }
}
