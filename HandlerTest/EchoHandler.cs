using System;
using NewLife.Data;
using NewLife.Net.Handlers;

namespace HandlerTest
{
    class EchoHandler : Handler
    {
        public override Object Read(IHandlerContext context, Object message)
        {
            var session = context.Session;

            var pk = message as Packet;
            session.WriteLog("收到：{0}", pk.ToStr());

            // 把收到的数据发回去
            session.Send(pk);

            return null;
        }
    }
}