using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Configuration;


namespace Coding_Tracker_1
{
    public class UserInput
    {
        static string connectionString = ConfigurationManager.AppSettings["k1"];
        public static void GetInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("MAIN MENU\n");
                Console.WriteLine("Please select an option");
                Console.WriteLine("\n----------------------");
                Console.WriteLine("\nType 0 to Close Application");
                Console.WriteLine("Type 1 to Insert an Entry");
                Console.WriteLine("Type 2 to Update an Entry");
                Console.WriteLine("Type 3 to View all Entries");
                Console.WriteLine("Type 4 to Delete an Entry");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Console.WriteLine("Goodbye!");
                        closeApp = true;
                        Environment.Exit(0);
                        break;

                    case "1":
                        //Insert();
                        break;

                    case "2":
                        //Update();
                        break;

                    case "3":
                        //GetAllRecords();
                        break;

                    case "4":
                        //Delete();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number 0 - 4");
                        break;


                }
            }
        }
    }
}
