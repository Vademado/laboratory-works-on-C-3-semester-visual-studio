using laboratory_work_10;

namespace laboratory_work_11
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using StockPricesDbContext db = new StockPricesDbContext();
            TcpServer server = new TcpServer("127.0.0.1", 8888, db);
            Client client = new Client("127.0.0.1", 8888);

            var servrerTask = server.Start();
            await client.GetStockPrice("AAPL");
            //client.GetStockPrice();

            await servrerTask;

        }
    }
}