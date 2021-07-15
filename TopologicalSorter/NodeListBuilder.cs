using System;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSorter
{
    class NodeListBuilder
    {
        private Dictionary<object, Tuple<List<object>, List<object>>> _givenDict;
        private Dictionary<object, List<object>> _sortingDict;
        private bool _isListingDependents;

        public NodeListBuilder(int[,] array, object[] keyArray = null, bool isListingDependents = true)
        {
            this._isListingDependents = isListingDependents;
            _sortingDict = new Dictionary<object, List<object>>();

            int arraySize = array.GetLength(1);

            if (keyArray == null)
            {
                var temparr = Enumerable.Range(0, arraySize).ToArray();
                keyArray = Array.ConvertAll<int, object>(temparr, x => (object)x);
            }

            for (int i = 0; i < array.GetLength(1); i++)
            {
                
                var key = keyArray[i];
                var list = new List<object>();

                for (int j = 0; j < array.GetLength(0); j++)
                {
                    if (array[j, i] != 0)
                        list.Add(keyArray[j]);
                }

                _sortingDict.Add(key, list);
            }
        }

        public NodeListBuilder(Dictionary<object, List<object>> relationalDictionary, bool isListingDependents = true)
        {
            this._sortingDict = relationalDictionary;
            this._isListingDependents = isListingDependents;
        }
        public NodeListBuilder(Dictionary<object, Tuple<List<object>, List<object>>> relationalDictionary)
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

            return nodeList;
        }

        private int CheckLists(Dictionary<object, Tuple<List<object>, List<object>>> basicRelationships)
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

        private Dictionary<object, List<object>> NullOrEmptyListRemover(
            Dictionary<object, Tuple<List<object>, List<object>>> originalDictionary,
            int tupleItemNumber)
        {
            var newDictionary = new Dictionary<object, List<object>>();

            foreach (var entry in originalDictionary)
            {
                var key = entry.Key;
                List<object> value = null;

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
