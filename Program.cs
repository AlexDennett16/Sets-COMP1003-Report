using System;

namespace Report
{
    class Program
    {
        int[] set1 = new int[20];
        int[] set2 = new int[20];

        //Returns value with adaptable Capcity modifier
        public static void Clear_Set(ref int[] set)
        {
            set = new int[Capacity(ref set)]; 
        }

        //
        public static bool Is_Empty(ref int[] set)
        {
            int[] emptySet = new int[Capacity(ref set)];
            for (int i = 0; i < Size(ref set); i++)
            {
                if (set[i] != emptySet[i])
                    return false;
            }
            return true;
        }

        public static int Size(ref int[] set)
        {
            for (int i = 0; i < 20; i++)
            {
                if (set[i] == 0)
                    return i;
            }
            return Capacity(ref set);
        }

        public static int Capacity(ref int[] set)
        {
            return set.Length;
        }

        public static bool Is_Element_Of(ref int[] set, int x)
        {
            for (int i = 0; i < Size(ref set); i++)
                if (x == set[i])
                    return true;

            return false;
        }

        public static void Print(ref int[] set)
        {
            if (Is_Empty(ref set) == true)
            {
                Console.WriteLine("There are no elements in this array!\n\n");
                return;
            }
                
            for (int i = 0; i < Size(ref set); i++)
                Console.Write(set[i] + ",");
            Console.WriteLine("\n\n");
        }   


        /*
         * PART B
         * These add and remove commands are build around keeping a set contiguous
         * A set should be of form {1,3,5,2,4,0,0,0} where 0 are null values and not counted as prt of the set
         * They remove the need for a sorting algorithm after every deletion to check the block is together
         */
        

        //If x doesnt exist in the set, and the set is not at capacity, we can add x to the end
        public static void Add(ref int[] set, int x)
        {
            if (x <= 0)
                Console.WriteLine("You are trying to add an invalid element! Negatives and Zero are not allowed!");

            else if (Is_Element_Of(ref set, x) == false && (Size(ref set) < Capacity(ref set)))
                set[Size(ref set)] = x;
        }

        /* First check if element in array, if not abort removal
         * Then if array is size 1, we can just remove the only entry
         * Otherwise we replace the deleted entry with the last entry (set[sizeOfSet])
         */
        public static void Remove(ref int[] set, int x)
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

        //We iterate over the given array, creating a new array from it, with same elements
        //We only copy from Size, as the rest of elements will be 0's
        public static int[] Copy_Set(ref int[] set)
        {
            int[] copyset = new int[Capacity(ref set)];

            for (int i = 0; i < Size(ref set); i++)
                copyset[i] = set[i];

            return copyset;
        }


        //Checks eevry number in set1 to the entirety of set2, if we find any non-matching entries, return false
        //otherwise all entries in set1 exist in set2, and it is a true subset
        public static bool Is_Subset(ref int[] set1, ref int[] set2)
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
        public static Tuple<int[], int[]> Size_Of_Two_Arrays(ref int[] set1, ref int[] set2)
        {
            int set1Length = Size(ref set1);
            int set2Length = Size(ref set2);

            int[] bigArray = set1Length >= set2Length ? set1 : set2;
            int[] smallArray = set1Length < set2Length ? set1 : set2;

            return Tuple.Create(bigArray, smallArray);
        }

        /*
         * Grab tuple of largest and smallest array, then iterate over largest of the two
         * We then iterate over every element in the larger array, and comparing it to the entire smaller array, to check if it is present
         * if it is we add it to the newly created unionArray, which is initialised to capacity of bigArray, to prevent overflows
         */
        public static int[] Intersection(ref int[] set1, ref int[] set2)
        {
            Tuple<int[], int[]> arrayHolder = Size_Of_Two_Arrays(ref set1, ref set2);
            var (bigArray, smallArray) = arrayHolder;

            int[] unionArray = new int[Capacity(ref bigArray)];
            for (int i = 0; i < Size(ref bigArray); i++)
            {
                if (Is_Element_Of(ref smallArray, bigArray[i]) == true)
                    unionArray[Size(ref unionArray)] = bigArray[i];
            }
            return unionArray;
        }


        /* 
         * Grab tuple of largest and smallest array, then iterate over largest of the two
         * If it exists in bigarray and not in small array we add it to diffArray
         * If it is in both, we remove it from small array
         * then after full iteration add what is left from small array as these are exclusive to small array
         */          
        public static int[] Symmetric_Difference(ref int[] set1, ref int[] set2)
        {
            Tuple<int[], int[]> arrayHolder = Size_Of_Two_Arrays(ref set1, ref set2);
            var (bigArray, smallArray) = arrayHolder; 

            int[] diffArray = new int[Capacity(ref bigArray)];

            for (int i = 0; i < Size(ref bigArray); i++)
                if (Is_Element_Of(ref smallArray, bigArray[i]) == false)
                    diffArray[Size(ref diffArray)] = bigArray[i];

            for (int i = 0; i < Size(ref smallArray); i++)
                if (Is_Element_Of(ref bigArray, smallArray[i]) == false)
                    diffArray[Size(ref diffArray)] = smallArray[i];


            return diffArray;
        }






        static void Main(string[] args)
        {


            int[] set1 = new int[20];
            int[] set2 = new int[20];


            //Testing empty Array
            Console.WriteLine("Blow should give empty array message");
            Print(ref set1);

            //Testing clear set
            Add(ref set1, 100);
            Clear_Set(ref set1);
            Console.WriteLine("\nBelow Should give empty set");
            Print(ref set1);


            //Set1 (10, 20, 30, 40, 50, 60) Set2 (10, 30, 50, 70)
            Add(ref set1, 10);
            Add(ref set1, 20);
            Add(ref set1, 30);
            Add(ref set1, 40);
            Add(ref set1, 50);
            Add(ref set1, 60);

            Add(ref set2, 10);
            Add(ref set2, 30);
            Add(ref set2, 50);
            Add(ref set2, 70);
            Add(ref set2, 15);

            //Testing Add and Print
            Console.WriteLine("\nExpected 10, 30, 50, 70, 15");
            Print(ref set2);

            //Testing remove
            Console.WriteLine("\nExpected 10, 30, 50, 70, ");
            Remove(ref set2, 15);
            Print(ref set2);


            Console.WriteLine("Size of set is should be 6, capacity should be 20 but adaptable\nSize is " + Size(ref set1) + " and Capacity is " + Capacity(ref set1));




            Console.WriteLine("\nThe below should resolve to false as neither are subsets\n" + Is_Subset(ref set1, ref set2));

            
            int[] intersec = Intersection(ref set1, ref set2);
            int[] symdiff = Symmetric_Difference(ref set1, ref set2);

            Console.WriteLine("\nThe below should print 10, 30, 50 with current add inputs - Intersection");
            Print(ref intersec);

            Console.WriteLine("\nThe below should print 20, 40, 60, 70 with current add inputs - Symmetric Difference");
            Print(ref symdiff);
            


        }
    }
}
