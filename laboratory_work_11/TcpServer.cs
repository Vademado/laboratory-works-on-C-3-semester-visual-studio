using laboratory_work_10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace laboratory_work_11
{
    internal class TcpServer
    {
        public TcpListener tcpListener { get; init; }
        public IPAddress localaddr {  get; init; }
        public int port { get; init; }
        public TcpServer(IPAddress localaddr, int port)
        {
            this.localaddr = localaddr;
            this.port = port;
            tcpListener = new TcpListener(this.localaddr, this.port);
        }

        public TcpServer(string localaddr, int port) : this(IPAddress.Parse(localaddr), port) { }

        public async Task Start()
        {   try
            {
                tcpListener.Start();
                while (true)
                {
                    using TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                    //using StockPricesDbContext db = new StockPricesDbContext();


                    byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString());
                    // отправляем данные
                    await tcpClient.Client.SendAsync(data);
                    Console.WriteLine($"Клиенту {tcpClient.Client.RemoteEndPoint} отправлены данные");
                }
            }
            finally
            {
                tcpListener.Stop();
            }

        }


    }
}
