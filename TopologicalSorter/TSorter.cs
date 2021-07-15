using System;
using System.Collections.Generic;
using System.Linq;

namespace TopologicalSorter
{
    public static class TSorter
    {
        public static object[] Sort(Dictionary<object, List<object>> relationalDictionary, 
                                    bool isListingDependents = true)
        {
            var sortedObjects = CallBuilderAndSorter(relationalDictionary, isListingDependents);

            return sortedObjects;
        }

        public static object[] Sort(int[,] incidentArray, 
                                    object[] keyArray = null, 
                                    bool isListingDependents = true)
        {
            var sortingDict = new Dictionary<object, List<object>>();

            int arraySize = incidentArray.GetLength(1);

            if (keyArray == null)
            {
                var temparr = Enumerable.Range(0, arraySize).ToArray();
                keyArray = Array.ConvertAll<int, object>(temparr, x => (object)x);
            }

            for (int i = 0; i < incidentArray.GetLength(1); i++)
            {

                var key = keyArray[i];
                var list = new List<object>();

                for (int j = 0; j < incidentArray.GetLength(0); j++)
                {
                    if (incidentArray[j, i] != 0)
                        list.Add(keyArray[j]);
                }

                sortingDict.Add(key, list);
            }

            var sortedObjects = CallBuilderAndSorter(sortingDict, isListingDependents);

            return sortedObjects;
        }

        public static object[] Sort(Dictionary<object, 
                                    Tuple<List<object>, 
                                    List<object>>> relationalDictionary)
        {
            var sortingDict = new Dictionary<object, List<object>>();
            var givenDict = relationalDictionary;
            bool hasDependents;

            int listsStatusID = CheckLists(givenDict);

            switch (listsStatusID)
            {
                case 1:
                case 3:
                    sortingDict = NullOrEmptyListRemover(givenDict, 1);
                    hasDependents = true;
                    break;
                case 2:
                    sortingDict = NullOrEmptyListRemover(givenDict, 2);
                    hasDependents = false;
                    break;
                default:
                    throw new Exception();
            }

            var sortedObjects = CallBuilderAndSorter(sortingDict, hasDependents);

            return sortedObjects;
        }

        private static object[] CallBuilderAndSorter(Dictionary<object,
                                                     List<object>> sortingDictionary,
                                                     bool hasDependents)
        {
            var builder = new NodeListBuilder(sortingDictionary, hasDependents);
            builder.BuildList();

            var sortedObjects = MainSorter.TopologicalSort(builder.NodeList);

            return sortedObjects;
        }

        private static int CheckLists(Dictionary<object, 
                                      Tuple<List<object>, 
                                      List<object>>> basicRelationships)
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

        private static Dictionary<object, List<object>> NullOrEmptyListRemover(Dictionary<object, 
                                                                               Tuple<List<object>,
                                                                               List<object>>> originalDictionary,
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
