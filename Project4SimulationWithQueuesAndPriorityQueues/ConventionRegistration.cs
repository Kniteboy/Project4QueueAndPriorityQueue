//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:			Project 4 - Simulating Convention Registration
//	File Name:		ConventionRegistration.cs
//	Description:		Class used to manage the simulation of registration windows in a convention
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Edmund Yong, yongez@etsu.edu, Department of Computing, East Tennessee State University
//	Created:			Friday, November 15, 2019
//	Copyright:		Edmund Yong, 2019
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    /// <summary>
    /// Class used to manage the simulation of registration windows in a convention
    /// </summary>
    class ConventionRegistration
    {
        private int maxLineCount;
        private PriorityQueue<Event> PQ;
        private List<Queue<Registrant>> regLines;

        private DateTime startTime;

        /// <summary>
        /// Property that gets or sets the start time.
        /// </summary>
        /// <value>
        /// The convention's start time.
        /// </value>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        } //end StartTime

        private DateTime closingTime;

        /// <summary>
        /// Property that gets or sets the closing time.
        /// </summary>
        /// <value>
        /// The convention's closing time.
        /// </value>
        public DateTime ClosingTime
        {
            get { return closingTime; }
            set { closingTime = value; }
        } //end ClosingTime

        /// <summary>
        /// Default constructor that initializes a new instance of the <see cref="ConventionRegistration"/> class.
        /// </summary>
        public ConventionRegistration()
        {
            maxLineCount = 0;
            PQ = new PriorityQueue<Event>();
            regLines = new List<Queue<Registrant>>();
            startTime = new DateTime();
            closingTime = new DateTime();

        } //end ConventionRegistration()

        public ConventionRegistration()

    } //end ConventionRegistration
}