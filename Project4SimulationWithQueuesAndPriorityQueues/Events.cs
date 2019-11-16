using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    enum EVENTTYPE {ARRIVAL, DEPARTURE };
    class Event : IComparable
    {
        public EVENTTYPE Type { get; set; }
        public DateTime Time { get; set; }
        public Registrant Registrant { get; set; }
        

        public Event()
        {
            Type = EVENTTYPE.ARRIVAL;
            Time = DateTime.Now;
            Registrant = null;
        }


        public Event(EVENTTYPE type, DateTime time, Registrant patron)
        {
            Type = type;
            Time = time;
            Registrant = patron;
        }

        public override string ToString()
        {
            string str = "";

            str += String.Format("Registrant {0}", Registrant.ToString().PadLeft(3));
            str += Type + "'s"
            str += String.Format(" at {0}", Time.ToShortTimeString().PadLeft(8));
        }


        public int CompareTo(object obj)
        {
            if (!(obj is Event))
                throw new ArgumentException("The argument is not an Event object");

            Event e = (Event)obj;
            return (e.Time.CompareTo(Time));
        }
    }
}