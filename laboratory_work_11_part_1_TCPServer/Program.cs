using laboratory_work_11_part_1_TCPServer.Models;
using Microsoft.EntityFrameworkCore;

namespace laboratory_work_11_part_1_TCPServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<StockPricesContext>().UseSqlServer("Data Source=localhost;Initial Catalog=StockPrices;Persist Security Info=True;User ID=sa;Password=HelloWorld10;Trust Server Certificate=True").Options;
            using StockPricesContext db = new StockPricesContext(options);

            //await db.Tickers.AddAsync(new Ticker { TickerName = "AAPL" });
            //await db.SaveChangesAsync();
            //await db.TodaysConditions.AddAsync(new TodaysCondition { TickerId = 4, State = "Raising"});
            //await db.SaveChangesAsync();
            //await db.Prices.AddAsync(new Price { TickerId = 4, PriceOnDate = 129.5, Date = new DateOnly(2024, 12, 26) });
            //await db.SaveChangesAsync();

            TcpServer tcpServer = new("127.0.0.1", 8888, db);
            TcpServer server = tcpServer;

            var servrerTask = server.Start();

            await servrerTask;

        }
    }
}