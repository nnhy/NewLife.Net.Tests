using System;
using NewLife.Net.Handlers;

namespace EchoTest
{
    class EchoHandler : Handler
    {
        public override Boolean Open(IHandlerContext context)
        {
            var session = context.Session;

            // 欢迎语
            var str = String.Format("Welcome to visit {1}!  [{0}]\r\n", session, Environment.MachineName);
            session.Send(str.GetBytes());

            return true;
        }

        public override Boolean Close(IHandlerContext context, String reason)
        {
            context.Session.WriteLog("再见！");

            return base.Close(context, reason);
        }

        public override Object Read(IHandlerContext context, Object message)
        {
            var session = context.Session;

            var pk = context.Data.Packet;
            session.WriteLog("收到：{0}", pk.ToStr());

            // 把收到的数据发回去
            session.Send(pk);

            return null;
        }
    }
}