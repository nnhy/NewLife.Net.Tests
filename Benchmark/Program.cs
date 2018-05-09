using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;

namespace Benchmark
{
    class Program
    {
        static void Main(String[] args)
        {
            XTrace.UseConsole();

            try
            {
                var cfg = new Config();

                // 分解参数
                if (args != null && args.Length > 0) cfg.Parse(args);

                // 显示帮助菜单或执行
                if (cfg.Address.IsNullOrEmpty())
                    ShowHelp();
                else
                    Work(cfg);
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex.GetTrue());
            }

            //Console.WriteLine("OK!");
            //Console.ReadKey();
        }

        static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("压力测试工具：nc [-n 1000] [-c 10] [-s] tcp://127.0.0.1:1234");
            Console.WriteLine("\t-n\t请求数");
            Console.WriteLine("\t-c\t并发数");
            Console.WriteLine("\t-s\t字符串内容");

            Console.ResetColor();
        }

        static void Work(Config cfg)
        {
            var uri = new NetUri(cfg.Address);
            if (cfg.Content.IsNullOrEmpty()) cfg.Content = "学无先后达者为师";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NewLife.NC v{0}", AssemblyX.Entry.Version);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("目标：{0}", uri);
            Console.WriteLine("请求：{0:n0}", cfg.Times);
            Console.WriteLine("并发：{0:n0}", cfg.Thread);
            var buf = cfg.Content.GetBytes();
            Console.WriteLine("并发：[{0:n0}] {1}", buf.Length, cfg.Content);
            Console.ResetColor();
        }
    }
}