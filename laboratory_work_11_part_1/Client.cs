using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_11
{
    internal class Client
    {
        public TcpClient tcpClient { get; init; } = new TcpClient();
        public IPAddress localaddr { get; init; }
        public int port { get; init; }

        public Client(IPAddress localaddr, int port)
        {
            this.localaddr = localaddr;
            this.port = port;
        }

        public Client(string localaddr, int port) : this(IPAddress.Parse(localaddr), port) { }
        public async Task InitializeAsync(IPAddress localaddr, int port)
        {
            await tcpClient.ConnectAsync(localaddr, port);
        }

        public async Task GetStockPrice(string ticker)
        {
            try
            {
                await InitializeAsync(localaddr, port);
                // https://metanit.com/sharp/net/3.5.php
                byte[] data = Encoding.UTF8.GetBytes(ticker);
                byte[] size = BitConverter.GetBytes(data.Length);

                await tcpClient.Client.SendAsync(size);
                await tcpClient.Client.SendAsync(data);

                Console.WriteLine("Сообщение отправлено");
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}
