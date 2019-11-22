using System;
using System.IO;
using Data.Handlers;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
            Console.WriteLine("Started and working...");
            Data.Handlers.DashboardTypeHandler.Instance.CreateType();
            Console.WriteLine("Done...");
            Console.ReadLine();

        }

        private static void Initialize()
        {
            Directory.SetCurrentDirectory("C:\\Projects\\LiveMonitor\\Web");
        }
    }
}
