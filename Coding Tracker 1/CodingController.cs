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
        internal static string Duration(string startTime, string endTime)
        {
            DateTime startDuration = DateTime.ParseExact(startTime, "t", new CultureInfo("en-US"));
            DateTime endDuration = DateTime.ParseExact(endTime, "t", new CultureInfo("en-US"));
            double difference = (endDuration - startDuration).TotalHours;
            string final = Convert.ToString(difference);
            return final;
        }
        public static void Insert()
        {

            string date = UserInput.GetUserInput("Please enter date of coding in MM-DD-YY format.");

            string startTime = UserInput.GetUserInput("Enter time coding started. Enter 0 to return to main menu");

            string endTime = UserInput.GetUserInput("Enter time coding stopped. Enter 0 to return to main menu");

            string duration = Duration(startTime, endTime);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = $"INSERT INTO Coding_Tracker (Date, StartTime, EndTime, Duration) VALUES ('{date}','{startTime}','{endTime}','{duration}')";

                tableCommand.ExecuteNonQuery();

                connection.Close();
            }
            Console.WriteLine($"\n\nYou coded for {duration} hours\n\n");
        }
        public static void GetAllRecords()
        {
            Console.Clear();

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

            var inputId = UserInput.GetUserInput("Enter ID of record you would like to update.");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();

                string date = UserInput.GetUserInput("Please enter date of coding in MM-DD-YY format.");

                string startTime = UserInput.GetUserInput("Please enter time coding started in 12 hour format (ex: 4:00 AM or 11:00 PM");

                string endTime = UserInput.GetUserInput("Please enter time coding started in 12 hour format (ex: 4:00 AM or 11:00 PM");

                string duration = Duration(startTime, endTime);

                tableCommand.CommandText = $"UPDATE Coding_Tracker SET Date = '{date}', StartTime = '{startTime}', EndTime = '{endTime}', Duration = '{duration}' WHERE Id = {inputId}";

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
            GetAllRecords();

            var inputId = UserInput.GetUserInput("Enter ID of record you would like to delete.");

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

            }
            
        }

    }
}
