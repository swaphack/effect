using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Game.Foundation.Net
{
    public class NetSocket
    {
        /// <summary>
        /// 服务端地址
        /// </summary>
        private RemoteAddress _serverAddress;

        public Socket Socket { get; protected set; }

        /// <summary>
        /// 状态改变时处理
        /// </summary>
        public NetStatusDelegate OnStatusChanged { get; set; }

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool Connected
        {
            get
            {
                if (Socket == null)
                {
                    return false;
                }

                return Socket.Connected;
            }
        }

        public NetSocket()
        {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void StatusChanged()
        {
            OnStatusChanged?.Invoke(this);
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
                Socket.BeginConnect(_serverAddress.IP, _serverAddress.Port, (IAsyncResult ret) => {
                    if (ret.IsCompleted)
                    {
                        Socket.EndConnect(ret);
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
            Socket.BeginDisconnect(true, (IAsyncResult ret) => {
                if (ret.IsCompleted)
                {
                    Socket.EndDisconnect(ret);
                    this.StatusChanged();
                }
            }, this);
        }

        public void ShutDown()
        {
            Socket.Shutdown(SocketShutdown.Both);
        }

        public void Close()
        {
            Socket.Close();
        }

        public void Bind()
        {
            IPAddress ipAddress = IPAddress.Parse(_serverAddress.IP);

            IPEndPoint endPoint = new IPEndPoint(ipAddress, _serverAddress.Port);

            Socket.Bind(endPoint);
        }

        public void Listen(int backlog)
        {
            Socket.Listen(backlog);
        }
    }
}


