
using System;
using System.Collections.Generic;

namespace Report
{
    class Program
    {
        List<int> S = new List<int>();
        List<int> T = new List<int>();

        public void Clear_Set()
        {
            S = new List<int>();
        }


        public bool Is_Empty()
        {
            if (S.Count == 0)
                return true;
            else
                return false;
        }

        public int Size()
        {
            return S.Count;
        }

        public int Capacity()
        {
            return S.Capacity;
        }

        public bool Is_Element_Of(int x)
        {
            if (S.Contains(x))
                return true;
            else
                return false;
        }

        public void Print()
        {
            for (int i = 0; i < S.Count; i++)
                Console.WriteLine(S[i]);
        }

        //If x doesnt exist in S, we add it
        public List<int> Add(int x)
        {
            if (!S.Contains(x))
            {
                S.Add(x);
                return S;
            }
            else
                return S;
        }

        //We check if list contains x, if it does, we delete the entry from S
        public List<int> Remove(int x)
        {
            if (S.Contains(x))
                S.Remove(x);
            return S;
        }

        //Returns a copied, by value, list of S
        public List<int> Copy_Set(List<int> S)
        {
            return new List<int>(S);
        }


        //Checks eevry number in S, if we find any non-matching entries,
        //return false else returns true as S is in T
        public bool Is_Subset(List<int> S, List<int> T)
        {
            foreach(int testnum in S)
            {
                if (!T.Contains(testnum))
                    return false;
            }
            return true;
        }
        /*<variables>
         * bigList = Int List that holds the larger of given lists
         * smallList = Int List that holds the smaller ~
         * unionList = Int List, intialised but not filled
         *</variables>
         *<summary>
         * Iterates over every integar entry in bigList testing if it is
         * in the smallList, if it is we add to our union list. Else
         * do nothing. Then returns this union list.
         *</summary> 
         */
        public List<int> Intersection(List<int> S, List<int> T)
        {
            List<int> bigList = S.Count > T.Count ? S : T;
            List<int> smallList = S.Count < T.Count ? S : T;
            List<int> unionList = new List<int>();
            foreach (int testnum in bigList)
            {
                if (smallList.Contains(testnum))
                    unionList.Add(testnum);
            }
            return unionList;
        }


        /*<summary>
         * Creates an empty list, difflist, then iterates over S.
         * If entry in S is unique, it is added to our difflist, if not
         * we remove the duplicate copy from T. Once fully iterated
         * Adds all unique entries from T then returns the Unique entries
         * list 
         *</summary>
         */          
        public List<int> Symmetric_Difference(List<int> S, List<int> T)
        {
            List<int> diffList = new List<int>();

            foreach(int testnum in S)
            {
                if (T.Contains(testnum) == false)
                    diffList.Add(testnum);
                else
                    T.Remove(testnum);
            }

            diffList.AddRange(T);
            return diffList;
        }


        static void Main(string[] args)
        {


            List<int> S = new List<int>();
            List<int> T = new List<int>();


            Console.WriteLine("Hello World!");
        }
    }
}
