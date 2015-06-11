using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixTreeSort
{
    /// <summary>
    /// Plain-old, sequential LSD Radix Sort, using a binary string representation of integers.
    /// </summary>
    class LSDRadixSort
    {
        /// <summary>
        /// Run the LSD Radix Sort on the binary representation of a 32-bit integer array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int[] RunBinary(int[] values)
        {
            // step 1: convert values to binary strings.
            String[] a = values.Select(i => Convert.ToString(i, 2).PadLeft(32, '0')).ToArray();
            int W = 32;

            // run exact method from CS 311 Algorithms book
            int N = a.Length;
            int R = 2;
            string[] aux = new string[N];

            for (int d = W - 1; d >= 0; d--)
            {
                // compute frequency count
                int[] count = new int[R + 1];
                for (int i = 0; i < N; i++)
                {
                    int index = (int)Char.GetNumericValue(a[i], d) + 1;
                    count[index]++;
                }

                // transform counts to indices
                for (int r = 0; r < R; r++)
                {
                    count[r + 1] += count[r];
                }

                // distribute.
                for (int i = 0; i < N; i++)
                {
                    int index = (int)Char.GetNumericValue(a[i], d);
                    aux[count[index]++] = a[i];
                }

                // copy back.
                for (int i = 0; i < N; i++)
                {
                    a[i] = aux[i];
                }
            }

            // convert strings back to integers.
            return a.Select(s => Convert.ToInt32(s, 2)).ToArray();
        }

        /// <summary>
        /// Run the LSD Radix Sort from Sedgewick/Wayne Algorithms book.
        /// This assumes that all Strings are of equal length.
        /// </summary>
        /// <param name="values">The array of strings to run on.</param>
        /// <param name="W">Number of leading characters to sort on</param>
        /// <returns></returns>
        public static void Run(string[] a, int W)
        {
            int N = a.Length;
            int R = 256;
            string[] aux = new string[N];

            for (int d = W - 1; d >= 0; d--)
            {
                // compute frequency count
                int[] count = new int[R + 1];
                for (int i = 0; i < N; i++)
                {
                    int index = a[i][d] + 1;
                    count[index]++;
                }

                // transform counts to indices
                for (int r = 0; r < R; r++)
                {
                    count[r + 1] += count[r];
                }

                // distribute.
                for (int i = 0; i < N; i++)
                {
                    int index = a[i][d];
                    aux[count[index]++] = a[i];
                }

                // copy back.
                for (int i = 0; i < N; i++)
                {
                    a[i] = aux[i];
                }
            }
        }


        public static void RunBinaryTest(int[] array)
        {
            // make local copy to preserve original array
            int[] arrayCopy = new int[array.Length];
            array.CopyTo(arrayCopy, 0);


            Console.WriteLine("LSDRadixSort.Run()");
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Initial:\n{0}", Utility.ArrayContentsToString(arrayCopy));

            timer.Start();
            arrayCopy = RunBinary(arrayCopy);
            timer.Stop();

            Console.WriteLine("Sorted:\n{0}", Utility.ArrayContentsToString(arrayCopy));
            Console.WriteLine("Elapsed time: {0:N3}", timer.ElapsedMilliseconds / 1000.0);
        }

        /// <summary>
        /// Generate an array of specified size of random strings of length W.
        /// </summary>
        /// <param name="W">The string length.</param>
        /// <param name="size">The size of the array.</param>
        /// <returns>An array of random strings.</returns>
        public static string[] GetRandomStringArray(int size, int W)
        {
            int wordSize = W;
            string chars = "abcdefghijklmnopqrstuvwxyz";
            Random r = new Random();
            string[] values = new string[size];
            for(int s = 0; s < size; s++){
                char[] c = new char[W];
                for (int w = 0; w < W; w++){
                    c[w] = chars[r.Next(chars.Length)];
                }
                values[s] = new String(c);
            }

            return values;
        }

        public static void RunTest(string[] array, int W)
        {
            // make local copy to preserve original array
            string[] arrayCopy = new string[array.Length];
            array.CopyTo(arrayCopy, 0);


            Console.WriteLine("LSDRadixSort.Run({0:N0})", array.Length);
            Stopwatch timer = new Stopwatch();
           // Console.WriteLine("Initial:\n{0}", Utility.ArrayContentsToString(arrayCopy));

            timer.Start();
            Run(arrayCopy, W);
            timer.Stop();

            //Console.WriteLine("Sorted:\n{0}", Utility.ArrayContentsToString(arrayCopy));
            Console.WriteLine("Elapsed time: {0:N3} seconds", timer.ElapsedMilliseconds / 1000.0);
        }
    }
}
