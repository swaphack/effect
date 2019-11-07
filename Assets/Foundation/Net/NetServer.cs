using System.Net.Sockets;
using System.IO;
using System;

namespace Game.Foundation.Net
{
    /// <summary>
    /// 服务端
    /// </summary>
    public class NetServer : NetSocket, IProcess
    {
        public const int BLACK_LOG = 1000;

        public NetClientPool ClientPool { get; private set; }

        public NetServer()
        {
            this.InitClientPool();
        }

        protected void InitClientPool()
        {
            ClientPool = new NetClientPool(this);
        }

        public void StartListen()
        {
            this.Bind();
            this.Listen(BLACK_LOG);
        }

        public void Process()
        {
            Socket.BeginAccept((IAsyncResult ar) => {
                Socket s = Socket.EndAccept(ar);
                var client = new NetClient();
                client.SetSocket(s);
                ClientPool.AddClient(client);
            }, this);

            ClientPool.Process();
        }
    }
}
