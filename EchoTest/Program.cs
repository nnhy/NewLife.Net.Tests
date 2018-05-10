using NewLife.Log;
using NewLife.Net;
using NewLife.Threading;
using System;
using System.Threading;

namespace EchoTest
{
    internal class Program
    {
        private static String _port;
        private static TimerX _timer;
        private static NetServer _server;

        private static void Main(String[] args)
        {
            XTrace.UseConsole();

            try
            {
                Console.Write("请输入需要连接的端口号：");
                _port = Console.ReadLine();
                Console.WriteLine();
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

        private static void TestServer()
        {
            // 实例化服务端，指定端口，同时在Tcp/Udp/IPv4/IPv6上监听
            var svr = new MyNetServer
            {
                Port = _port.ToInt(),
                Log = XTrace.Log
            };
            svr.Start();

            _server = svr;

            // 定时显示性能数据
            _timer = new TimerX(ShowStat, svr, 100, 1000);
        }

        private static void TestClient()
        {
            var uri = new NetUri("tcp://127.0.0.1:" + _port);
            //var uri = new NetUri("tcp://net.newlifex.com:1234");
            var client = uri.CreateRemote();
            client.Log = XTrace.Log;
            client.Received += (s, e) =>
            {
                XTrace.WriteLine("收到：{0}", e.Packet.ToStr());
            };
            client.Open();

            // 定时显示性能数据
            _timer = new TimerX(ShowStat, client, 100, 1000);

            // 循环发送数据
            for (var i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);

                var str = "你好" + (i + 1);
                client.Send(str);
            }

            client.Dispose();
        }

        private static void ShowStat(object state)
        {
            var msg = String.Empty;
            switch (state)
            {
                case NetServer ns:
                    msg = "Server：" + ns.GetStat();
                    break;
                case ISocketRemote ss:
                    msg = "Remote：" + ss.GetStat();
                    break;
            }
            Console.Title = msg;
        }
    }
}