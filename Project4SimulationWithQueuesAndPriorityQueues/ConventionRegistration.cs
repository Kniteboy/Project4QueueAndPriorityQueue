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
using System.Threading;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    /// <summary>
    /// Class used to manage the simulation of registration windows in a convention
    /// </summary>
    class ConventionRegistration
    {
        private Random r;
        private int maxLineCount;
        private PriorityQueue<Event> PQ;
        private List<Queue<Registrant>> regLines;
        private int actualNumberOfRegistrants;
        private DateTime openTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
        private DateTime closingTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 18, 0, 0);

        #region Properties
        private int expectedNumberOfRegistrants;

        /// <summary>
        /// Property that gets or sets the expected number of registrants.
        /// </summary>
        /// <value>
        /// The expected number of registrants at the convention.
        /// </value>
        public int ExpectedNumberOfRegistrants
        {
            get { return expectedNumberOfRegistrants; }
            set { expectedNumberOfRegistrants = value; }
        } //end ExpectedNumberOfRegistrants

        private int hoursOfOperation;

        /// <summary>
        /// Property that gets or sets the hours of operation.
        /// </summary>
        /// <value>
        /// The convention's hours of operation.
        /// </value>
        public int HoursOfOperation
        {
            get { return hoursOfOperation; }
            set { hoursOfOperation = value; }
        } //end HoursOfOperation

        private int numberOfWindows;

        /// <summary>
        /// Property that gets or sets the number of registration windows.
        /// </summary>
        /// <value>
        /// The number of registration windows at the convention.
        /// </value>
        public int NumberOfWindows
        {
            get { return numberOfWindows; }
            set { numberOfWindows = value; }
        } //end NumberOfWindows

        private double checkoutDuration;

        /// <summary>
        /// Property that gets or sets the expected checkout duration.
        /// </summary>
        /// <value>
        /// The expected checkout duration per customer.
        /// </value>
        public double CheckoutDuration
        {
            get { return checkoutDuration; }
            set { checkoutDuration = value; }
        } //end CheckoutDuration
        #endregion
        
        /// <summary>
        /// Default constructor that initializes a new instance of the <see cref="ConventionRegistration"/> class.
        /// </summary>
        public ConventionRegistration()
        {
            r = new Random();
            maxLineCount = 1;
            PQ = new PriorityQueue<Event>();
            expectedNumberOfRegistrants = 1000;
            hoursOfOperation = 10;
            numberOfWindows = 1;
            regLines = new List<Queue<Registrant>>(numberOfWindows);
            checkoutDuration = 4.5;

        } //end ConventionRegistration()

        /// <summary>
        /// Runs the Convention Registration simulation.
        /// </summary>
        public void RunSimulation()
        {
            for (int i = 0; i < numberOfWindows; i++)
            {
                regLines.Add(new Queue<Registrant>());
            } //end for (int i = 0; i < numberOfWindows; i++)

            while (PQ.Count > 0)
            {
                while (!(PQ.Peek().Type == EVENTTYPE.DEPARTURE))
                {
                    int shortestLines = LongestLine();
                    regLines[shortestLine].Enqueue(PQ.Peek().Registrant);
                    PQ.Dequeue();

                    DrawLines();
                } //end while (!(PQ.Peek().Type == EVENTTYPE.DEPARTURE))
                if (PQ.Peek().Type == EVENTTYPE.DEPARTURE)
                {
                    for (int i = 0; i < numberOfWindows; i++)
                    {
                        if (PQ.Peek().Registrant.RegistrantNumber == regLines[i].Peek().RegistrantNumber)
                        {
                            regLines[i].Dequeue();
                        } //end if (PQ.Peek().Registrant.RegistrantNumber == regLines[0].Peek().RegistrantNumber)
                    } //end for (int i = 0; i < numberOfWindows; i++)

                    PQ.Dequeue();
                    DrawLines();
                } //end if (PQ.Peek().Type == EVENTTYPE.DEPARTURE)

                Thread.Sleep(1000);
            }
        } //end RunSimulation()

        #region Utility Methods
        ///</summary>
        /// Finds the shortest registration line.
        /// </summary>
        /// <returns>Position of the shortest registration line</returns>
        public int ShortestLine()
        {
            int shortestLine = 0;
            for(int i = 0; i < numberOfWindows; i++)
            {
                if (regLines[i].Count < regLines[shortestLine].Count)
                {
                    shortestLine = i;
                }
            }
            return shortestLine;
        }//end ShortestLine()

        /// <summary>
        /// Finds the length of the longest registration line.
        /// </summary>
        /// <returns>Length of the longest registration line</returns>
        public int LongestLine()
        {
            int longestLine = 0;
            for(int i = 0; i < numberOfWindows; i++)
            {
                if (regLines[i].Count > longestLine)
                {
                    longestLine = regLines.Count;
                }
            }
            return longestLine;
        }//end longestLine()

        /// <summary>
        /// Draws the registration lines in the console.
        /// </summary>
        public void DrawLines()
        {
            List<List<Registrant>> queues = new List<List<Registrant>>();

            string headerText = "";

            headerText += "\n\t\t\tRegistration Windows";
            headerText += "\n\t\t\t--------------------\n\n";

            for (int i = 0; i < numberOfWindows; i++)
            {
                headerText += $"\tW {i + 1}";
            } //end for (int i = 0; i < numberOfWindows; i++)

            Console.Clear();
            Console.WriteLine(headerText);

            for (int i = 0; i < numberOfWindows; i++)
            {
                queues.Add(new List<Registrant>(regLines[i].ToArray()));
            } //end for (int i = 0; i < numberOfWindows; i++)

            for (int i = 0; i < LongestLine(); i++)
            {
                for (int j = 0; j < numberOfWindows; j++)
                {
                    if (!(queues[i][j].RegistrantNumber == 0))
                    {
                        Console.Write($"\t{queues[i][j].RegistrantNumber}");
                    } //end 
                    else
                    {
                        Console.Write($"\t    ");
                    } //end else
                } //end 
                Console.Write("\n");
            } //end 

        } //end DrawLines()
        #endregion

        /// <summary>
        /// Generates the events for registrant arrival and departure times.
        /// </summary>
        public void GenerateEvents()
        {
            TimeSpan start;
            actualNumberOfRegistrants = Poisson(expectedNumberOfRegistrants);

            for (int i = 1; i <= actualNumberOfRegistrants; i++)
            {
                start = new TimeSpan(0, r.Next(10 * 60), 0);

                Registrant temp = new Registrant(i, start, checkoutDuration);

                PQ.Enqueue(new Event(EVENTTYPE.ARRIVAL, openTime.Add(temp.ArrivalTime), temp));
                PQ.Enqueue(new Event(EVENTTYPE.DEPARTURE, openTime.Add(temp.DepartureTime), temp));
            } //end for (int i = 0; i < actualNumberOfRegistrants; i++)
        } //end GenerateEvents()

        #region Random Distributions
        /// <summary>
        /// The method generating numbers from a Poisson distribution for the registrant's display number
        /// </summary>
        /// <param name="ExpectedValue">The expected number of registrants</param>
        /// <returns>a number from the Poisson distribution</returns>
        private int Poisson(double ExpectedValue)
        {
            double dLimit = -ExpectedValue;
            double dSum = Math.Log(r.NextDouble());

            int Count;
            for (Count = 0; dSum > dLimit; Count++)
                dSum += Math.Log(r.NextDouble());
            return Count;
        } //end Poisson(double ExpectedValue)

        /// <summary>
        /// The negative exponential function determines how long an individual stays at the registration booth (after getting out of the line)
        /// </summary>
        /// <param name="ExpectedValue">The expected time taken by each registrant</param>
        /// <returns>The time taken at the registration booth</returns>
        private double NegativeExponential(double ExpectedValue)
        {
            return -ExpectedValue * Math.Log(r.NextDouble(), Math.E);
        } //end NegativeExponential(double ExpectedValue)
        #endregion distribution code

    } //end ConventionRegistration
}