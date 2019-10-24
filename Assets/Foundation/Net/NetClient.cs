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
        private NetBufferStream _buffStream;

        public NetBufferStream BufferStream
        {
            get
            {
                return _buffStream;
            }
        }

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
            _buffStream = new NetBufferStream(this);
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
            _buffStream.Process();
        }
    }
}
