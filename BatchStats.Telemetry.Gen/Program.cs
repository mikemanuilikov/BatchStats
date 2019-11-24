using BatchStats.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatchStats.Telemetry.Gen
{
    class Program
    {
        private static HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Starting simulated generator, press enter to exit!");
            Task.Run(async () => await RunSimulatedGeneratorDataAsync());
            Console.ReadLine();
        }

        private static async Task RunSimulatedGeneratorDataAsync()
        {
            var rand = new Random();

            var nextAnomalousEvent = DateTime.Now.AddSeconds(30);

            while (true)
            {
                int batchId = (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() % 10000);

                var batchStages = rand.Next(1, 5);

                for (int i = 0; i < batchStages; i++)
                {
                    var temperature = new DataPoint
                    {
                        BatchId = batchId.ToString(),
                        SensorId = "Temperature",
                        Value = rand.Next(15, 40),
                        BatchTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    };

                    var rpms = new DataPoint
                    {
                        BatchId = batchId.ToString(),
                        SensorId = "Pressure",
                        Value = rand.Next(500, 600),
                        BatchTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                    };

                    if (nextAnomalousEvent < DateTime.Now)
                    {
                        Console.WriteLine("**GENERATING ANOMALY!");
                        temperature.Value = rand.Next(40, 60);
                        rpms.Value = rand.Next(750, 900);
                        nextAnomalousEvent = DateTime.Now.AddSeconds(30);
                    }
                    else
                    {
                        Console.WriteLine("Sending normal message...");
                    }

                    await SendTelemetry(temperature);
                    await SendTelemetry(rpms);

                    Thread.Sleep(1000);
                }
            }
        }

        private static async Task SendTelemetry(DataPoint telemetry)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(telemetry), Encoding.UTF8, "application/json");
            await httpClient.PostAsync("https://batchstatsapi.azurewebsites.net/sensors/telemetry", stringContent);
        }
    }
}