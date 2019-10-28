using System.Net.Sockets;
using System.IO;
using System;

namespace Assets.Foundation.Net
{
    /// <summary>
    /// 服务端
    /// </summary>
    public class NetServer : NetSocket, IProcess
    {
        public const int BLACK_LOG = 1000;
        /// <summary>
        /// 客户端池
        /// </summary>
        private NetClientPool _clientPool;

        public NetClientPool ClientPool
        {
            get
            {
                return _clientPool;
            }
        }

        public NetServer()
        {
            this.InitClientPool();
        }

        protected void InitClientPool()
        {
            _clientPool = new NetClientPool(this);
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
                _clientPool.AddClient(client);
            }, this);

            _clientPool.Process();
        }
    }
}
