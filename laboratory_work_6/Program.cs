using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace laboratory_work_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Weather a = new Weather();
        }
    }

    class Weather
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public double Temp { get; set; }
        public string Description { get; set; }
        private string APIKey;
        private double latitude, longitude;
        private static Random random = new Random();
        private static HttpClient httpClient = new HttpClient();

        public Weather()
        {
            latitude = random.NextDouble() + random.Next(-90, 91);
            longitude = random.NextDouble() + random.Next(-180, 181);
            ReadAPIKey();
            GetData().GetAwaiter().GetResult();
        }

        private void ReadAPIKey(string filePath = "D:/laboratory works on C# 3 semester visual studio/laboratory_work_6/api_key.txt")
        {
            if (File.Exists(filePath))
            {
                APIKey = File.ReadAllText(filePath);
            }
            else { throw new ArgumentException("Incorrect path to the api key file"); }
        }

        public async Task GetData()
        {
            string URL = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={APIKey}";
            try
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, URL);
                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");

                Console.WriteLine(new string('-', 50));
                JObject jObject = JObject.Parse(jsonResponse);
                Console.WriteLine(jObject);
                Console.WriteLine(new string('-', 50));
                if (jObject["sys"]["country"] != null) { Console.WriteLine("None"); }
                else { Console.WriteLine(jObject["sys"]["country"]); }
                if (jObject["name"]  != null) { Console.WriteLine("None"); }
                else { Console.WriteLine(jObject["name"]); }
                Console.WriteLine(jObject["sys"]["country"]);
                Console.WriteLine(jObject["name"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.TargetSite);
            }
        }
    }
}
