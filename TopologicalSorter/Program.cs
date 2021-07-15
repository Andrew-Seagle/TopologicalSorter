using System;
using System.Collections.Generic;

namespace TopologicalSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var testin = new Dictionary<object, Tuple<List<object>, List<object>>>();
            var testlist = new List<object>();
            testlist.Add("l;asdfjkd");
            testlist.Add("test");
            var nulllist = new List<object>() { null };
            var testtup = new Tuple<List<object>, List<object>>(testlist, nulllist);

            testin.Add("test", testtup);

            var builder = new NodeListBuilder(testin);
            var nodeList = builder.BuildList();

            var keyArray = new object[] { "two", "three", "five", "seven", "eight", "nine", "ten", "eleven" };

            var arraytest = new int[,] {
                {0,0,0,0,0,0,0,1},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,1,0,1,0,0,0,0},
                {0,0,0,0,1,0,0,1},
                {0,1,0,0,0,0,0,1},
                {0,0,1,1,0,0,0,0},
            };

            var arraytestR = new int[,] {
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,1,0,1,0},
                {0,0,0,0,0,0,0,1},
                {0,0,0,0,1,0,0,1},
                {0,0,0,0,0,1,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {1,0,0,0,0,1,1,0},
            };

            var builderarr = new NodeListBuilder(arraytestR, keyArray, false);
            var nodeListarr = builderarr.BuildList();

            var final = Sorter.TopologicalSort(nodeListarr);

            int i = 0;
        }
    }
}
