using System;
using System.Collections.Generic;

namespace TopologicalSorter
{
    class NodeListBuilder
    {
        private Dictionary<Object, Tuple<List<Object>, List<Object>>> _givenDict;

        public NodeListBuilder(Dictionary<Object, List<Object>> basicRelationships, bool isListingDependents = true)
        {

        }
        public NodeListBuilder(Dictionary<Object, Tuple<List<Object>, List<Object>>> basicRelationshipsDictionary)
        {
            this._givenDict = basicRelationshipsDictionary;
            int listsStatusID = CheckLists(_givenDict);
            switch (listsStatusID)
            {
                case 0:
                    throw new Exception();
                    break;
                case 1:

            }
                
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
            Dictionary<Object, Tuple<List<Object>, List<Object>>> originalDict)
        {
            var newDict = new Dictionary<Object, List<Object>>();

            return newDict;
        }
    }
}
