using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    enum EVENTTYPE {ENTER, LEAVE };
    class Event : IComparable
    {
        public EVENTTYPE Type { get; set; }
        public DateTime Time { get; set; }
        //public int Patron { get; set }
        //make this into a registrant

        public Event()
        {
            Type = EVENTTYPE.ENTER;
            Time = DateTime.Now;
            Patron = -1;
        }


        public Event(EVENTTYPE type, DateTime time, int patron)
        {
            Type = type;
            Time = time;
            Patron = patron;
        }

        public override string ToString()
        {
           
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