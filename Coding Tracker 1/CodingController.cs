using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.Sqlite;
namespace Coding_Tracker_1
{
    internal class CodingController
    {
        static string connectionString = ConfigurationManager.AppSettings["k1"];
        public static void Insert()
        {
            CodingSession session = new CodingSession();
            
            session.Date = UserInput.GetDateInput();

            session.StartTime = UserInput.GetNumberInput("Enter time coding started. Enter 0 to return to main menu");

            session.EndTime = UserInput.GetNumberInput("Enter time coding stopped. Enter 0 to return to main menu");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = $"INSERT INTO drinking_water (Date, Quantity) VALUES ('{date}',{quantity})";

                tableCommand.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
