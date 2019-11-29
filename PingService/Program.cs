using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PingService
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (DateTime.Now.Second == 01)
                {
                    Console.WriteLine("DataSet created: " + DateTime.UtcNow.ToShortTimeString());

                    // Call the method to ping our endpoint with dataset creation
                    var pingservice = new PingMonitor();
                    pingservice.PingMethod().Wait();

                    Thread.Sleep(1000);
                }
            }
        }
    }

    // class with a method which is used to ping our service every 10 seconds to create new datasets
    public class PingMonitor
    {
        public PingMonitor() { }

        //Method which calls the endpoint of our service which currently creates any new datasets not yet in the database
        public async Task PingMethod()
        {
            Uri Url = new Uri("https://livemonitortest.azurewebsites.net/dataset");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(Url);
            }
        }
    }
}