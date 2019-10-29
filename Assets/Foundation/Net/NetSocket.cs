using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Assets.Foundation.Net
{
    public class NetSocket
    {
        /// <summary>
        /// 客户端连接
        /// </summary>
        private Socket _socket;
        /// <summary>
        /// 服务端地址
        /// </summary>
        private RemoteAddress _serverAddress;
        /// <summary>
        /// 状态改变时处理
        /// </summary>
        private NetStatusDelegate _connectStatusChanged;

        public Socket Socket
        {
            get
            {
                return _socket;
            }
            protected set
            {
                _socket = value;
            }
        }

        /// <summary>
        /// 状态改变时处理
        /// </summary>
        public NetStatusDelegate OnStatusChanged
        {
            get
            {
                return _connectStatusChanged;
            }
            set
            {
                _connectStatusChanged = value;
            }
        }

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool Connected
        {
            get
            {
                if (_socket == null)
                {
                    return false;
                }

                return _socket.Connected;
            }
        }

        public NetSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void StatusChanged()
        {
            if (_connectStatusChanged != null)
            {
                _connectStatusChanged(this);
            }
        }

        /// <summary>
        /// 服务端地址
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public void SetServerAddress(string address, int port)
        {
            _serverAddress = new RemoteAddress(address, port);
        }

        /// <summary>
        /// 连接到服务端
        /// </summary>
        public void Connect()
        {
            if (!_serverAddress.IsValid)
            {
                this.StatusChanged();
                return;
            }
            try
            {
                _socket.BeginConnect(_serverAddress.IP, _serverAddress.Port, (IAsyncResult ret) => {
                    if (ret.IsCompleted)
                    {
                        _socket.EndConnect(ret);
                        this.StatusChanged();
                    }
                }, this);
            }
            catch (SocketException e)
            {
                Debug.Log(e.Message);
                this.StatusChanged();
            }
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
            _socket.BeginDisconnect(true, (IAsyncResult ret) => {
                if (ret.IsCompleted)
                {
                    _socket.EndDisconnect(ret);
                    this.StatusChanged();
                }
            }, this);
        }

        public void ShutDown()
        {
            _socket.Shutdown(SocketShutdown.Both);
        }

        public void Close()
        {
            _socket.Close();
        }

        public void Bind()
        {
            IPAddress ipAddress = IPAddress.Parse(_serverAddress.IP);

            IPEndPoint endPoint = new IPEndPoint(ipAddress, _serverAddress.Port);

            _socket.Bind(endPoint);
        }

        public void Listen(int backlog)
        {
            _socket.Listen(backlog);
        }
    }
}


