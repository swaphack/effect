using System.Threading;
using Assets.Foundation.Net;

namespace Assets.Editor.Tools.GameDeploy
{
    public class GameServer
    {
        /// <summary>
        /// 服务器
        /// </summary>
        private NetServer _sever;
        /// <summary>
        /// 客户端消息
        /// </summary>
        private NetMultiMessage _messages;

        public GameServer()
        {
            _sever = new NetServer();
            _messages = new NetMultiMessage();
        }
        /// <summary>
        /// 设置终端地址
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public void SetEndPoint(string address, int port)
        {
            _sever.SetServerAddress(address, port);
        }

        /// <summary>
        /// 客户端状态改变
        /// </summary>
        /// <param name="client"></param>
        private void OnClientStatusChanged(NetClient client)
        {
            if (client.Connected)
            {
                _messages.AddMessage(client, new NetMessage());
            }
            else
            {
                _messages.RemoveMessage(client);
            }
        }

        /// <summary>
        /// 开始接收
        /// </summary>
        public void StartAccept()
        {
            _sever.StartListen();
            _sever.ClientPool.OnClientStatusChanged = this.OnClientStatusChanged;

            Thread thread = new Thread(() => {
                while (_sever.Connected)
                {
                    Process();
                    Thread.Sleep(10);
                }
            });
            thread.Start();
        }

        public void Stop()
        {
            _sever.Close();
        }

        protected void Process()
        {
            _sever.Process();
            _messages.Process();
        }
    }
}


