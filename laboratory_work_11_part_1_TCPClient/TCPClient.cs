using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace laboratory_work_11_part_1_TCPClient
{
    internal class TCPClient
    {
        public TcpClient tcpClient { get; init; } = new TcpClient();
        public IPAddress localaddr { get; init; }
        public int port { get; init; }

        public TCPClient(IPAddress localaddr, int port)
        {
            this.localaddr = localaddr;
            this.port = port;
        }

        public TCPClient(string localaddr, int port) : this(IPAddress.Parse(localaddr), port) { }
        public async Task InitializeAsync(IPAddress localaddr, int port)
        {
            await tcpClient.ConnectAsync(localaddr, port);
        }

        public async Task GetStockPrice(string ticker)
        {
            try
            {
                await InitializeAsync(localaddr, port);
                byte[] data = Encoding.UTF8.GetBytes(ticker);
                byte[] size = BitConverter.GetBytes(data.Length);

                await tcpClient.Client.SendAsync(size);
                await tcpClient.Client.SendAsync(data);

                byte[] sizeBuffer = new byte[4];
                await tcpClient.Client.ReceiveAsync(sizeBuffer);
                int sizeResponceData = BitConverter.ToInt32(sizeBuffer, 0);

                byte[] responceData = new byte[sizeResponceData];
                int responceBytesLen = await tcpClient.Client.ReceiveAsync(responceData);
                var responceMessage = BitConverter.ToDouble(responceData, 0);

                Console.WriteLine($"Last ticker {ticker} price: {responceMessage}");
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}