using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.Sqlite;
using System.Globalization;
using ConsoleTableExt;


namespace Coding_Tracker_1
{
    internal class CodingController
    {
        static string connectionString = ConfigurationManager.AppSettings["k1"];
        //Duration calculation
        internal static int Duration(string startTime, string endTime)
        { 
            TimeSpan startDuration = TimeSpan.ParseExact(startTime,"hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan endDuration = TimeSpan.ParseExact(endTime, "hh\\:mm", CultureInfo.InvariantCulture);
            TimeSpan difference = (endDuration - startDuration);
            string stringDifference = difference.TotalHours.ToString();

            int final = Convert.ToInt32(stringDifference);

            return final;

        }
        public static void Insert()
        {
            Console.Clear();

            Console.WriteLine("INSERT");
            Console.WriteLine("--------");
            Console.WriteLine("Enter 0 at any time to return to Main Menu");
            Console.WriteLine("--------\n");

            string date = UserInput.GetDateInput("Please enter date of coding in MM-DD-YY format.");

            string startTime = UserInput.GetNumberInput("Enter time coding started");

            string endTime = UserInput.GetNumberInput("Enter time coding stopped");

            int duration = Duration(startTime, endTime);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = $"INSERT INTO Coding_Tracker (Date, StartTime, EndTime, Duration) VALUES ('{date}','{startTime}','{endTime}',{duration})";

                tableCommand.ExecuteNonQuery();

                connection.Close();
            }

            UserInput.GetSelection();
        }
        public static void GetAllRecords()
        {
            Console.WriteLine("ALL ENTRIES");
            Console.WriteLine("------------");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = @"SELECT * FROM Coding_Tracker";

                List<CodingSession> tableData = new List<CodingSession>();

                var reader = tableCommand.ExecuteReader();

                //Recieve DB data and store in properties
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(new CodingSession
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetString(1),
                            StartTime = reader.GetString(2),
                            EndTime = reader.GetString(3),
                            Duration = reader.GetString(4)
                        }); ;; ;
                        
                    }
                    

                }
                else
                    Console.WriteLine("No data found");

                connection.Close();

                ConsoleTableBuilder.From(tableData).WithColumn("ID", "Start Time", "End Time", "Duration in Hours", "Date").ExportAndWriteLine();
                Console.WriteLine();

            }
            
        }
        public static void Update()
        {
            Console.Clear();

            GetAllRecords();

            Console.WriteLine("------");
            Console.WriteLine("Enter 0 at any time to return to Main Menu");
            Console.WriteLine("------\n");

            var inputId = UserInput.GetNumberInput("Enter ID of record you would like to update.");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();

                string date = UserInput.GetDateInput("Please enter date of coding in MM-DD-YY format.");

                string startTime = UserInput.GetNumberInput("Please enter time coding started in 12 hour format (ex: 4:00 AM or 11:00 PM");

                string endTime = UserInput.GetNumberInput("Please enter time coding started in 12 hour format (ex: 4:00 AM or 11:00 PM");

                int duration = Duration(startTime, endTime);

                tableCommand.CommandText = $"UPDATE Coding_Tracker SET Date = '{date}', StartTime = '{startTime}', EndTime = '{endTime}', Duration = {duration} WHERE Id = {inputId}";

                int rowCount = tableCommand.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Update();
                    Console.WriteLine("Record does not exist. Please try again.");
                }
                else
                {
                    Console.WriteLine($"Record {inputId} was updated");
                    Console.WriteLine("\n\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

        }
        public static void Delete()
        {
            Console.Clear();

            Console.WriteLine("DELETE");
            Console.WriteLine("--------");
            Console.WriteLine("Enter 0 at any time to return to Main Menu");
            Console.WriteLine("--------\n");

            GetAllRecords();

            var inputId = UserInput.GetDateInput("Enter ID of record you would like to delete.");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = $"DELETE FROM Coding_Tracker WHERE Id = {inputId}";

                int rowCount = tableCommand.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine("Record does not exist. Press any key to try again.");
                    Console.ReadKey();
                    Delete();
                }

                else
                {
                    Console.WriteLine("Record was deleted. Press any key to continue");
                    Console.ReadKey();
                }

                connection.Close();

                UserInput.GetSelection();
            }
            
        }

    }
}
