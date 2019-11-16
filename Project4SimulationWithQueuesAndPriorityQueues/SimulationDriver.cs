///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Project4SimulationWithQueuesAndPriorityQueues
//	File Name:         SimulationDriver.cs
//	Description:       Manages the menu of the program and selecting different choices
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Seth Norton, nortonsp@etsu.edu
//	Created:           Saturday, November 16, 2019
//	Copyright:         Seth Norton, 2019
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using MenuClassDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityNamespace;

namespace Project4SimulationWithQueuesAndPriorityQueues
{
    /// <summary>
    /// The class which manages the menu of the program and passes the input to the convention registration class to be handled
    /// </summary>
    class SimulationDriver
    {
        /// <summary>
        /// The main entry point of the application which holds options for the user to edit and allow the user to run the simulation
        /// </summary>
        static void Main()
        {
            ConventionRegistration cr = new ConventionRegistration();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
            Menu menu = new Menu();
            menu = menu + "Set the number of Registrants" + "Set the number of hours of operation" + "Set the number of windows" + "Set the expected checkout duration" + "Run the simulation" + "End the program";

            Choices choice = new Choices();
            while(choice != Choices.QUIT)
            {
                switch (choice)
                {
                    case Choices.NUMBEROFREGISTRANTS:
                        //Set the expected value of registrants here
                        break;
                    case Choices.HOURSOFOPERATION:
                        //set the hours of operation here
                        break;
                    case Choices.NUMBEROFWINDOWS:
                        //set the number of windows here
                        break;
                    case Choices.CHECKOUTDURATION:
                        //set the expected value of the checkout duration here
                        break;
                    case Choices.QUIT:
                        //quit the application
                        Environment.Exit(1);
                        break;
                }
            }
            

        }
    }
}
