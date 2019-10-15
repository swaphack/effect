using System.Net.Sockets;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Collections;
using Assets.Foundation.Managers;

namespace Assets.Foundation.Managers
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class Client : Singleton<Client>
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        private struct ServerAddress
        {
            public string host;
            public int port;

            public ServerAddress(string host, int port)
            {
                this.host = host;
                this.port = port;
            }
        }
        /// <summary>
        /// 客户端连接
        /// </summary>
        private TcpClient _client = new TcpClient();
        
        /// <summary>
        /// 服务端地址
        /// </summary>
        private ServerAddress _address;

        public Client()
        {
        }

        /// <summary>
        /// 设置远程地址
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public void SetRemote(string address, int port)
        {
            _address = new ServerAddress(address, port);
        }

#region connect

        public delegate void ClientCallback(Client client);
        /// <summary>
        /// 连接成功回调
        /// </summary>
        private ClientCallback _connectCallback;
        /// <summary>
        /// 断开连接回调
        /// </summary>
        private ClientCallback _disconnectBallback;

        /// <summary>
        /// 连接回调
        /// </summary>
        public ClientCallback ConnectCallback
        {
            get
            {
                return _connectCallback;
            }
            set
            {
                _connectCallback = value;
            }
        }

        /// <summary>
        /// 断开连接回调
        /// </summary>
        public ClientCallback DisconnectCallback
        {
            get
            {
                return _disconnectBallback;
            }
            set
            {
                _disconnectBallback = value;
            }
        }

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool Connected 
        {
            get
            {
                if (_client == null)
                {
                    return false;
                }

                return _client.Connected;
            }
        } 

        private void OnConnect()
        {
            EnumeratorManager.Instance.Add(ConnectState());
        }

        private void OnDisconnect()
        {
            EnumeratorManager.Instance.Add(DisconnectState());
        }

        private IEnumerator ConnectState()
        {
            yield return null;

            if (_connectCallback != null)
            {
                _connectCallback(this);
            }
        }

        private IEnumerator DisconnectState()
        {
            yield return null;

            if (_disconnectBallback != null)
            {
                _disconnectBallback(this);
            }
        }

        /// <summary>
        /// 连接到服务端
        /// </summary>
        public void Connect()
        { 
            _client.BeginConnect(_address.host, _address.port, (IAsyncResult ret)=>{
                if (ret.IsCompleted)
                {
                    var target = ret.AsyncState as Client;
                    target.OnConnect();
                }
            }, this);
        }
        /// <summary>
        /// 重连
        /// </summary>
        public void Reconnect()
        {
            this.Disconnect();
            
            this.Connect();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            if (!Connected)
            {
                return;
            }
            _client.Client.Disconnect(true);
        }

        public void Close()
        {
            _client.Close();
        }

#endregion


        #region msg handler

        public delegate void RecvBuffCallback(byte[] buff);

        /// <summary>
        /// 接收数据处理
        /// </summary>
        private RecvBuffCallback _recvBuffCallback;

        public RecvBuffCallback RecvBuffHand
        {
            get
            {
                return _recvBuffCallback;
            }
            set
            {
                _recvBuffCallback = value;
            }
        }

        private void OnRecvBuff(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }
            EnumeratorManager.Instance.Add(RecvBuffState(data));
        }

        private IEnumerator RecvBuffState(byte[] data)
        {
            yield return null;
            if (_recvBuffCallback != null)
            {
                _recvBuffCallback(data);
            }
        }

        #endregion


        #region recv and send buff 

        public const int RecvBuffSize = 1024;
        public const int SendBuffSize = 1024;
        /// <summary>
        /// 临时接收流
        /// </summary>
        private byte[] _recvBuff = new byte[RecvBuffSize];
        
        /// <summary>
        /// 接受处理结果
        /// </summary>
        private IAsyncResult _recvRet;

        /// <summary>
        /// 临时发送流
        /// </summary>
        private byte[] _sendBuff = new byte[SendBuffSize];
        /// <summary>
        /// 发送流
        /// </summary>
        private MemoryStream _sendStream = new MemoryStream();
        /// <summary>
        /// 发送处理结果
        /// </summary>
        private IAsyncResult _sendRet;

        private void OnRecvBuff(int count)
        {
            byte[] buff = new byte[count];
            Array.Copy(_recvBuff, buff, count);
            OnRecvBuff(buff);
        }

        private void OnSendBuff(int count)
        {
            byte[] data = _sendStream.ToArray();
            _sendStream = new MemoryStream(data, count, data.Length - count);
        }

        /// <summary>
        /// 开始接收数据
        /// </summary>
        public void StartRecv()
        {
            if (!_client.Connected)
            {
                return;
            }

            int size = RecvBuffSize;
            _recvRet = _client.GetStream().BeginRead(_recvBuff, 0, size, (IAsyncResult ret) =>
            {
                if (ret.IsCompleted)
                {
                    int nCount = _client.GetStream().EndRead(_recvRet);
                    this.OnRecvBuff(nCount);
                    this.StartRecv();
                }
            }, this);
        }

        public void EndRecv()
        {
            if (_recvRet != null)
            {
                _client.GetStream().EndRead(_recvRet);
                _recvRet = null;
            }
        }

        /// <summary>
        ///  开始发送
        /// </summary>
        private void progressSend()
        {
            if (!_client.Connected)
            {
                return;
            }

            if (_sendStream.Length == 0)
            {
                return;
            }

            if (_sendRet != null)
            {
                return;
            }

            long pos = _sendStream.Position;
            _sendStream.Position = 0;
            int size = _sendStream.Read(_sendBuff, 0, SendBuffSize);
            _sendStream.Position = pos;

            _sendRet = _client.GetStream().BeginWrite(_recvBuff, 0, size, (IAsyncResult ret) =>
            {
                if (ret.IsCompleted)
                {
                    _client.GetStream().EndWrite(_sendRet);
                    this.OnSendBuff(size);
                    _sendRet = null;
                }
            }, this);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data"></param>
        public void Send(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }

            _sendStream.BeginWrite(data, 0, data.Length, (IAsyncResult ret) => { 

            }, this);
        }
#endregion

        private readonly static object _sendLock = new object();
        void Update()
        {
            lock (_sendLock)
            {
                if (!Connected)
                {
                    return;
                }
                this.progressSend();
            }
        }
    }
}
