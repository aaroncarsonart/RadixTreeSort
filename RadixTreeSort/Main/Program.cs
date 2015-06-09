﻿using System;
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
        /// <summary>
        /// Run a set of tests.
        /// </summary>
        /// <param name="tests"></param>
        public Program(int testCount, int listSize, int maxValue)
        {
            for (int i = 1; i <= testCount; i++)
            {
                System.Console.WriteLine("****************************************");
                System.Console.WriteLine("Test #{0}", i);
                System.Console.WriteLine("****************************************");
                RunTest(listSize, maxValue);
            }
        }

        /// <summary>
        /// run a single test.
        /// </summary>
        private void RunTest(int size, int maxValue)
        {
            Stopwatch timer = new Stopwatch();

            int[] array = Utility.GetRandomArray(size, maxValue);
            
            // run control test (systen library)
            int[] cArray = new int[array.Length];
            array.CopyTo(cArray, 0);
            timer.Start();
            Console.WriteLine("Array.Sort()");
            Array.Sort(cArray);
            timer.Stop();
            Console.WriteLine("Elapsed time: {0} seconds\n\n", timer.ElapsedMilliseconds / 1000.0);

            // run sequential test
            int[] sArray = new int[array.Length];
            array.CopyTo(sArray, 0);
            timer.Restart();
            SequentialRadixTreeSort.Run(sArray);
            timer.Stop();
            Console.WriteLine("Elapsed time: {0} seconds\n\n", timer.ElapsedMilliseconds / 1000.0);
            
            // run parallel test
            int[] pArray = new int[array.Length];
            array.CopyTo(pArray, 0);
            timer.Restart();            
            ParallelRadixTreeSort.Run(pArray);
            timer.Stop();
            Console.WriteLine("Elapsed time: {0} seconds\n\n", timer.ElapsedMilliseconds / 1000.0);
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

            Program p = new Program(1, 100000, Int32.MaxValue);
            Console.ReadLine();
        }
    }
}
