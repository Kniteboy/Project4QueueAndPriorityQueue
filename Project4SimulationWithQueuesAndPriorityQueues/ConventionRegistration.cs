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

        private PriorityQueue<Event> PQ;
        private List<Queue<Registrant>> regLines;
        private int actualNumberOfRegistrants;
        private DateTime openTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);
        double minimumCheckoutTime = 1.5;       //the minimum time a person can take to checkout
        int longestQueueLine = 0;
        int eventCount = 0;
        int arrivalCount = 0;
        int departureCount = 0;


        int indexCounter = 0;


        TimeSpan avgInterval = new TimeSpan(0, 0, 0);
        TimeSpan minInterval = new TimeSpan(0, 0, 0);
        TimeSpan maxInterval = new TimeSpan(0, 0, 0);
        
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
            PQ = new PriorityQueue<Event>();
            expectedNumberOfRegistrants = 1000;
            hoursOfOperation = 10;
            numberOfWindows = 9;
            regLines = new List<Queue<Registrant>>(numberOfWindows);
            checkoutDuration = 4.5;
            Console.Title = "Registration Simulation";

        } //end ConventionRegistration()

        /// <summary>
        /// Runs the Convention Registration simulation.
        /// </summary>
        public void RunSimulation()
        {
            minInterval = new TimeSpan(0, 0, 0);
            maxInterval = new TimeSpan(0, 0, 0);
            avgInterval = new TimeSpan(0, 0, 0);
            longestQueueLine = 0;
            eventCount = 0;
            arrivalCount = 0;
            departureCount = 0;

            string headerText = "";

            headerText += "\n\t\t\tRegistration Windows";
            headerText += "\n\t\t\t--------------------\n\n";

            for (int i = 0; i < numberOfWindows; i++)
            {
                headerText += $"\tW {i + 1}";
            } //end for (int i = 0; i < numberOfWindows; i++)

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(headerText);

            for (int i = 0; i < numberOfWindows; i++)
            {
                regLines.Add(new Queue<Registrant>());
            } //end for (int i = 0; i < numberOfWindows; i++)

            while (PQ.Count > 0 || !AllQueuesEmpty())
            {
                while (PQ.Count > 0 && PQ.Peek().Type == EVENTTYPE.ARRIVAL)
                {
                    int shortestLine = ShortestLine();
                    
                    if (regLines[shortestLine].Count == 0)
                    {
                        double tempInterval = (minimumCheckoutTime * 60) + NegativeExponential((checkoutDuration * 60) - (minimumCheckoutTime * 60));
                        PQ.Peek().Registrant.Interval = TimeSpan.FromSeconds(tempInterval);

                        PQ.Peek().Registrant.DepartureTime = PQ.Peek().Registrant.ArrivalTime + PQ.Peek().Registrant.Interval;

                        if (PQ.Peek().Registrant.Interval < minInterval || minInterval.Equals(new TimeSpan(0, 0, 0)))
                        {
                            minInterval = PQ.Peek().Registrant.Interval;
                        } //end if (PQ.Peek().Registrant.Interval < minInterval || minInterval.Equals(new TimeSpan(0, 0, 0)))

                        if (PQ.Peek().Registrant.Interval > maxInterval)
                        {
                            maxInterval = PQ.Peek().Registrant.Interval;
                        } //end if (PQ.Peek().Registrant.Interval > maxInterval)

                        avgInterval = avgInterval.Add(PQ.Peek().Registrant.Interval);

                        PQ.Enqueue(new Event(EVENTTYPE.DEPARTURE, openTime.Add(PQ.Peek().Registrant.DepartureTime), PQ.Peek().Registrant));    
                    }//end if(regLines[shortestLine].Count == 0)
                 
                    regLines[shortestLine].Enqueue(PQ.Peek().Registrant);
                    eventCount++;
                    arrivalCount++;

                    Console.SetCursorPosition(0, 0);

                    //DrawLines();
                 
                    PQ.Dequeue();

                } //end while (PQ.Count > 0 && PQ.Peek().Type == EVENTTYPE.ARRIVAL)
                if (PQ.Count > 0 && PQ.Peek().Type == EVENTTYPE.DEPARTURE)
                {
                  
                    for (int i = 0; i < numberOfWindows; i++)
                    {
                        
                        if (regLines[i].Count > 0 && PQ.Peek().Registrant.RegistrantNumber == regLines[i].Peek().RegistrantNumber)
                        {
                            TimeSpan previousPerson = regLines[i].Peek().DepartureTime;
                            regLines[i].Dequeue();
                            eventCount++;
                            departureCount++;
                            //DrawLines();
                            if (regLines[i].Count > 0)
                            {
                               

                                double tempInterval = (minimumCheckoutTime * 60) + NegativeExponential((checkoutDuration * 60) - (minimumCheckoutTime * 60));
                                regLines[i].Peek().Interval = TimeSpan.FromSeconds(tempInterval);

                                if (regLines[i].Peek().Interval < minInterval || minInterval.Equals(new TimeSpan(0, 0, 0)))
                                {
                                    minInterval = regLines[i].Peek().Interval;
                                } //end if (regLines[i].Peek().Interval < minInterval || minInterval.Equals(new TimeSpan(0, 0, 0)))

                                if (regLines[i].Peek().Interval > maxInterval)
                                {
                                    maxInterval = regLines[i].Peek().Interval;
                                } //end if (regLines[i].Peek().Interval > maxInterval)

                                avgInterval = avgInterval.Add(regLines[i].Peek().Interval);

                                regLines[i].Peek().DepartureTime = regLines[i].Peek().Interval + previousPerson;//possible solution?
                                PQ.Enqueue(new Event(EVENTTYPE.DEPARTURE, openTime.Add(regLines[i].Peek().DepartureTime), regLines[i].Peek()));
                            }//end if(regLines[i].Count > 0)

                        } //end if (regLines[i].Count > 0 && PQ.Peek().Registrant.RegistrantNumber == regLines[i].Peek().RegistrantNumber)

                    } //end for (int i = 0; i < numberOfWindows; i++)
                    
                    PQ.Dequeue();
                    //DrawLines();
                } //end if (PQ.Count > 0 && PQ.Peek().Type == EVENTTYPE.DEPARTURE)
                DrawLines();
            }
            avgInterval = TimeSpan.FromSeconds(avgInterval.TotalSeconds / actualNumberOfRegistrants);

            Console.WriteLine($"\nAverage Service Time: {avgInterval.ToString("g")}");

            Console.WriteLine($"Minimum Service Time: {minInterval.ToString("g")}");
            Console.WriteLine($"Maximum Service Time: {maxInterval.ToString("g")}");
        } //end RunSimulation()

        #region Utility Methods
        /// <summary>
        /// Finds the shortest registration line.
        /// </summary>
        /// <returns>Position of the shortest registration line</returns>
        public int ShortestLine()
        {
            int shortestLine = 0;   //integer representing the queue 
            for(int i = 0; i < numberOfWindows; i++)
            {
                if (regLines[i].Count < regLines[shortestLine].Count)
                {
                    shortestLine = i;
                } //end if (regLines[i].Count < regLines[shortestLine].Count)
            } //end for(int i = 0; i < numberOfWindows; i++)
            return shortestLine;
        }//end ShortestLine()

        /// <summary>
        /// Finds the length of the longest registration line.
        /// </summary>
        /// <returns>Length of the longest registration line</returns>
        public int LongestLine()
        {
            int longestLine = 0;
            for (int i = 0; i < numberOfWindows; i++)
            {
                if (regLines[i].Count > longestLine)
                {
                    longestLine = regLines[i].Count;
                    if(longestQueueLine < longestLine)
                    {
                        longestQueueLine = longestLine;
                    } //end if(longestQueueLine < longestLine)
                } //end if (regLines[i].Count > longestLine)
            }
            return longestLine;
        }//end LongestLine()

        /// <summary>
        /// Draws the registration lines in the console.
        /// </summary>
        public void DrawLines()
        {
            List<List<Registrant>> queues = new List<List<Registrant>>();


            ClearConsoleLines(6, LongestLine() + indexCounter + 6);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(0,6);


            for (int i = 0; i < numberOfWindows; i++)
            {
                queues.Add(new List<Registrant>(regLines[i].ToArray()));
            } //end for (int i = 0; i < numberOfWindows; i++)

            for (int i = 0; i < queues.Count; i++)
            {
                while (queues[i].Count < LongestLine())
                {
                    queues[i].Add(new Registrant());
                }//end while(queues[i].Count < LongestLine())
            } //end for (int i = 0; i < queues.Count; i++)

          
            for (int i = 0; i < LongestLine(); i++)
            {
                for (int j = 0; j < numberOfWindows; j++)
                {
                    if (!(queues[j][i].RegistrantNumber == 0))
                    {
                        Console.Write($"\t{queues[j][i].RegistrantNumber:0000}");
                    } //end if (!(queues[j][i].RegistrantNumber == 0))
                    else
                    {
                        Console.Write($"\t    ");
                    } //end else
                } //end for (int j = 0; j < numberOfWindows; j++)




                Console.Write("\n");
            } //end for (int i = 0; i < LongestLine(); i++)

            
            while (LongestLine() + indexCounter < longestQueueLine)
            {
                Console.WriteLine();
                indexCounter++;
            }// end while (LongestLine() + indexCounter < longestQueueLine)

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("So Far: -------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Longest Queue So Far: {longestQueueLine}");
            Console.WriteLine();
            Console.WriteLine($"Events Processed So Far: {eventCount}".PadRight(32) + $"Arrivals: {arrivalCount}".PadRight(16) + $"Departures: {departureCount}".PadRight(15));
            Console.WriteLine($"Number of Registrants: {actualNumberOfRegistrants}".PadRight(40) + $"Checkout Duration: {checkoutDuration}".PadRight(25));
            Console.WriteLine($"Hours of operation: {hoursOfOperation}");

          //  Thread.Sleep(100);
        } //end DrawLines()

        /// <summary>
        /// Determines if all the queues in the program are empty
        /// </summary>
        /// <returns>true if all queues are empty and false otherwise</returns>
        private bool AllQueuesEmpty()
        {
            bool allQueuesEmpty = true;
            for (int i = 0; i < regLines.Count; i++)
            {
                if(regLines[i].Count > 0)
                {
                    allQueuesEmpty = false;
                } //end if(regLines[i].Count > 0)

            } //end for (int i = 0; i < regLines.Count; i++)
            return allQueuesEmpty;
        } //end AllQueuesEmpty()

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

                Registrant temp = new Registrant(i, start);

                PQ.Enqueue(new Event(EVENTTYPE.ARRIVAL, openTime.Add(temp.ArrivalTime), temp));
            } //end for (int i = 0; i < actualNumberOfRegistrants; i++)
        } //end GenerateEvents()
        #endregion

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
        /// A method to clear specific console lines
        /// </summary>
        /// <param name="start">The starting row to clear</param>
        /// <param name="end">The ending row to clear</param>
        private static void ClearConsoleLines(int start, int end)
        {
            Console.SetCursorPosition(start, 0);
            for (int i = 0; i < end; i++)
            {
                Console.SetCursorPosition(0, start + i);
                Console.Write(new string(' ', Console.WindowWidth));
                
            }//end for(int i = 0; i < end; i++)

        }//end ClearConsoleLines(int start, int end)

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
