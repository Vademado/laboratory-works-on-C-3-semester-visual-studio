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
