using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Assets.Foundation.Net
{
    public class NetClientPool : IProcess
    {
        private HashSet<NetClient> _clients = new HashSet<NetClient>();
        private Socket _socket;
        private NetClientDelegate _clientStatusChanged;

        public NetClientDelegate OnClientStatusChanged
        {
            get
            {
                return _clientStatusChanged;
            }
            set
            {
                _clientStatusChanged = value;
            }
        }

        public NetClientPool(Socket s)
        {
            _socket = s;
        }

        public NetClientPool(NetServer server)
        {
            if (server == null)
            {
                return;
            }
            _socket = server.Socket;
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(NetClient client)
        {
            if (client == null)
            {
                return;
            }

            if (OnClientStatusChanged != null)
            {
                OnClientStatusChanged(client);
            }

            _clients.Add(client);
        }

        /// <summary>
        /// 移除客户端
        /// </summary>
        /// <param name="client"></param>
        public void RemoveClient(NetClient client)
        {
            if (client == null)
            {
                return;
            }

            if (OnClientStatusChanged != null)
            {
                OnClientStatusChanged(client);
            }

            _clients.Remove(client);
        }

        /// <summary>
        /// 清空客户端
        /// </summary>
        public void Clear()
        {
            _clients.Clear();
        }

        public void Process()
        {
            List<NetClient> clients = new List<NetClient>();
            foreach (var item in _clients)
            {
                clients.Add(item);
            }

            foreach (var item in clients)
            {
                item.Process();
                if (!item.Connected)
                {
                    this.RemoveClient(item);
                }
            }
        }
    }
}
