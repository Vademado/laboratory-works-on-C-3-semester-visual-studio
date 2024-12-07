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
                // буфер для считывания данных
                byte[] data = new byte[512];

                // получаем данные из потока
                int bytes = await tcpClient.Client.ReceiveAsync(data);
                // получаем отправленное время
                string time = Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine($"Текущее время: {time}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
