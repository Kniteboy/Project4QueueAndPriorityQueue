///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project4SimulationWithQueuesAndPriorityQueues
//	File Name:         Event.cs
//	Description:       Defines the type of events that will be associated with a registrant
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
    /// The three different event types for the registration event
    /// </summary>
    enum EVENTTYPE {ARRIVAL, DEPARTURE, LINESELECTION };
    /// <summary>
    /// The class associated with associating an event with a registrant
    /// </summary>
    /// <seealso cref="System.IComparable" />
    class Event : IComparable<Event>
    {
        /// <summary>
        /// Gets or sets the type of event (either an arrival, departure or a line selection)
        /// </summary>
        /// <value>
        /// The type of event (either an arrival, departure or a line selection)
        /// </value>
        public EVENTTYPE Type { get; set; }
        /// <summary>
        /// Gets or sets the time the event happened
        /// </summary>
        /// <value>
        /// The time the event happened
        /// </value>
        public DateTime Time { get; set; }
        /// <summary>
        /// Gets or sets the registrant which will be associated with a specific event ant time
        /// </summary>
        /// <value>
        /// The registrant which is associated with the event
        /// </value>
        public Registrant Registrant { get; set; }



        /// <summary>
        /// The default constructor for the event type setting the event time to arrival, Time to now and the registrant to null
        /// </summary>
        public Event()
        {
            Type = EVENTTYPE.ARRIVAL;
            Time = DateTime.Now;
            Registrant = null;
        }


        /// <summary>
        /// Associates an event with a specific event type, specific time the event happened and a specific registrant
        /// </summary>
        /// <param name="type">The type of event (arrival or departure)</param>
        /// <param name="time">The time the event occurred</param>
        /// <param name="patron">The patron/registrant associated with the event</param>
        public Event(EVENTTYPE type, DateTime time, Registrant patron)
        {
            Type = type;
            Time = time;
            Registrant = patron;
        }

        /// <summary>
        /// Formats a string for use when displaying event information
        /// </summary>
        /// <returns>
        /// A string representing the registrant, type of event and the time of the event occurring
        /// </returns>
        public override string ToString()
        {
            string str = "";

            str += String.Format("Registrant {0}", Registrant.ToString().PadLeft(3));
            str += Type + "'s";
            str += String.Format(" at {0}", Time.ToShortTimeString().PadLeft(8));
            return str;
        }



        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance (Looking for an Event object to compare with another event)</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="obj" /> in the sort order. Zero This instance occurs in the same position in the sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref name="obj" /> in the sort order.
        /// </returns>
        /// <exception cref="ArgumentException">The argument is not an Event object</exception>
        public int CompareTo(Event obj)
        {
            if (!(obj is Event))
                throw new ArgumentException("The argument is not an Event object");

            Event e = (Event)obj;
            return (e.Time.CompareTo(Time));    //uses DateTime.CompareTo
        }
    }
}