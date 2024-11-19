using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace laboratory_work_9
{
    internal class StockDataFetcher
    {
        public static string APIKeyPath { get; private set; } = null;
        private static string APIKey;
        public string StockQuotesPath { get; private set; }
        private List<string> StockQuotes;
        public List<JObject> JsonResponses { get; private set; }
        public Dictionary<string, double> DictionaryStockQuotesYearlyAveragePrice { get; private set; }
        private static HttpClient httpClient = new HttpClient();

        public StockDataFetcher(string apiKeyPath = null, string stockQuotesPath = null)
        {
            StockQuotesPath = stockQuotesPath;
            if (APIKeyPath != apiKeyPath)
            {
                APIKeyPath = apiKeyPath;
                ReadAPIKey(APIKeyPath);
            }
        }

        static StockDataFetcher()
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIKey);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            ReadAPIKey(APIKeyPath);
        }

        private static void ReadAPIKey(string? filePath)
        {
            if (filePath is null || filePath.Equals(string.Empty))
            {
                string[] files = Directory.GetFiles("E:\\laboratory-works-on-C-3-semester-visual-studio\\laboratory_work_9_part_1", "api_key.txt", SearchOption.AllDirectories);
                if (files.Length > 0)
                    filePath = files[0];
                else
                    throw new FileNotFoundException("File api_key.txt not found.");
            }
            if (File.Exists(filePath))
            {
                APIKey = File.ReadAllText(filePath);
            }
            else { throw new ArgumentException("Incorrect path to the api key file"); }
        }

        private List<string> ReadStockQuotes(string? filePath)
        {
            if (filePath is null || filePath.Equals(string.Empty))
            {
                string[] files = Directory.GetFiles("E:\\laboratory-works-on-C-3-semester-visual-studio\\laboratory_work_9_part_1", "ticker.txt", SearchOption.AllDirectories);
                if (files.Length > 0)
                    filePath = files[0];
                else
                    throw new FileNotFoundException("File ticker.txt not found.");
            }
            List<string> stockQuotesList = null;
            if (File.Exists(filePath))
            {
                stockQuotesList = new List<string>(File.ReadAllLines(filePath));
            }
            else throw new ArgumentException("Incorrect path to the stock quotes file");
            return stockQuotesList;
        }

        public async Task GetDataAsync()
        {
            if (StockQuotes is null) StockQuotes = ReadStockQuotes(StockQuotesPath);
            Task<JObject>[] tasksJsonResponse = new Task<JObject>[StockQuotes.Count];
            for (int i = 0; i < StockQuotes.Count; i++)
            {
                tasksJsonResponse[i] = SendRequestAndParseResponseAsync(StockQuotes[i]);
            }
            JsonResponses = new List<JObject>(await Task.WhenAll(tasksJsonResponse));
        }

        private async Task<HttpResponseMessage> SendRequestAsync(string stockQuote)
        {
            string url = $"https://api.marketdata.app/v1/stocks/candles/D/{stockQuote}/?from={DateTime.Now.AddYears(-1).AddDays(1):yyyy-MM-dd}&to={DateTime.Now:yyyy-MM-dd}&token={APIKey}";
            HttpResponseMessage responseMessage = null;
            try
            {
                while (responseMessage is null || (!responseMessage.IsSuccessStatusCode && responseMessage.StatusCode != System.Net.HttpStatusCode.NotFound))
                {
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                    responseMessage = await httpClient.SendAsync(requestMessage);
                    Console.WriteLine($"{new string('=', 110)}\nresponseMessage.StatusCode: {responseMessage.StatusCode}\n{url}\n{new string('=', 110)}");

                }
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine($"{e.Message}\nRequest to {url} was canceled due to timeout");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + $"\n{url}");
            }
            return responseMessage;
        }

        private async Task<JObject> GetJsonResponse(HttpResponseMessage responseMessages)
        {
            //if (responseMessages is null) throw new ArgumentNullException("responseMessages");
            if (responseMessages is null) return new JObject();
            string jsonString = await responseMessages.Content.ReadAsStringAsync();
            return JObject.Parse(jsonString);
        }

        private async Task<JObject> SendRequestAndParseResponseAsync(string stockQuote)
        {
            HttpResponseMessage responseMessages = await SendRequestAsync(stockQuote);
            return await GetJsonResponse(responseMessages);
        }

        public Task<double> GetYearlyAverageStockQuotePriceAsync(JObject stockQuote)
        {
            List<double> lows = stockQuote["l"]?.ToObject<List<double>>() ?? new List<double>();
            List<double> highs = stockQuote["h"]?.ToObject<List<double>>() ?? new List<double>();
            if (lows.Count != highs.Count) throw new ArgumentException("The number of lows and highs in the stock quote is not equal");
            var dailyAverages = lows.Zip(highs, (low, high) => (low + high) / 2);
            if (dailyAverages.Count() == 0) return Task.FromResult(0.0);
            //return dailyAverages.Average();
            return Task.FromResult(dailyAverages.Average());
        }

        public async Task GetDictionaryYearlyAverageStockQuotesPriceAsync()
        {
            if (JsonResponses is null) await GetDataAsync();
            Task<double>[] yearAveragePrices = new Task<double>[JsonResponses.Count];
            for (int i = 0; i < JsonResponses.Count; i++)
            {
                //yearAveragePrices[i] = Task.Run(() => GetYearlyAverageStockQuotePriceAsync(JsonResponses[i]));
                yearAveragePrices[i] = GetYearlyAverageStockQuotePriceAsync(JsonResponses[i]);
            }
            double[] yearlyAverageStockQuotesPrice = await Task.WhenAll(yearAveragePrices);
            DictionaryStockQuotesYearlyAveragePrice = new Dictionary<string, double>();
            for (int i = 0; i < StockQuotes.Count; i++)
            {
                DictionaryStockQuotesYearlyAveragePrice.Add(StockQuotes[i], yearlyAverageStockQuotesPrice[i]);
            }
        }

        public async Task WriteStockQuotesToFileAsync()
        {
            try
            {
                if (DictionaryStockQuotesYearlyAveragePrice is null) await GetDictionaryYearlyAverageStockQuotesPriceAsync();
                using FileStream fileStream = new FileStream(Path.Combine("E:\\laboratory-works-on-C-3-semester-visual-studio\\laboratory_work_9_part_1", "YearlyAverageStockQuotesPrice.txt"), FileMode.OpenOrCreate);
                foreach (var stockQuote in DictionaryStockQuotesYearlyAveragePrice)
                {
                    byte[] buffer = Encoding.Default.GetBytes($"{stockQuote.Key}:{stockQuote.Value.ToString()}\n");
                    await fileStream.WriteAsync(buffer, 0, buffer.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
