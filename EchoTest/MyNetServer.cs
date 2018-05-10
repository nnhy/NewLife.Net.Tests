using NewLife.Net;
using System;
using NewLife.Threading;

namespace EchoTest
{
    /// <inheritdoc />
    /// <summary>定义服务端，用于管理所有网络会话</summary>
    internal class MyNetServer : NetServer<MyNetSession>
    {
    }

    /// <inheritdoc />
    /// <summary>定义会话。每一个远程连接唯一对应一个网络会话，再次重复收发信息</summary>
    internal class MyNetSession : NetSession<MyNetServer>
    {
        private int _timerCount;
        private long _totCount;

        /// <inheritdoc />
        /// <summary>客户端连接</summary>
        public override void Start()
        {
            base.Start();

            //// 欢迎语
            //var str = String.Format("Welcome to visit {1}!  [{0}]\r\n", Remote, Environment.MachineName);
            //Send(str);

            var timer = new TimerX(ShowSpeed, null, 1000, 1000);
        }

        /// <inheritdoc />
        /// <summary>收到客户端数据</summary>
        /// <param name="e"></param>
        protected override void OnReceive(ReceivedEventArgs e)
        {
            //WriteLog("收到：{0}", e.Packet.ToStr());
            //// 把收到的数据发回去
            //Send(e.Packet);

            var packet = e.Packet;
            _totCount += packet.Count;

            var str = string.Empty;
            for (var i = 0; i < packet.Count; i++)
            {
                var data = packet.Data[i];
                str += data.ToString("X2") + " ";
                if (i >= 1024) break;
            }

            WriteLog("接收:{0}, 总接收:{1}, 数据:\n{2}", packet.Count, _totCount, str);
        }

        private void ShowSpeed(Object state)
        {
            WriteLog("总接收:{0}, 接收速度:{1}", _totCount, _totCount / ++_timerCount);
        }
    }
}