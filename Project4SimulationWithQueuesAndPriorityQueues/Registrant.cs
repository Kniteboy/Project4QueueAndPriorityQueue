using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    public class Registrant
    {
        private static Random r = new Random();

        public int RegistrantNumber { get; set;}
        public TimeSpan Interval { get; set; }

        public TimeSpan ArrivalTime{get; set;}
        TimeSpan departureTime;
        public TimeSpan DepartureTime
        {
            get => departureTime;
            set
            {
                departureTime += Interval; 
            }
        }


        public Registrant()
        {
            ArrivalTime = new TimeSpan();
            Interval = new TimeSpan(0, (int)(1.5 + NegativeExponential(3)), 0);
            DepartureTime = new TimeSpan();
            RegistrantNumber = Poisson(1000);   //The convention center is planning for an expected 1000 people 
        }

        public Registrant(int ExpectedNumberOfPeople)
        {
            ArrivalTime = new TimeSpan(Registrant.r.Next(0, ConventionRegistration.EndTimeHour), Registrant.r.Next(0,  ConventionRegistration.EndTimeMinute), 0); //Generate random number between start and closing time of event
            Interval = new TimeSpan(0, (int)(1.5 + NegativeExponential(3)), 0); //Minimum time is one minute thirty seconds and the expected time is four minute and thirty seconds
            DepartureTime = new TimeSpan();
            RegistrantNumber = Poisson(ExpectedNumberOfPeople);   //Allows the registration convention center to change the expected number of people 
        }


        private static int Poisson(double ExpectedValue)
        {
            double dLimit = -ExpectedValue;
            double dSum = Math.Log(Registrant.r.NextDouble());

            int Count;
            for (Count = 0; dSum > dLimit; Count++)
                dSum += Math.Log(Registrant.r.NextDouble());
            return Count;

        }

        private static double  NegativeExponential(double ExpectedValue)
        {
            return -ExpectedValue * Math.Log(Registrant.r.NextDouble(), Math.E);
        }


    }
}