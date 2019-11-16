///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project4SimulationWithQueuesAndPriorityQueues/Project4SimulationWithQueuesAndPriorityQueues
//	File Name:         PriorityQueue.cs
//	Description:       Hold and manage a queue where items are sorted first by priority, then by order of entry
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
    /// <summary>Hold and manage a queue sorted first by priority, then by order of entry</summary>
    /// <typeparam name="T">Type of the item to queue.</typeparam>
    /// <seealso cref="Project4SimulationWithQueuesAndPriorityQueues.IPriorityQueue{T}" />
    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        #region Fields and Properties
        private Node<T> top; //reference to top node in priority queue
        public int Count { get; private set; }
        #endregion

        #region PriorityQueue Methods
        /// <summary>Enqueues the item in the correct place based on its priority.</summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item)
        {
            Node<T> newNode = new Node<T>(item, null);

            if (Count == 0)
            {
                top = newNode; //add to top if PQ is empty
            }
            else
            {
                Node<T> previous = null;
                Node<T> current = top;

                // Search for first node in queue with lower priority than new node
                while (current != null && current.CompareTo(newNode) >= 0)
                {
                    previous = current;
                    current = current.Next;
                }

                // Set link of new node to correct next node
                newNode.Next = current;

                if (previous == null)
                {
                    top = newNode; //new node has highest priority
                }
                else
                {
                    previous.Next = newNode; //update link of node just above new node
                }
            }

            Count++;
        }

        /// <summary>  Returns the top node and removes it from the queue.</summary>
        /// <returns>The top node.</returns>
        /// <exception cref="InvalidOperationException">Cannot remove from empty queue.</exception>
        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Cannot remove from empty queue.");
            }

            Node<T> oldNode = top;
            top = top.Next;
            Count--;
            return oldNode.Item;
        }

        /// <summary>Returns the top item in the priority queue.</summary>
        /// <returns>The top item.</returns>
        /// <exception cref="InvalidOperationException">Cannot obtain top of empty priority queue.</exception>
        public T Peek()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Cannot obtain top of empty priority queue.");
            }

            return top.Item;
        }
        #endregion

        #region Container Methods
        /// <summary>Clears the priority queue.</summary>
        public void Clear()
        {
            top = null; //nodes are garbage collected
            Count = 0;
        }

        /// <summary>Determines whether the priority queue is empty.</summary>
        /// <returns>
        ///   <c>true</c> if priority queue is empty; otherwise, <c>false</c>.</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }
        #endregion
    }
}
