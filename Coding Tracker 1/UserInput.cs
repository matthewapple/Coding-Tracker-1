using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Coding_Tracker_1
{
    public class UserInput
    {
        public static void GetSelection()
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
                        CodingController.Insert();
                        break;

                    case "2":
                        CodingController.Update();
                        break;

                    case "3":
                        CodingController.GetAllRecords();
                        break;

                    case "4":
                        CodingController.Delete();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a number 0 - 4");
                        break;

                }
            }
        }

        public static string GetUserInput(string message)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (input == "0")
                GetSelection();

            return input;
        }
    }
}
