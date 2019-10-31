using System.Net.Sockets;
using System;
using System.IO;

namespace Assets.Foundation.Net
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class NetClient : NetSocket, IProcess
    {
        public NetBufferStream BufferStream { get; private set; }

        public NetClient()
        {
            this.InitBuffStream();
        }

        public void SetSocket(Socket s)
        {
            Socket = s;            
        }

        private void InitBuffStream()
        {
            BufferStream = new NetBufferStream(this);
        }

        /// <summary>
        /// 重连
        /// </summary>
        public void Reconnect()
        {
            this.Disconnect();
            
            this.Connect();
        }

        public void Process()
        {
            BufferStream.Process();
        }
    }
}
