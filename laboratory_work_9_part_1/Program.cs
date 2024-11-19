using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#nullable enable

namespace laboratory_work_9
{
    internal class Program
    {
        async static Task Main(string[] args)
        {
            StockDataFetcher stockDataFetcher = new StockDataFetcher();
            await stockDataFetcher.WriteStockQuotesToFileAsync();
        }
    }
}
