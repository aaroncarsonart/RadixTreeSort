using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixTreeSort
{
    /// <summary>
    /// Sequential defines a sequentail RadixTreeSort in the Sort() method.
    /// </summary>
    public class ParallelRadixTreeSort
    {
        /// <summary>
        /// Run a sequential RadixTreeSort over an array of integers.
        /// </summary>
        /// <param name="values">An array of integers to sort.</param>
        /// <returns>The sorted array.</returns>
        public static int[] Run(int[] values)
        {
            Console.WriteLine("ParallelRadixTreeSort.Run()");
            Console.WriteLine("\ninitial values:\n{0}\n", Utility.ArrayContentsToString(values));

            // initialize the root of the tree.
            Node[] root = Node.NewSet();
            int bitCount = 8 * sizeof(int);
            int initialPosition = bitCount - 1;

            // **********************************************************************
            // 1. put each integer into the tree.
            // **********************************************************************
            System.Threading.Tasks.Parallel.For(0, values.Length, p =>
            {
                int value = values[p];
                Put(root, value, initialPosition);
            });

            // **********************************************************************
            // 2. Retrieve each integer in-order and place into values.
            // **********************************************************************
            
            System.Threading.Tasks.Parallel.For(0, values.Length,p =>
            {
                values[p] = Get(root, p + 1, new int[bitCount], initialPosition, 0);
            });
            
            Console.WriteLine("\nsorted values:\n{0}\n", Utility.ArrayContentsToString(values));
            return values;
        }

        /// <summary>
        /// Put is a helper method that recursively inserts an integer one bit at a time into a binary tree.
        /// it stops running after 32 iterations (or after sizeof(int)).
        /// </summary>
        /// <param name="current">The current child of the tree.</param>
        /// <param name="value">The integer value being inserted.</param>
        /// <param name="position"></param>
        public static void Put(Node[] current, int value, int position)
        {
            // 1. get the current bit (0 or 1)
            int bit = (value >> position) & 1;
            //Console.Write(bit + (position %4 == 0 ? " " : ""));

            // 4. increment the appropriate count.
            current[bit].Count += 1;

            // 2. initialize Next if null
            current[bit].Next = current[bit].Next ?? Node.NewSet();

            // 3. if position is not zero, recurse with the position of next bit.
            if (position != 0)
            {
                Put(current[bit].Next, value, position - 1);
            }

        }

        /// <summary>
        /// Helper method to get A value in ascending order from a tree.
        /// </summary>
        /// <param name="current">The current child node of the tree.</param>
        /// <param name="bits">The array bits that have so far been retrieved.</param>
        /// <param name="bitPosition">The index of the current bit being retrieved.</param>
        /// <param name="sortedPosition">the position in the final sorted order to get the next value for.</param>
        /// <param name="zeroCount">The cumulative sum of current[0].Count</param>
        public static int Get(Node[] current, int sortedPosition, int[] bits, int bitPosition, int zeroCount)
        {
            // 1. increment zeroCount.
            zeroCount = current[0].Count;

            // 2. if the sortPosition is less than the zeroCount, next bit is zero; otherwise, it is one.
            int bit;

            if (sortedPosition <= zeroCount)
            {
                bit = 0;                
            }
            else
            {
                bit = 1;
                sortedPosition -= zeroCount;
            }

            // set the bit.
            bits[bitPosition] = bit;

            // report results.
            List<int> tmp = bits.ToList().GetRange(bitPosition, bits.Length - bitPosition);
            tmp.Reverse();
            
            /*
            Console.WriteLine("sortedPosition: {0,3} bits: {1,-40} bitPosition: {2,3} zeroCount: {3,3} Bit: {4}",
                sortedPosition,
                Utility.AddSpacing(String.Concat(tmp)),
                bitPosition,
                zeroCount,
                bit);
            */
            // add newline every 4 lines for easy reading.
            //if (bitPosition % 4 == 0) Console.WriteLine();
            
            
            //Console.Write(bit);
            


            // 3. Base Case: if on a leaf node, convert bits to an Int32 and return.
            if (bitPosition == 0)
            {
                String result = String.Concat(bits.Reverse());
                return Convert.ToInt32(result, 2);
            }

            // 4. otherwise, recurse to next bit position.
            else
            {
                return Get(current[bit].Next, sortedPosition, bits, bitPosition - 1, zeroCount);
            }
        }
    }
}
