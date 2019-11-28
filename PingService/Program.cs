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
            var pingservice = new PingMonitor();

            // As long as this service runs it will call the PingMethod every 30 seconds
            while (true)
            {
                pingservice.PingMethod().Wait();

                Console.WriteLine("Waiting 30 seconds to ping again");
                Thread.Sleep(30000);
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
            Uri Url = new Uri("https://livemonitorapp.azurewebsites.net/dataset");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(Url);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode + "  --  " + response.ReasonPhrase);
                }
                else
                {
                    Console.WriteLine("Not successful statuscode: " + response.StatusCode);
                }
            }
        }
    }
}