using System;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Data.Sqlite;
using ConsoleTableExt;


namespace Coding_Tracker_1
{
    internal class Program
    {
       

        static string connectionString = ConfigurationManager.AppSettings["k1"];
        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCommand = connection.CreateCommand();
                tableCommand.CommandText = @"CREATE TABLE IF NOT EXISTS Coding_Tracker 
                    (Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date TEXT,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration INT)";

                tableCommand.ExecuteNonQuery();

                connection.Close();
            }

            UserInput.GetSelection();

        }
    }
}
