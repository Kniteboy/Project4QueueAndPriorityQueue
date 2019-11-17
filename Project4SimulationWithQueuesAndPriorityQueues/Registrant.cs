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

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    /// <summary>
    /// The class handling the times of arrival, departure and time taken by each individual
    /// </summary>
    public class Registrant
    {
        /// <summary>A Random object to handle all the random events in the registrant class</summary>
        private static Random r = new Random();

        #region properties

        /// <summary>
        /// Used to give each registrant a number (generated from a Poisson distribution)
        /// </summary>

        public int RegistrantNumber { get; set; }

        /// <summary>
        /// The interval/duration backing store each registrant takes at the registration table
        /// </summary>
        private TimeSpan interval;
        /// <summary>
        /// The interval/duration each registrant takes at the registration table
        /// </summary>
        public TimeSpan Interval
        {
            get => interval;
            set
            {
                interval = value;
            }
        }

        /// <summary>
        /// The arrival time of each registrant to the registration line
        /// </summary>

        public TimeSpan ArrivalTime { get; set; }


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
                departureTime = value; //add the interval to the arrival time
            }
        }//end TimeSpan DepartureTime

        #endregion properties

        #region constructor
        /// <summary>
        /// The default constructor which states that the expected number of registrants is 1000 people
        /// </summary>
        public Registrant()
        {
            ArrivalTime = new TimeSpan(0, 0, 0);
            //Interval = new TimeSpan(0, (int)(1.5 + NegativeExponential(3)), 0);
            //DepartureTime = new TimeSpan();
            RegistrantNumber = 0;   //The default registration number will be zero
        }//end Registrant()


        /// <summary>
        /// Constructor to take all the registrant information (number, arrival time, departure time and the expected registration time)
        /// </summary>
        /// <param name="registrantNumber">the registrant's number</param>
        /// <param name="arrivalTime">the arrival time of the registrant</param>
        /// <param name="departtureTime">the departure time of the registrant</param>
        /// <param name="expectedRegistrationTime">the expected registration time of the registrant</param>
        public Registrant(int registrantNumber, TimeSpan arrivalTime, double expectedRegistrationTime)
        {
            ArrivalTime = arrivalTime;
            //Interval = new TimeSpan(0, (int) (expectedRegistrationTime + NegativeExponential(3)), 0); //Minimum time is one minute thirty seconds and the expected time is four minute and thirty seconds
            //DepartureTime = ArrivalTime + Interval;
            RegistrantNumber = registrantNumber;   //Sets the specified registrant number
        }//end Registrant(int registrantNumber, TimeSpan arrivalTime, int expectedRegistrationTime)

        #endregion constructor



        #region distribution code

        /// <summary>
        /// The negative exponential function determines how long an individual stays at the registration booth (after getting out of the line)
        /// </summary>
        /// <param name="ExpectedValue">The expected time taken by each registrant</param>
        /// <returns>The time taken at the registration booth</returns>
        private static double NegativeExponential(double ExpectedValue)
        {
            return -ExpectedValue * Math.Log(Registrant.r.NextDouble(), Math.E);
        }//end NegativeExponential(double ExpectedValue)
        #endregion distribution code

    }
}