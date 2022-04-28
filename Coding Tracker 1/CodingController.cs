using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.Sqlite;
using System.Globalization;


namespace Coding_Tracker_1
{
    internal class CodingController
    {
        static string connectionString = ConfigurationManager.AppSettings["k1"];
        public static void Insert()
        {
            CodingSession session = new CodingSession();
            
            string date = UserInput.GetDateInput();

            string startTime = UserInput.GetNumberInput("Enter time coding started. Enter 0 to return to main menu");

            string endTime = UserInput.GetNumberInput("Enter time coding stopped. Enter 0 to return to main menu");

            //Duration calculation
            DateTime startDuration = DateTime.ParseExact(startTime,"t", new CultureInfo("en-US"));
            DateTime endDuration = DateTime.ParseExact(endTime, "t", new CultureInfo("en-US"));
            double difference = (endDuration - startDuration).TotalHours;
            string duration = Convert.ToString(difference);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = $"INSERT INTO Coding_Tracker (Date, StartTime, EndTime, Duration) VALUES ('{date}','{startTime}','{endTime}','{duration}')";

                tableCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static void GetAllRecords()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                var tableCommand = connection.CreateCommand();

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
                            Date = DateTime.ParseExact(reader.GetString(1), "MM-dd-yy", new CultureInfo("en-US")),
                            StartTime = DateTime.ParseExact(reader.GetString(2),"t", new CultureInfo("en-US")),
                            EndTime = DateTime.ParseExact(reader.GetString(3),"t", new CultureInfo("en-US")),
                        });; ;
                        
                        
                    }

                }
                else
                    Console.WriteLine("No data found");

                connection.Close();


            }
        }
    }
}
