using System;
using System.IO;

namespace TestConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Initialize();
            Console.WriteLine("Started and working...");
            //Insert and remove calls to methods here
            Data.Handlers.DashboardTypeHandler.Instance.CreateType().Wait(); //Seeds the DB with DashboardTypes
            Console.WriteLine("Done...");
            Console.ReadLine();

        }

        private static void Initialize()
        {
            //Needed for the application to find appsettings
            Directory.SetCurrentDirectory("C:\\Projects\\LiveMonitor\\Web");
        }
    }
}
