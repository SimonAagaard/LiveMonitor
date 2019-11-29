using System;
using System.IO;

namespace TestConsole
{
    class Program
    {
        //Set TestConsole as startup project to run
        static void Main(string[] args)
        {
            Initialize();
            Console.WriteLine("Started and working...");
            //Insert and remove calls to methods here
            Data.Handlers.DashboardTypeHandler.Instance.CreateType().Wait(); //Seeds the DB with DashboardTypes
            //Console.WriteLine();
            Console.WriteLine("Done...");

        }

        private static void Initialize()
        {
            //Needed for the application to find the appsettings file
            Directory.SetCurrentDirectory("C:\\Projects\\LiveMonitor\\Web");
        }
    }
}
