using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_6
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            GetData();
        }

        private void ReadAPIKey(string filePath = "D:/laboratory works on C# 3 semester visual studio/laboratory_work_6/api_key.txt")
        {
            if (File.Exists(filePath))
            {
                APIKey = File.ReadAllText(filePath);
            }
            else { throw new ArgumentException("Incorrect path to the api key file"); }
        }

        private async void GetData()
        {
            string URL = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={APIKey}";
            try
            {
                //HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, URL);
                //HttpResponseMessage responseMessage = httpClient.SendAsync(requestMessage).Result;
                HttpResponseMessage response = await httpClient.GetAsync(URL);
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content, 12);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.TargetSite);
            }
        }
    }
}
