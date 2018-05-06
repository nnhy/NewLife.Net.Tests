using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NewLife.Log;
using NewLife.Net;

namespace EchoTest
{
    class Program
    {
        static void Main(String[] args)
        {
            XTrace.UseConsole();

            try
            {
                Console.Write("请选择运行模式：1，服务端；2，客户端  ");
                var ch = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (ch == '1')
                    TestServer();
                else
                    TestClient();
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
            }

            Console.WriteLine("OK!");
            Console.ReadKey();
        }

        static NetServer _server;
        static void TestServer()
        {
            // 实例化服务端，指定端口，同时在Tcp/Udp/IPv4/IPv6上监听
            var svr = new MyNetServer
            {
                Port = 1234,
                Log = XTrace.Log
            };
            svr.Start();

            _server = svr;
        }

        static void TestClient()
        {
            var uri = new NetUri("tcp://127.0.0.1:1234");
            var client = uri.CreateRemote();
            client.Log = XTrace.Log;
            client.Received += (s, e) =>
            {
                XTrace.WriteLine("收到：{0}", e.Packet.ToStr());
            };
            client.Open();

            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);

                var str = "你好" + (i + 1);
                client.Send(str);
            }

            client.Dispose();
        }
    }
}