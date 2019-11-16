///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project4SimulationWithQueuesAndPriorityQueues/Project4SimulationWithQueuesAndPriorityQueues
//	File Name:         IContainer.cs
//	Description:       Specifiy methods and properties for containers
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
    /// <summary>Container that holds items.</summary>
    /// <typeparam name="T">Type of item to hold.</typeparam>
    public interface IContainer<T>
    {
        /// <summary>Removes all objects from the container.</summary>
        void Clear();

        /// <summary>Determines whether this instance is empty.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
        bool IsEmpty();

        /// <summary>Number of items in the container.</summary>
        /// <value>The count.</value>
        int Count { get; }
    }
}
