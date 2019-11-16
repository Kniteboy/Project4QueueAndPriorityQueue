///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project4SimulationWithQueuesAndPriorityQueues
//	File Name:         Registrant.cs
//	Description:       Handles the time events of the registrant and also the registrant number
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Seth Norton, nortonsp@etsu.edu
//	Created:           Saturday, November 16, 2019
//	Copyright:         Seth Norton, 2019
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    /// <summary>
    /// The class handling the times of arrival, departure and time taken by each individual
    /// </summary>
    public class Registrant
    {
        /// <summary>
        /// A Random object to handle all the random events in the registrant class
        /// </summary>
        private static Random r = new Random();
        
        /// <summary>
        /// Used to give each registrant a number (generated from a Poisson distribution)
        /// </summary>

        public int RegistrantNumber { get; set;}
        /// <summary>
        /// The interval/duration each registrant takes at the registration table
        /// </summary>
        public TimeSpan Interval { get; set; }
        
        /// <summary>
        /// The arrival time of each registrant to the registration line
        /// </summary>

        public TimeSpan ArrivalTime{get; set;}


        /// <summary>
        /// The departure time of the registrant (backing store)
        /// </summary>
        TimeSpan departureTime;
        /// <summary>
        /// The property which manages the departure time of the registrant
        /// </summary>
        public TimeSpan DepartureTime
        {
            get => departureTime;
            set
            {
                departureTime = ArrivalTime + Interval;  //add the interval to the arrival time
            }
        }

        /// <summary>
        /// The default constructor which states that the expected number of registrants is 1000 people
        /// </summary>
        public Registrant()
        {
            ArrivalTime = new TimeSpan(Registrant.r.Next(0, ConventionRegistration.EndTimeHour), Registrant.r.Next(0, ConventionRegistration.EndTimeMinute), 0);
            Interval = new TimeSpan(0, (int)(1.5 + NegativeExponential(3)), 0);
            DepartureTime = new TimeSpan();
            RegistrantNumber = Poisson(1000);   //The convention center is planning for an expected 1000 people 
        }

        /// <summary>
        /// Allows the user to change the expected number of people at the registration event
        /// </summary>
        /// <param name="ExpectedNumberOfPeople">The expected number of people at the registration event</param>
        public Registrant(int ExpectedNumberOfPeople)
        {
            ArrivalTime = new TimeSpan(Registrant.r.Next(0, ConventionRegistration.EndTimeHour), Registrant.r.Next(0,  ConventionRegistration.EndTimeMinute), 0); //Generate random number between start and closing time of event
            Interval = new TimeSpan(0, (int)(1.5 + NegativeExponential(3)), 0); //Minimum time is one minute thirty seconds and the expected time is four minute and thirty seconds
            DepartureTime = new TimeSpan();
            RegistrantNumber = Poisson(ExpectedNumberOfPeople);   //Allows the registration convention center to change the expected number of people 
        }



        /// <summary>
        /// The method generating numbers from a Poisson distribution for the registrant's display number
        /// </summary>
        /// <param name="ExpectedValue">The expected number of registrants</param>
        /// <returns>a number from the Poisson distribution</returns>
        private static int Poisson(double ExpectedValue)
        {
            double dLimit = -ExpectedValue;
            double dSum = Math.Log(Registrant.r.NextDouble());

            int Count;
            for (Count = 0; dSum > dLimit; Count++)
                dSum += Math.Log(Registrant.r.NextDouble());
            return Count;

        }

        /// <summary>
        /// The negative exponential function determines how long an individual stays at the registration booth (after getting out of the line)
        /// </summary>
        /// <param name="ExpectedValue">The expected time taken by each registrant</param>
        /// <returns>The time taken at the registration booth</returns>
        private static double  NegativeExponential(double ExpectedValue)
        {
            return -ExpectedValue * Math.Log(Registrant.r.NextDouble(), Math.E);
        }


    }
}