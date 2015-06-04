using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadixTreeSort.Sequential;

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
            int[] array = Utility.GetRandomArray(size, maxValue);
            SequentialRadixTreeSort.Run(array);
        }

        /// <summary>
        /// Run the program.
        /// </summary>
        /// <param name="args">Does nothing.</param>
        public static void Main(string[] args)
        {
           System.Console.SetBufferSize(200,1000);   // make sure buffer is bigger than window
           System.Console.SetWindowSize(120,40);   //set window size to almost full screen
           System.Console.SetWindowPosition(0,0);   // sets window position to upper left

           Program p = new Program(1, 400, Int32.MaxValue);

           Console.ReadLine();
        }
    }
}
