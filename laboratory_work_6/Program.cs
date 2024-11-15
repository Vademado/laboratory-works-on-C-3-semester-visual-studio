using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            List<Weather> weatherInLocations = LocationWeatherService.GetWeather(50);

            LocationWeatherService.GetCountryWithExtremeTemperature(weatherInLocations);
            Console.WriteLine(new string('-', 100));

            LocationWeatherService.CalculateGlobalAverageTemperature(weatherInLocations);
            Console.WriteLine(new string('-', 100));

            LocationWeatherService.GetUniqueCountryCount(weatherInLocations);
            Console.WriteLine(new string('-', 100));

            LocationWeatherService.GetFirstLocationsWithWeatherDescriptions(weatherInLocations, "clear sky", "rain", "few clouds");
        }
    }

    class Weather
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public double Temp { get; set; }
        public string Description { get; set; }
        private static string APIKey;
        private double latitude, longitude;
        private static Random random = new Random();
        private static HttpClient httpClient = new HttpClient();

        static Weather()
        {
            ReadAPIKey();
        }

        public Weather()
        {
            GetData().GetAwaiter().GetResult();
        }

        private static void ReadAPIKey(string filePath = "D:\\laboratory-works-on-C-3-semester-visual-studio\\laboratory_work_6\\api_key.txt")
        {
            if (File.Exists(filePath))
            {
                APIKey = File.ReadAllText(filePath);
            }
            else { throw new ArgumentException("Incorrect path to the api key file"); }
        }

        public async Task GetData()
        {
            while (Country is null || Name is null)
            {
                latitude = random.NextDouble() + random.Next(-90, 91);
                longitude = random.NextDouble() + random.Next(-180, 181);
                string URL = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={APIKey}";
                try
                {
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, URL);
                    HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

                    if (HttpStatusCode.OK.Equals(responseMessage.StatusCode))
                    {
                        string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                        JObject jObject = JObject.Parse(jsonResponse);

                        Country = jObject["sys"]["country"]?.ToString();
                        Name = jObject["name"]?.ToString();
                        Temp = Convert.ToDouble(jObject["main"]["temp"]);
                        Description = jObject["weather"][0]["description"]?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.TargetSite);
                }

            }
        }
    }


    static class LocationWeatherService
    {
        public static List<Weather> GetWeather(int numberOfLocations)
        {
            List<Weather> weatherInLocations = new List<Weather>(numberOfLocations);
            for (int i = 0; i < numberOfLocations; i++)
            {
                weatherInLocations.Add(new Weather());
            }
            return weatherInLocations;
        }

        public static void GetCountryWithExtremeTemperature(List<Weather> weatherInLocations)
        {
            double minTemp = weatherInLocations.Min(x => x.Temp);
            double maxTemp = weatherInLocations.Max(x => x.Temp);

            var CountryWithMinTemp = from x in weatherInLocations
                                     where x.Temp == minTemp
                                     select x.Country;

            var CountryWithMaxTemp = from x in weatherInLocations
                                     where x.Temp == maxTemp
                                     select x.Country;

            Console.WriteLine($"Country with the lowest temperature ({minTemp} K): {string.Join(", ", CountryWithMinTemp)}");
            Console.WriteLine($"Country with the highest temperature ({maxTemp} K): {string.Join(", ", CountryWithMaxTemp)}");
        }

        public static void CalculateGlobalAverageTemperature(List<Weather> weatherInLocations)
        {
            double averageTemp = weatherInLocations.Average(x => x.Temp);

            Console.WriteLine($"Global average temperature: {averageTemp} K");
        }

        public static void GetUniqueCountryCount(List<Weather> weatherInLocations)
        {
            int uniqueCountryCount = weatherInLocations.Select(x => x.Country).Distinct().Count();

            Console.WriteLine($"The number of countries: {uniqueCountryCount}");
        }

        public static void GetFirstLocationsWithWeatherDescriptions(List<Weather> weatherInLocations, params string[] descriptions)
        {
            foreach (string description in descriptions)
            {
                var location = weatherInLocations.FirstOrDefault(x => x.Description == description);

                Console.WriteLine($"Locations with weather description '{description}': {location?.Name ?? "None"}");
            }
        }
    }
}
