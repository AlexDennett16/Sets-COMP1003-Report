using System;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;

namespace Report
{
    class Program
    {
        int[] set1 = new int[20];
        int[] set2 = new int[20];

        public void Clear_Set(ref int[] set)
        {
            set = new int[set.Length]; 
        }


        public bool Is_Empty(ref int[] set)
        {
            int[] emptySet = new int[set.Length];
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i] != emptySet[i])
                    return false;
            }
            return true;
        }

        public int Size(ref int[] set)
        {
            for (int i = 0; i < 20; i++)
            {
                if (set[i] == 0)
                    return i;
            }
            return 20;
        }

        public int Capacity(ref int[] set)
        {
            return set.Length;
        }

        public bool Is_Element_Of(ref int[] set, int x)
        {
            foreach (int y in set)
            {
                if (x == y) 
                    return true;
            }
            return false;
        }

        public void Print(ref int[] set)
        {
            if (Is_Empty(ref set) == true)
            {
                Console.WriteLine("There are no elements in this array!\n\n");
                return;
            }
                
            for (int i = 0; i < Size(ref set); i++)
                Console.WriteLine(set[i]);
            Console.WriteLine("\n\n");
        }   


        /*
         * PART B
         * These add and remove commands are build around keeping a set contiguous
         * A set should be of form {1,3,5,2,4,0,0,0} where 0 are null values and not counted as prt of the set
         * They remove the need for a sorting algorithm after every deletion to check the block is together
         */
        

        //If x doesnt exist in the set, and the set is not at capacity, we can add x to the end
        public void Add(ref int[] set, int x)
        {
            if (Is_Element_Of(ref set, x) == false && (Size(ref set) < Capacity(ref set)))
                set[Size(ref set)] = x;
        }

        /* First check if element in array, if not abort removal
         * Then if array is size 1, we can just remove the only entry
         * Otherwise we replace the deleted entry with the last entry (set[sizeOfSet])
         */
        public void Remove(ref int[] set, int x)
        {
            if (Is_Element_Of(ref set, x) == false)
                return;

            int sizeOfSet = Size(ref set);
            if (sizeOfSet == 1)
            {
                set[0] = 0;
                return;
            }

            for (int i = 0; i < sizeOfSet; i++)
            {
                if (set[i] == x)
                {
                    set[i] = set[sizeOfSet];
                    set[sizeOfSet] = 0;
                }

            }
        }

        //PART C

        //Set is passed by value in this example, therefore returning it will return a value-copied but different array
        public int[] Copy_Set(ref int[] set)
        {
            int[] copyset = new int[Capacity(ref set)];

            for (int i = 0; i < Capacity(ref set); i++)
                copyset[i] = set[i];

            return copyset;
        }


        //Checks eevry number in set1 to the entirety of set2, if we find any non-matching entries, return false
        //otherwise all entries in set1 exist in set2, and it is a true subset
        public bool Is_Subset(ref int[] set1, ref int[] set2)
        {
            for (int i = 0; i < Size(ref set1); i++)
            {
                if (Is_Element_Of(ref set2, set1[i]) == false)
                    return false;
            }
            return true;
        }

        //Takes in 2 reference arrays and takes the smaller and larger of the 2
        //Putting larger first, and smaller second, only used in 2 functions below
        public Tuple<int[], int[]> Size_Of_Two_Arrays(ref int[] set1, ref int[] set2)
        {
            int set1Length = Size(ref set1);
            int set2Length = Size(ref set2);

            int[] bigArray = set1Length >= set2Length ? set1 : set2;
            int[] smallArray = set1Length < set2Length ? set1 : set2;

            Print(ref bigArray);
            Print(ref smallArray);

            return Tuple.Create(bigArray, smallArray);
        }

        /*
         * Grab tuple of largest and smallest array, then iterate over largest of the two
         * We then iterate over every element in the larger array, and comparing it to the entire smaller array, to check if it is present
         * if it is we add it to the newly created unionArray, which is initialised to capacity of bigArray, to prevent overflows
         */
        public int[] Intersection(ref int[] set1, ref int[] set2)
        {
            Tuple<int[], int[]> arrayHolder = Size_Of_Two_Arrays(ref set1, ref set2);
            var (bigArray, smallArray) = arrayHolder;

            Print(ref bigArray);
            Print(ref smallArray);

            int[] unionArray = new int[Capacity(ref bigArray)];
            for (int i = 0; i < Size(ref bigArray); i++)
            {
                if (Is_Element_Of(ref smallArray, bigArray[i]) == true)
                {
                    Print(ref unionArray);
                    unionArray[i] = bigArray[i];
                } 
            }
            return unionArray;
        }


        /* 
         * Grab tuple of largest and smallest array, then iterate over largest of the two
         * If it exists in bigarray and not in small array we add it to diffArray
         * If it is in both, we remove it from small array
         * then after full iteration add what is left from small array as these are exclusive to small array
         */          
        public int[] Symmetric_Difference(ref int[] set1, ref int[] set2)
        {
            Tuple<int[], int[]> arrayHolder = Size_Of_Two_Arrays(ref set1, ref set2);
            var (bigArray, smallArray) = arrayHolder; 

            int[] diffArray = new int[Capacity(ref bigArray)];
            for (int i = 0; i < Size(ref bigArray); i++)
            {
                if (Is_Element_Of(ref smallArray, bigArray[i]) == true)
                    diffArray[i] = bigArray[i];
                else
                    smallArray[i] = 0;
            }

            int ptr = Size(ref diffArray); 
            for (int i = 0; i < Size(ref smallArray); i++)
            {
                diffArray[ptr + i] = smallArray[i];
            }

            return diffArray;
        }






        static void Main(string[] args)
        {


            int[] set1 = new int[20];
            int[] set2 = new int[20];

            Program p = new Program();

            //Testing empty Array
            //p.Print(ref set1);

            //Set1 (10, 20, 0, 0...) Set2 (10, 0, 0, 0...)
            p.Add(ref set1, 10);
            p.Add(ref set1, 20);
            p.Add(ref set1, 30);
            p.Add(ref set1, 40);
            p.Add(ref set1, 50);
            p.Add(ref set1, 60);

            p.Add(ref set2, 10);
            p.Add(ref set2, 30);
            p.Add(ref set2, 50);
            p.Add(ref set2, 70);


            
            int[] intersec = p.Intersection(ref set1, ref set2);
            //int[] symdiff = p.Symmetric_Difference(ref set1, ref set2);

            p.Print(ref intersec);

            //p.Print(ref symdiff);
            


        }
    }
}
