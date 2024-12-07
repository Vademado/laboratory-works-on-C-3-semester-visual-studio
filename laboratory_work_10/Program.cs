// Data Source=localhost;Initial Catalog=StockPrices;User ID=sa;Password=HelloWorld10;Trust Server Certificate=True
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json.Linq;
namespace laboratory_work_10
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            StockDataFetcher stockDataFetcher = new StockDataFetcher();
            await stockDataFetcher.GetDataAsync();
            await SeedDatabase(stockDataFetcher.JsonResponses);

            Console.Write("Enter the name of the ticker: ");
            string? tickerName = Console.ReadLine();
            GetTodayCondition(tickerName);
        }

        public static async Task SeedDatabase(List<JObject> jsonResponces)
        {
            using StockPricesDbContext db = new StockPricesDbContext();
            List<Task<EntityEntry<Price>>> tasksAddPrices = new List<Task<EntityEntry<Price>>>();
            foreach (JObject response in jsonResponces)
            {
                Ticker? ticker = new Ticker() { TickerName = response["tiсker"].ToString() };
                await db.Tickers.AddAsync(ticker);
                await db.SaveChangesAsync();
                var prices = (from i in response["c"] select i.Value<float>()).ToList();
                for (int i = 0; i < prices.Count; i++)
                {
                    tasksAddPrices.Add(db.AddAsync(new Price() { TickerId = ticker.Id, PriceOnDate = prices[i], Date = new DateOnly(DateTime.Now.AddYears(-1).Year, DateTime.Now.Month, DateTime.Now.AddDays(1 + i).Day) }).AsTask());
                }
                if (prices[prices.Count - 1] > prices[prices.Count - 2]) await db.TodaysConditions.AddAsync(new TodaysCondition() { TickerId = ticker.Id, State = "Growth" });
                else if (prices[prices.Count - 1] < prices[prices.Count - 2]) await db.TodaysConditions.AddAsync(new TodaysCondition() { TickerId = ticker.Id, State = "Decline" });
                else await db.TodaysConditions.AddAsync(new TodaysCondition() { TickerId = ticker.Id, State = "Unchanged" });
            }
            await Task.WhenAll(tasksAddPrices);
            await db.SaveChangesAsync();
        }

        public static void GetTodayCondition(string tickerName, StockPricesDbContext? db = null)
        {
            if (db is null) using (db = new StockPricesDbContext()) { GetTodayCondition(tickerName, db); return; }
            var todayCondition = (from t in db.Tickers
                                  join todC in db.TodaysConditions on t.Id equals todC.TickerId
                                  where t.TickerName == "AADI"
                                  select new
                                  {
                                      TickerName = tickerName,
                                      TickerState = todC.State
                                  }).FirstOrDefault();
            Console.WriteLine($"Ticker: {todayCondition.TickerName}, State: {todayCondition.TickerState}");
        }
        public static List<Ticker> GetTickerList(StockPricesDbContext? db = null)
        {
            if (db is null) using (db = new StockPricesDbContext()) return GetTickerList(db);
            return db.Tickers.ToList();
        }

        public static List<Price> GetPricesList(StockPricesDbContext? db = null)
        {
            if (db is null) using (db = new StockPricesDbContext()) return GetPricesList(db);
            return db.Prices.ToList();
        }

        public static List<TodaysCondition> GetTodaysConditions(StockPricesDbContext? db = null)
        {
            if (db is null) using (db = new StockPricesDbContext()) return GetTodaysConditions(db);
            return db.TodaysConditions.ToList();
        }
    }
}