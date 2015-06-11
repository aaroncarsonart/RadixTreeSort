using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadixTreeSort;
using System.Diagnostics;

namespace RadixTreeSort
{
    /// <summary>
    /// The entry point for our program.
    /// </summary>
    public class Program
    {

        public const int ARRAY      = 1;
        public const int LSD        = 2;
        public const int SEQUENTIAL = 3;
        public const int PARALLEL   = 4;
        public const int P_IMPROVED = 5;

        public static readonly int[] TYPES = { ARRAY, LSD, SEQUENTIAL, PARALLEL, P_IMPROVED };


        /// <summary>
        /// run a single test.
        /// </summary>
        /// <returns>The running time, in seconds.</returns>
        private static double RunTest(int[] array, int type)
        {
            // timer for measuring running time
            Stopwatch timer = new Stopwatch();

            // copy array contents to allow a repeatable test on the same data.
            int[] arrayCopy = new int[array.Length];
            array.CopyTo(arrayCopy, 0);
            
            // select the algorithm to run
            switch (type)
            {
                case ARRAY:
                    Console.Write("Array.Sort({0:N0}): ", array.Length);
                    timer.Start();
                    Array.Sort(arrayCopy);
                    timer.Stop();
                    break;

                case LSD:
                    Console.Write("LSDRadixSort.Run({0:N0}): ", array.Length);
                    timer.Start();
                    arrayCopy = LSDRadixSort.RunBinary(arrayCopy);
                    timer.Stop();
                    //Console.WriteLine(Utility.ArrayContentsToString(arrayCopy));
                    break;

                case SEQUENTIAL:
                    Console.Write("SequentialRadixTreeSort.Run({0:N0}): ", array.Length);
                    timer.Start();
                    SequentialRadixTreeSort.Run(arrayCopy);
                    timer.Stop();
                    break;
                case PARALLEL:
                    Console.Write("  ParallelRadixTreeSort.Run({0:N0}): ", array.Length);
                    timer.Start();
                    ParallelRadixTreeSort.Run(arrayCopy);
                    timer.Stop();
                    break;

                case P_IMPROVED:
                    Console.Write("  ParallelRadixTreeSort.RunImproved({0:N0}): ", array.Length);
                    timer.Start();
                    ParallelRadixTreeSort.RunImproved(arrayCopy);
                    timer.Stop();
                    break;
            }
            double time = timer.ElapsedMilliseconds / 1000.0;
            Console.WriteLine("{0:N3} seconds", time);
            return time;
        }

        /// <summary>
        /// Run tests, comparing each kind of sorting algorithm.
        /// </summary>
        public static void TestSortingAlgorithms(int[] types, int arraySize, int testsPerAlgorithm)
        {
            // specify test parameters
            int maxValue = Int32.MaxValue;

            // create the array
            int[] array = Utility.GetRandomArray(arraySize, maxValue);

            // ***********************************
            // run tests for each algorithm type
            // ***********************************
            foreach (int type in types)
            {
                double times = 0.0;
                // run the specified number of tests
                for (int i = 0; i < testsPerAlgorithm; i++)
                {
                    if (testsPerAlgorithm > 1)
                    {
                        Console.Write("{0,2}. ", i + 1);
                    }
                    times += RunTest(array, type);
                }

                // separate tests and report average running time (if more than 1 test per algorithm)
                if (testsPerAlgorithm > 1)
                {
                    double averageTime = times / testsPerAlgorithm;
                    Console.WriteLine("average time: {0:N3} seconds", averageTime);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Run tests, comparing each kind of sorting algorithm.
        /// </summary>
        public static void TestSortingAlgorithms(int arraySize, int testsPerAlgorithm)
        {
            TestSortingAlgorithms(TYPES, arraySize, testsPerAlgorithm);
        }

        /// <summary>
        /// Test the LSDRadixStringSort.
        /// </summary>
        /// <param name="wordCount">The number of words to test.</param>
        /// <param name="wordSize">The size of each word.</param>
        public static void TestLSDRadixStringSort(int wordCount, int wordSize)
        {
            string[] strings = LSDRadixSort.GetRandomStringArray(wordCount, wordSize);
            LSDRadixSort.RunTest(strings, wordSize);
        }

        /// <summary>
        /// Run the program.
        /// </summary>
        /// <param name="args">Does nothing.</param>
        public static void Main(string[] args)
        {
            
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("C#");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("");

            TestSortingAlgorithms(new int[]{ SEQUENTIAL,PARALLEL, P_IMPROVED }, 100000, 10);
            //TestLSDRadixStringSort(100000, 32);

            //Parallel.For(0, 10, p => Console.WriteLine(p));

            Console.Write("Tests complete.  Press any key to continue...");
            Console.ReadLine();
        }
    }
}
