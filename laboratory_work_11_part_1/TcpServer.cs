using laboratory_work_10;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace laboratory_work_11
{
    internal class TcpServer
    {
        public TcpListener tcpListener { get; init; }
        public IPAddress localaddr { get; init; }
        public int port { get; init; }
        private StockPricesDbContext db { get; set; }
        public TcpServer(IPAddress localaddr, int port, StockPricesDbContext db)
        {
            this.localaddr = localaddr;
            this.port = port;
            tcpListener = new TcpListener(this.localaddr, this.port);
            this.db = db;
        }

        public TcpServer(string localaddr, int port, StockPricesDbContext db) : this(IPAddress.Parse(localaddr), port, db) { }

        public async Task Start()
        {
            try
            {
                tcpListener.Start();
                while (true)
                {
                    using var tcpClient = await tcpListener.Server.AcceptAsync();

                    byte[] sizeBuffer = new byte[4];
                    await tcpClient.ReceiveAsync(sizeBuffer);
                    int sizeRequestData = BitConverter.ToInt32(sizeBuffer, 0);

                    byte[] requestData = new byte[sizeRequestData];
                    int requestBytes = await tcpClient.ReceiveAsync(requestData);
                    var requestMessage = Encoding.UTF8.GetString(requestData, 0, requestBytes);

                    double responseMessage;
                    var tickerId = (from ticker in db.Tickers
                                    where ticker.TickerName == requestMessage
                                    select ticker.Id).FirstOrDefault(-1);
                    var lastPrice = (from price in db.Prices
                                     where tickerId == price.TickerId
                                     select price.PriceOnDate).LastOrDefault(-1.0);
                    if (tickerId == -1) responseMessage = lastPrice;
                    else responseMessage = -1.0;


                    byte[] responseData = BitConverter.GetBytes(responseMessage);
                    byte[] sizeResponceData = BitConverter.GetBytes(responseData.Length);
                    await tcpClient.SendAsync(sizeResponceData);
                    await tcpClient.SendAsync(responseData);
                }
            }
            finally
            {
                tcpListener.Stop();
            }

        }


    }
}
