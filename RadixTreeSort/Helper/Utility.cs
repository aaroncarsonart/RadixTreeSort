using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixTreeSort
{
    class Utility
    {
        /// <summary>
        /// Test the binary conversion methods.
        /// </summary>
        public static void TestBinaryConversions()
        {
            for (int i = 0; i < 16; i++)
            {
                Console.WriteLine("binary of int {0,2}: {1}", i, IntegerAsBinaryString(i));
            }

            for (char c = 'a'; c <= 'z'; c++)
            {
                Console.WriteLine("binary of char {0}: {1}", c, CharAsBinaryString(c));
            }

            for (byte b = 0; b < 16; b++)
            {
                Console.WriteLine("binary of byte {0,2}: {1}", b, ByteAsBinaryString(b));
            }
        }

        /// <summary>
        /// Example of how to access binary values of an integer.
        /// </summary>
        /// <param name="value"></param>
        public static String IntegerAsBinaryString(int value)
        {
            int bits = 8 * sizeof(int);
            StringBuilder sb = new StringBuilder();
            for (int position = bits - 1; position >= 0; position--)
            {
                int b = GetBit(value, position);
                sb.Append(b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Example of how to access binary values of an integer.
        /// </summary>
        /// <param name="value"></param>
        public static String CharAsBinaryString(char value)
        {
            int bits = 8 * sizeof(char);
            StringBuilder sb = new StringBuilder();
            for (int position = bits - 1; position >= 0; position--)
            {
                int b = GetBit(value, position);
                sb.Append(b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Example of how to access binary values of an integer.
        /// </summary>
        /// <param name="value"></param>
        public static String ByteAsBinaryString(byte value)
        {
            int bits = 8 * sizeof(byte);
            StringBuilder sb = new StringBuilder();
            for (int position = bits - 1; position >= 0; position--)
            {
                int b = GetBit(value, position);
                sb.Append(b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get the Bit at the specified position of the given int value.
        /// </summary>
        /// <param name="value">The integer value to retrieve the bit from.</param>
        /// <param name="position">The position, counting from left to right, of the bit to retrieve.</param>
        /// <returns>The integer value of the bit.</returns>
        public static int GetBit(int value, int position)
        {
            return (value >> position) & 1;
        }

        /// <summary>
        /// Get the Bit at the specified position of the given byte value.
        /// </summary>
        /// <param name="value">The byte value to retrieve the bit from.</param>
        /// <param name="position">The position, counting from left to right, of the bit to retrieve.</param>
        /// <returns>The integer value of the bit.</returns>
        public static int GetBit(byte value, int position)
        {
            return (value >> position) & 1;
        }


        /// <summary>
        /// Get the Bit at the specified position of the given char value.
        /// </summary>
        /// <param name="value">The char value to retrieve the bit from.</param>
        /// <param name="position">The position, counting from left to right, of the bit to retrieve.</param>
        /// <returns>The integer value of the bit.</returns>
        public static int GetBit(char value, int position)
        {
            return (value >> position) & 1;
        }


        /// <summary>
        /// Helper method to get an array of random integers.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        /// <param name="maxValue">The maximum value for the random values.</param>
        /// <returns>An array of random integers.</returns>
        public static int[] GetRandomArray(int size, int maxValue)
        {
            Random r = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = r.Next(maxValue);
            }
            return array;
        }

        /// <summary>
        /// Get a string that represents the contents of an array.
        /// </summary>
        /// <param name="values">The array of integers to iterate over.</param>
        /// <returns>A formatted string displaying the contents of the array.</returns>
        public static String ArrayContentsToString(int[] values)
        {
            if (values.Length <= 10)
            {
                return String.Format("{{ {0} }}", String.Join(", ", values));
            }
            else
            {
                return String.Format("{{\n    {0}\n}}", String.Join(",\n    ", values.Select(i => String.Format("{0,10}", i)).ToArray()));
            }
        }

        /// <summary>
        /// Get a string that represents the contents of an array.
        /// </summary>
        /// <param name="values">The array of strings to iterate over.</param>
        /// <returns>A formatted string displaying the contents of the array.</returns>
        public static String ArrayContentsToString(string[] values)
        {
            if (values.Length <= 10)
            {
                return String.Format("{{ {0} }}", String.Join(", ", values));
            }
            else
            {
                return String.Format("{{\n    {0}\n}}", String.Join(",\n    ", values.Select(s => String.Format("{0,10}", s)).ToArray()));
            }
        }


        public static String AddSpacing(String binary)
        {
            int zeroes = binary.Length / 4;
            //Console.Write("*** length: {0} zeroes: {1} *** ", binary.Length, zeroes);
            for (int i = zeroes; i >= 1; i--)
            {
                binary = binary.Insert(i * 4, " ");
            }
            return binary;
        }

        /// <summary>
        /// Example of how to access binary values of an integer.
        /// </summary>
        /// <param name="value"></param>
        public static void PrintIntegerAsBinary(int value)
        {
            for (int index = 31; index >= 0; index--)
            {
                //int b = ((value & 1 << index) >> index);
                int b = ((value >> index) & 1);
                Console.Write(b);
            }
            Console.WriteLine();
        }
    }
}
