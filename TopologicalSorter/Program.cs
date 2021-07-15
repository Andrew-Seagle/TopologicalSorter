using System;
using System.Collections.Generic;

namespace TopologicalSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var testin = new Dictionary<Object, Tuple<List<Object>, List<Object>>>();
            var testlist = new List<Object>();
            testlist.Add("l;asdfjkd");
            testlist.Add("test");
            var nulllist = new List<Object>() { null };
            var testtup = new Tuple<List<Object>, List<Object>>(testlist, nulllist);

            testin.Add("test", testtup);

            var builder = new NodeListBuilder(testin);
            var nodeList = builder.BuildList();

            var keyArray = new Object[] { "two", "three", "five", "seven", "eight", "nine", "ten", "eleven" };

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

            var builderarr = new NodeListBuilder(arraytest, keyArray);
            var nodeListarr = builderarr.BuildList();

            int i = 0;
        }
    }
}
