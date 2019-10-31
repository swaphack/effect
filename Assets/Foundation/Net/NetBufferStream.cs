using System.Net.Sockets;
using System;
using System.IO;

namespace Assets.Foundation.Net
{
    /// <summary>
    /// 数据缓冲
    /// </summary>
    public class NetBufferStream : IProcess
    {
        public const int RECV_BUFF_SIZE = 1024;
        public const int SEND_BUFF_SIZE = 1024;

        public const int RECV_STREAM_SIZE = 1024 * 10;
        public const int SEND_STREAM_SIZE = 1024 * 10;

        /// <summary>
        /// 临时接收流
        /// </summary>
        private byte[] _recvTempBuffer = new byte[RECV_BUFF_SIZE];
        /// <summary>
        /// 临时发送流
        /// </summary>
        private byte[] _sendTempBuffer = new byte[SEND_BUFF_SIZE];

        /// <summary>
        /// 接受流
        /// </summary>
        private MemoryStream _recvStream = new MemoryStream();
        /// <summary>
        /// 发送流
        /// </summary>
        private MemoryStream _sendStream = new MemoryStream();

        private Socket _socket;

        public NetBufferDelegate OnBuffReceived { get; set; }

        public NetBufferStream(NetClient client)
        {
            if (client == null)
            {
                return;
            }
            _socket = client.Socket;
        }

        public NetBufferStream(Socket socket)
        {
            _socket = socket;
        }

        /// <summary>
        /// 接受处理结果
        /// </summary>
        private IAsyncResult _recvRet;

        /// <summary>
        /// 添加发送数据
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void AddSendBuff(byte[] buff, int offset, int count)
        {
            if (buff == null)
            {
                return;
            }

            _sendStream.BeginWrite(buff, offset, count, (IAsyncResult ret)=> {
                if (ret.IsCompleted) _sendStream.EndWrite(ret);
            }, null);
        }

        /// <summary>
        /// 添加接受数据
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void AddRecvBuff(byte[] buff, int offset, int count)
        {
            if (buff == null)
            {
                return;
            }

            _recvStream.BeginWrite(buff, offset, count, (IAsyncResult ret) => {
                if (ret.IsCompleted) _recvStream.EndWrite(ret);
            }, null);
        }

        private void OnEndSend(IAsyncResult ret)
        {
            if (ret == null)
            {
                return;
            }

            int len = _socket.EndSend(ret);
            if (len <= 0)
            {
                return;
            }

            _sendStream.Position += len;
            if (_sendStream.Position > RECV_STREAM_SIZE)
            {
                var data = _sendStream.ToArray();
                _sendStream = new MemoryStream(data, (int)_sendStream.Position, (int)(_sendStream.Length - _sendStream.Position));
            }
        }

        private void OnEndRecv(IAsyncResult ret)
        {
            if (ret == null)
            {
                return;
            }

            int len = _socket.EndReceive(ret);
            if (len <= 0)
            {
                return;
            }

            _recvStream.BeginWrite(_recvTempBuffer, 0, len, (IAsyncResult r)=>
            {
                if (ret.IsCompleted)
                {
                    _recvStream.EndWrite(r);
                }
            }, this);
        }

        /// <summary>
        /// 处理发送
        /// </summary>
        private void ProcessSendBuffer()
        {
            if (_socket == null || !_socket.Connected)
            {
                return;
            }

            int len = _sendStream.Read(_sendTempBuffer, 0, SEND_BUFF_SIZE);
            _socket.BeginSend(_sendTempBuffer, 0, len, SocketFlags.None, (IAsyncResult ret)=>
            {
                if (ret.IsCompleted)
                {
                    OnEndSend(ret);
                }
            }, this);
        }
        /// <summary>
        /// 处理接收
        /// </summary>
        private void ProcessReceiveBuffer()
        {
            if (_socket == null || !_socket.Connected)
            {
                return;
            }

            _socket.BeginReceive(_recvTempBuffer, 0, RECV_BUFF_SIZE, SocketFlags.None, (IAsyncResult ret) =>
            {
                if (ret.IsCompleted)
                {
                    OnEndRecv(ret);                    
                }
            }, this);
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        private void ProcessReceiveHandler()
        {
            if (OnBuffReceived == null)
            {
                return;
            }
            var data = _recvStream.ToArray();
            if (data == null || data.Length == 0)
            {
                return;
            }
            var len = OnBuffReceived(data);
            if (len <= 0)
            {
                return;
            }

            _recvStream = new MemoryStream(data, len,  data.Length - len);
        }

        /// <summary>
        /// 外部调用处理
        /// </summary>
        public void Process()
        {
            if (_socket == null || !_socket.Connected)
            {
                return;
            }

            this.ProcessReceiveBuffer();

            this.ProcessSendBuffer();

            this.ProcessReceiveHandler();
        }
    }
}
