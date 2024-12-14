namespace laboratory_work_11
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TcpServer server = new TcpServer("127.0.0.1", 8888);
            Client client = new Client("127.0.0.1", 8888);

            var servrerTask = server.Start();
            await client.GetStockPrice("AAPL");
            //client.GetStockPrice();

            await servrerTask;

        }
    }
}