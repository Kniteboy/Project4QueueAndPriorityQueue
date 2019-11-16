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
            #region menu setup
            Menu menu = new Menu("Convention Registration");
            menu = menu + "Set the number of Registrants" + "Set the number of hours of operation" + "Set the number of windows" + "Set the expected checkout duration" + "Run the simulation" + "End the program";
            #endregion 

            Choices choice = new Choices();
            while(choice != Choices.QUIT)
            {
               
                switch (choice)
                {
                    case Choices.NUMBEROFREGISTRANTS:
                        //Set the expected value of registrants here
                        int numberOfRegistrants = 0;
                        while(numberOfRegistrants == 0)
                        {
                            Int32.TryParse(Console.ReadLine(), out numberOfRegistrants);
                            cr.ExpectedNumberOfRegistrants = numberOfRegistrants;
                        }
                        break;
                    case Choices.HOURSOFOPERATION:
                        //set the hours of operation here
                        int hoursOfOperation = 0;
                        while (hoursOfOperation == 0)
                        {
                            Int32.TryParse(Console.ReadLine(), out hoursOfOperation);
                            cr.HoursOfOperation = hoursOfOperation;
                        }
                        break;
                    case Choices.NUMBEROFWINDOWS:
                        //set the number of windows here
                        int numberOfWindows = 0;
                        while (numberOfWindows == 0)
                        {
                            Int32.TryParse(Console.ReadLine(), out numberOfWindows);
                            cr.NumberOfWindows = numberOfWindows;
                        }
                        break;
                    case Choices.CHECKOUTDURATION:
                        //set the expected value of the checkout duration here
                        double checkoutDuration = 0.0;
                        while (checkoutDuration == 0.0)
                        {
                            Double.TryParse(Console.ReadLine(), out checkoutDuration);
                            cr.CheckoutDuration = checkoutDuration;
                        }
                        break;
                    case Choices.RUNSIMULATION:
                        //run the simulation
                        cr.DoSimulation();
                        break;
                }
                
                choice = (Choices)menu.GetChoice();
            }//end while(choice != Choices.QUIT)


        }//end Main()
    }//end SimulationDriver
}//end name space Project4SimulationWithQueuesAndPriorityQueues
