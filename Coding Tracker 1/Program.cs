using System;
using System.Configuration;
using System.Collections.Specialized;


namespace Coding_Tracker_1
{
    internal class Program
    {
        public static string connectionString = ConfigurationManager.AppSettings["k1"];
        static void Main(string[] args)
        {

            Console.WriteLine($"{connectionString}");
        }
    }
}
