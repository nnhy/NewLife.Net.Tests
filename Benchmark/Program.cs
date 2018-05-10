﻿using NewLife.Data;
using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmark
{
    internal class Program
    {
        private static void Main(String[] args)
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

        private static void ShowHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("压力测试工具：nc [-n 1000] [-c 10] [-s] tcp://127.0.0.1:1234");
            Console.WriteLine("\t-n\t请求数");
            Console.WriteLine("\t-c\t并发数");
            Console.WriteLine("\t-s\t字符串内容");

            Console.ResetColor();
        }

        private static void Work(Config cfg)
        {
            var uri = new NetUri(cfg.Address);
            if (cfg.Content.IsNullOrEmpty()) cfg.Content = "学无先后达者为师";
            var pk = new Packet(cfg.Content.GetBytes());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("NewLife.NC v{0}", AssemblyX.Entry.Version);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("目标：{0}", uri);
            Console.WriteLine("请求：{0:n0}", cfg.Times);
            Console.WriteLine("并发：{0:n0}", cfg.Thread);
            Console.WriteLine("并发：[{0:n0}] {1}", pk.Count, cfg.Content);
            Console.ResetColor();
            Console.WriteLine();

            var sw = Stopwatch.StartNew();

            // 多线程
            var ts = new List<Task>();
            var total = 0;
            for (var i = 0; i < cfg.Thread; i++)
            {
                var tsk = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var client = uri.CreateRemote();
                        client.Open();
                        for (var k = 0; k < cfg.Times; k++)
                        {
                            client.Send(pk);
                            Interlocked.Increment(ref total);
                        }
                    }
                    catch { }
                }, TaskCreationOptions.LongRunning);
                ts.Add(tsk);
            }
            Task.WaitAll(ts.ToArray());

            sw.Stop();

            Console.WriteLine("完成：{0:n0}", total);

            var ms = sw.Elapsed.TotalMilliseconds;
            Console.WriteLine("速度：{0:n0}tps", total * 1000L / ms);
        }
    }
}