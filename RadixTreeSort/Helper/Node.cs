using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixTreeSort
{
    /// <summary>
    /// <para>
    /// A Node contains information needed for each slot in a Node[].
    /// </para>
    public class Node
    {
        /// <summary>
        /// Used to track the number of values stored through this Node.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// A reference to the next Node[] in the tree, will be null if not set.
        /// </summary>
        public Node[] Next { get; set; }

        /// <summary>
        /// Create a new Node with a Count = 0 and Next = null.
        /// </summary>
        public Node()
        {
            Count = 0;
            Next = null;
        }

        /// <summary>
        /// Get a new, initialized array of Nodes.
        /// </summary>
        /// <param name="size">The size of the array to get.</param>
        /// <returns>An array of Nodes.</returns>
        public static Node[] NewSet(int size)
        {
            Node[] nodes = new Node[size];
            for (int i = 0; i < size; i++)
            {
                nodes[i] = new Node();
            }
            return nodes;
        }

        /// <summary>
        /// Get a new, initialized array of Nodes.
        /// </summary>
        /// <param name="size">The size of the array to get.</param>
        /// <returns>An array of Nodes.</returns>
        public static Node[] NewSet()
        {
            Node[] nodes = new Node[2];
            nodes[0] = new Node();
            nodes[1] = new Node();
            return nodes;
        }
    }
}
