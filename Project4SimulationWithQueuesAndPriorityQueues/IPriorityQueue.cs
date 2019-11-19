///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project4SimulationWithQueuesAndPriorityQueues/Project4SimulationWithQueuesAndPriorityQueues
//	File Name:         IPriorityQueue.cs
//	Description:       Specify methods and properties for a priority queue
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
    /// <summary>Specify methods and properties for a priority queue</summary>
    /// <typeparam name="T">Type of item in priority queue</typeparam>
    /// <seealso cref="Project4SimulationWithQueuesAndPriorityQueues.IContainer{T}" />
    public interface IPriorityQueue<T> : IContainer<T> where T : IComparable<T>
    {
        /// <summary>Enqueues the item in the correct place based on its priority.</summary>
        /// <param name="item">The item to enqueue.</param>
        void Enqueue(T item);

        /// <summary>Dequeues and returns the top item in the priority queue.</summary>
        /// <returns>The top item.</returns>
        T Dequeue();

        /// <summary>Returns the top item in the priority queue.</summary>
        /// <returns>The top item.</returns>
        T Peek();
    }
}
