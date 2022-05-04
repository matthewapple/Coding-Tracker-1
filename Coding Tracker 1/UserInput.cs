using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;



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
                Console.WriteLine("      MAIN MENU\n");
                Console.WriteLine("----------------------");
                Console.WriteLine("\nPlease select an option");
                Console.WriteLine("\n----------------------");
                Console.WriteLine("\nType 0 to Close Application");
                Console.WriteLine("Type 1 to Insert an Entry");
                Console.WriteLine("Type 2 to Update an Entry");
                Console.WriteLine("Type 3 to View all Entries");
                Console.WriteLine("Type 4 to Delete an Entry");
                Console.WriteLine("Type 5 to Track a Session");

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
                        Console.Clear();
                        CodingController.GetAllRecords();
                        break;

                    case "4":
                        CodingController.Delete();
                        break;
                    case "5":
                        CodingController.RunStopWatch();
                        break;
                    
                    default:
                        Console.WriteLine("Invalid input. Please enter a number 0 - 4");
                        break;

                }
            }
        }

        public static string GetDateInput()
        {
            Console.WriteLine("Please enter date of coding in MM-DD-YY format.");

            string input = Console.ReadLine();
            
            if (input == "0")
                GetSelection();
            
            while (!DateTime.TryParseExact(input, "MM-dd-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid format. (Format MM-DD-YY required). Type 0 to return to main menu, or try again");
                input = Console.ReadLine();
            };

            return input;
        }
        public static string GetTimeInput()
        {
            Console.WriteLine("Enter time coding started (24 hour clock (HH:MM) only)");

            string input = Console.ReadLine();

            if (input == "0")
                GetSelection();
            while (!DateTime.TryParseExact(input, "H:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid format. HH:MM required");
                input = Console.ReadLine();
            };

            return input;
        }
        public static string GetNumberInput(string message)
        {
            Console.WriteLine(message);

            string input = Console.ReadLine();

            if (input == "0")
                GetSelection();

            while ((!Int32.TryParse(input, out _)) || (Convert.ToInt32(input) < 0))
            {
                Console.WriteLine("Invalid input. Please try again.");
                input = Console.ReadLine();
                
            }

            return input;
        }
    }
}
