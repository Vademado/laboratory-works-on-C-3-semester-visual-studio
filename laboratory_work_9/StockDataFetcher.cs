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

namespace laboratory_work_9
{
    internal class StockDataFetcher
    {
        public static string APIKeyPath { get; private set; } = null;
        private static string APIKey;
        public string StockQuotesPath { get; private set; }
        private string[] StockQuotes;
        private static HttpClient httpClient = new HttpClient();
        public JObject[] JsonResponses { get; private set; }

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
            ReadAPIKey(APIKeyPath);
        }

        public async Task GetData()
        {
            StockQuotes = await ReadStockQuotes(StockQuotesPath);
            foreach (var stockQuote in StockQuotes)
            {
                Console.WriteLine(stockQuote);
            }
            if (StockQuotes is not null)
            {
                await GetDataAsync();
            }
        }

        private static void ReadAPIKey(string? filePath)
        {
            if (filePath is null || filePath.Equals(string.Empty)) filePath = Directory.GetFiles("E:\\laboratory-works-on-C-3-semester-visual-studio\\laboratory_work_9", "api_key.txt", SearchOption.AllDirectories)[0];
            if (File.Exists(filePath))
            {
                APIKey = File.ReadAllText(filePath);
            }
            else { throw new ArgumentException("Incorrect path to the api key file"); }
        }

        private async Task<string[]> ReadStockQuotes(string? filePath)
        {
            if (filePath is null || filePath.Equals(string.Empty)) filePath = Directory.GetFiles("E:\\laboratory-works-on-C-3-semester-visual-studio\\laboratory_work_9", "ticker.txt", SearchOption.AllDirectories)[0];
            string[] tiketsArray = null;
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    await fileStream.ReadAsync(buffer, 0, buffer.Length);
                    tiketsArray = Encoding.UTF8.GetString(buffer).TrimStart('\uFEFF').Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries); // delete BOM (Byte Order Mark)
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tiketsArray;
        }

        public async Task GetDataAsync()
        {
            //Thread[] threads = new Thread[StockQuotes.Length];
            //Console.WriteLine(threads.Length);
            //for (int i = 0; i < StockQuotes.Length; i++)
            //{
            //    int index = i;
            //    threads[i] = new Thread(() => SendRequestAsync(StockQuotes[index]));
            //    threads[i].Start();
            //}
            //for (int i = 0; i < StockQuotes.Length; i++)
            //{
            //    threads[i].Join();
            //}


            Task<HttpResponseMessage>[] tasksSendRequest = new Task<HttpResponseMessage>[StockQuotes.Length];
            Task<JObject>[] tasksJsonResponse = new Task<JObject>[StockQuotes.Length];
            for (int i = 0; i < StockQuotes.Length; i++)
            {
                tasksSendRequest[i] = SendRequestAsync(StockQuotes[i]);
            }
            await Task.WhenAll(tasksSendRequest);
            for (int i = 0; i < StockQuotes.Length; i++)
            {
                tasksJsonResponse[i] = GetJsonResponse(tasksSendRequest[i].Result);
            }
            JsonResponses = await Task.WhenAll(tasksJsonResponse);
            foreach (var jsonResponse in JsonResponses)
            {
                Console.WriteLine(jsonResponse);
            }
        }

        //private JObject SendRequestAsync(string stockQuote)
        //{
        //    HttpResponseMessage responseMessage = null;
        //    try
        //    {
        //        while (responseMessage is null || responseMessage.IsSuccessStatusCode)
        //        {
        //            //string url = $"http://api.marketdata.app/v1/stocks/candles/D/{stockQuote}/?from={DateTime.Now.Year - 1}-{DateTime.Now.Month}-{DateTime.Now.Day + 1}&to={DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}";
        //            // using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

        //            string url = $"http://api.marketdata.app/v1/stocks/candles/D/{stockQuote}/?from={DateTime.Now.AddYears(-1):yyyy-MM-dd}&to={DateTime.Now:yyyy-MM-dd}&token={APIKey}";
        //            Console.WriteLine(url);

        //            ////string url = $"https://api.marketdata.app/v1/stocks/candles/D/{stockQuote}/?from={DateTime.Now.Year - 1}-{DateTime.Now.Month}-{DateTime.Now.Day + 1}&to={DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}&token={APIKey}";
        //            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        //            //requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIKey);

        //            //requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIKey);
        //            responseMessage = httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    JObject jsonResponse = GetJsonResponse(responseMessage);
        //    foreach (var item in jsonResponse)
        //    {
        //        Console.WriteLine(item + "++++++++++++++++++++++++++++++++" + stockQuote);
        //    }
        //    return jsonResponse;
        //}

        //private JObject GetJsonResponse(HttpResponseMessage responseMessages)
        //{
        //    Console.WriteLine(responseMessages);
        //    if (responseMessages is null) throw new ArgumentNullException("responseMessages");
        //    string jsonString = responseMessages.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //    JObject jsonResponse = JObject.Parse(jsonString);
        //    return jsonResponse;
        //}

        private async Task<HttpResponseMessage> SendRequestAsync(string stockQuote)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                while (responseMessage is null || !responseMessage.IsSuccessStatusCode || responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    string url = $"http://api.marketdata.app/v1/stocks/candles/D/{stockQuote}/?from={DateTime.Now.AddYears(-1):yyyy-MM-dd}&to={DateTime.Now:yyyy-MM-dd}&token={APIKey}";
                    // using HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                    responseMessage = await httpClient.SendAsync(requestMessage);
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound) Console.WriteLine($"{new string('=', 100)}\nresponseMessage.StatusCode: {responseMessage.StatusCode}\n{url}\n{new string('=', 100)}");
                }
                return responseMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return responseMessage;
        }

        private async Task<JObject> GetJsonResponse(HttpResponseMessage responseMessages)
        {
            if (responseMessages is null) throw new ArgumentNullException("responseMessages");
            string jsonString = await responseMessages.Content.ReadAsStringAsync();
            JObject jsonResponse = JObject.Parse(jsonString);
            return jsonResponse;
        }


    }
}
