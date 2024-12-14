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
                    using var tcpClient = await tcpListener.Server.AcceptAsync();

                    byte[] sizeBuffer = new byte[4];
                    await tcpClient.ReceiveAsync(sizeBuffer);
                    int size = BitConverter.ToInt32(sizeBuffer, 0);

                    byte[] data = new byte[size];
                    int bytes = await tcpClient.ReceiveAsync(data);
                    var message = Encoding.UTF8.GetString(data, 0, bytes);
                    Console.WriteLine($"Размер сообщения: {size} байтов");
                    Console.WriteLine($"Сообщение: {message}");
                }
            }
            finally
            {
                tcpListener.Stop();
            }

        }


    }
}
