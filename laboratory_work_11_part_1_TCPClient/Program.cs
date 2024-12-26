namespace laboratory_work_11_part_1_TCPClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Ticker: ");
            string ticker = Console.ReadLine();
            TCPClient client = new TCPClient("127.0.0.1", 8888);
            await client.GetStockPrice(ticker);

            Console.ReadKey();

        }
    }
}