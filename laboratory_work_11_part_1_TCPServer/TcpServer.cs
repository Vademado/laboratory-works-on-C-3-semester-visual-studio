using System.Net;
using System.Net.Sockets;
using System.Text;
using laboratory_work_11_part_1_TCPServer.Models;

namespace laboratory_work_11_part_1_TCPServer
{
    internal class TcpServer
    {
        public TcpListener tcpListener { get; init; }
        public IPAddress localaddr { get; init; }
        public int port { get; init; }
        private StockPricesContext db { get; set; }
        public TcpServer(IPAddress localaddr, int port, StockPricesContext db)
        {
            this.localaddr = localaddr;
            this.port = port;
            tcpListener = new TcpListener(this.localaddr, this.port);
            this.db = db;
        }

        public TcpServer(string localaddr, int port, StockPricesContext db) : this(IPAddress.Parse(localaddr), port, db) { }

        public async Task Start()
        {
            try
            {
                tcpListener.Start();
                Console.WriteLine("TCP server is running");
                while (true)
                {
                    using var tcpClient = await tcpListener.Server.AcceptAsync();

                    byte[] sizeBuffer = new byte[4];
                    await tcpClient.ReceiveAsync(sizeBuffer);
                    int sizeRequestData = BitConverter.ToInt32(sizeBuffer, 0);

                    byte[] requestData = new byte[sizeRequestData];
                    int requestBytesLen = await tcpClient.ReceiveAsync(requestData);
                    var requestMessage = Encoding.UTF8.GetString(requestData, 0, requestBytesLen);

                    double responseMessage;
                    var tickerId = (from ticker in db.Tickers
                                    where ticker.TickerName == requestMessage
                                    select ticker.Id).FirstOrDefault();
   
                    responseMessage = (from price in db.Prices
                                     where tickerId == price.TickerId
                                     orderby price.Date 
                                     select (double)price.PriceOnDate).LastOrDefault();

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
