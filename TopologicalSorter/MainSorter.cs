using System;
using System.Linq;
using System.Collections.Generic;

namespace TopologicalSorter
{
    internal static class MainSorter
    {
        private static List<Node> _sortedList; 

        internal static object[] TopologicalSort(NodeList nodeList)
        {
            _sortedList = new List<Node>();

            while (true)
            {
                var nextNode = nodeList.Nodes.FirstOrDefault(n => n.SortStatus != Usage.Sorted);

                if (nextNode == null)
                    break;

                SortNode(nextNode);
            }

            var sortedObjectsArray = ExtractObjects(!nodeList.HasDependents);

            return sortedObjectsArray;
        }

        private static void SortNode(Node node)
        {
            if (node.SortStatus == Usage.Sorted)
                return;

            if (node.SortStatus == Usage.Sorting)
                throw new Exception();

            node.SortStatus = Usage.Sorting;

            foreach (Node neighbor in node.Neighbors)
            {
                SortNode(neighbor);
            }

            node.SortStatus = Usage.Sorted;

            _sortedList.Insert(0,node);
        }

        private static object[] ExtractObjects(bool requiresReverse = false)
        {
            var sortedObjects = _sortedList.Select(n => n.Identifier).ToArray();

            if (requiresReverse)
                Array.Reverse(sortedObjects);

            return sortedObjects;
        }
    }
}
