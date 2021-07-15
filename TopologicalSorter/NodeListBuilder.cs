using System;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSorter
{
    class NodeListBuilder
    {
        private Dictionary<Object, Tuple<List<Object>, List<Object>>> _givenDict;
        private Dictionary<Object, List<Object>> _sortingDict;
        private bool _isListingDependents;

        public NodeListBuilder(Dictionary<Object, List<Object>> relationalDictionary, bool isListingDependents = true)
        {
            this._sortingDict = relationalDictionary;
            this._isListingDependents = isListingDependents;
        }
        public NodeListBuilder(Dictionary<Object, Tuple<List<Object>, List<Object>>> relationalDictionary)
        {
            this._givenDict = relationalDictionary;

            int listsStatusID = CheckLists(_givenDict);

            switch (listsStatusID)
            {
                case 1:
                case 3:
                    _sortingDict = NullOrEmptyListRemover(_givenDict, 1);
                    _isListingDependents = true;
                    break;
                case 2:
                    _sortingDict = NullOrEmptyListRemover(_givenDict, 2);
                    _isListingDependents = false;
                    break;
                default:
                    throw new Exception();
            }
        }

        public NodeList BuildList()
        {
            var nodeList = new NodeList(_isListingDependents);

            foreach (Object key in _sortingDict.Keys)
            {
                nodeList.Add(new Node(key));
            }

            foreach (Object key in _sortingDict.Keys)
            {
                var neighbors = new List<Node>();

                var mainNode = nodeList.Nodes.Single(n => n.Identifier.Equals(key));

                foreach (Object obj in _sortingDict[key])
                {
                    var node = nodeList.Nodes.Single(n => n.Identifier.Equals(obj));
                    mainNode.Neighbors.Add(node);
                }
            }

            return nodeList;
        }

        private int CheckLists(Dictionary<Object, Tuple<List<Object>, List<Object>>> basicRelationships)
        {
            int statusID = 0;
            bool isList1NoE = true;
            bool isList2NoE = true;

            foreach (var entry in basicRelationships)
            {
                if (isList1NoE && !entry.Value.Item1.NullOrEmpty())
                {
                    isList1NoE = false;
                    statusID += 1;
                }

                if (isList2NoE && !entry.Value.Item2.NullOrEmpty())
                {
                    isList2NoE = false;
                    statusID += 2;
                }
            }

            return statusID;
        }

        private Dictionary<Object, List<Object>> NullOrEmptyListRemover(
            Dictionary<Object, Tuple<List<Object>, List<Object>>> originalDictionary,
            int tupleItemNumber)
        {
            var newDictionary = new Dictionary<Object, List<Object>>();

            foreach (var entry in originalDictionary)
            {
                var key = entry.Key;
                List<Object> value = null;

                if (tupleItemNumber == 1)
                    value = entry.Value.Item1;
                if (tupleItemNumber == 2)
                    value = entry.Value.Item2;

                newDictionary.Add(key, value);
            }

            return newDictionary;
        }
    }
}
