using System;
using NewLife.Data;
using NewLife.Log;
using NewLife.Net.Handlers;

namespace HandlerTest
{
    class EchoHandler : Handler
    {
        /// <summary>性能计数器</summary>
        public ICounter Counter { get; set; }

        public override Object Read(IHandlerContext context, Object message)
        {
            var session = context.Session;

            // 性能计数
            Counter?.Increment(1, 0);

            var pk = message as Packet;
#if DEBUG
            session.WriteLog("收到：{0}", pk.ToStr());
#endif

            // 把收到的数据发回去
            session.SendMessage(pk);

            return null;
        }
    }
}