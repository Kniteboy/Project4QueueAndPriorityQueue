///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project4SimulationWithQueuesAndPriorityQueues/Project4SimulationWithQueuesAndPriorityQueues
//	File Name:         Node.cs
//	Description:       Node<T> class used to form a linked list for use in PriorityQueue<T>
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Drew Whitinger, whitingera@etsu.edu
//	Created:           Friday, November 15, 2019
//	Copyright:         Drew Whitinger, 2019
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    /// <summary>Node class to form a linked list for use in PriorityQueue</summary>
    /// <typeparam name="T">The type of the item to be stored.</typeparam>
    public class Node<T> : IComparable<Node<T>> where T : IComparable<T>
    {
        #region Properties
        public T Item { get; set; }
        public Node<T> Next { get; set; }
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="Node{T}"/> class.</summary>
        public Node()
        {
            this.Item = default(T);
            this.Next = null;
        }

        /// <summary>Initializes a new instance of the <see cref="Node{T}"/> class.</summary>
        /// <param name="item">The item stored in the node.</param>
        /// <param name="next">The link to the next node.</param>
        public Node(T item, Node<T> next)
        {
            this.Item = item;
            this.Next = next;
        }
        #endregion

        #region Methods
        /// <summary>Returns result of comparing the Items of two Nodes.</summary>
        /// <param name="node">The node to compare to.</param>
        /// <returns>Result of comparing the Items of two Nodes.</returns>
        public int CompareTo(Node<T> node)
        {
            return Item.CompareTo(node.Item);
        }
        #endregion
    }
}
