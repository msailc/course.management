using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string json = @"[
                {
                    ""id"": 1,
                    ""status"": ""Redovan""
                },
                {
                    ""id"": 2,
                    ""status"": ""Vanredan""
                }
            ]";

            string endpoint = "http://localhost:5250/studentstatus";

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data successfully sent to the database!");
            }
            else
            {
                Console.WriteLine($"Failed to send data to the database. Status code: {response.StatusCode}");
            }
        }
    }
}