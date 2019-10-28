using UnityEngine;
using System.Collections;
using Assets.Foundation.Net;
using Assets.Foundation.Common;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class Client : SingletonBehaviour<Client>
    {
        private NetClient _client;
        
        private NetMessage _message;

        public Client()
        {
            _client = new NetClient();
            _message = new NetMessage();

            _client.BufferStream.OnBuffReceived = _message.AddBuffer;
            _client.OnStatusChanged = OnNetStatusChanged;
        }

        private void OnNetStatusChanged(NetSocket s)
        {

        }

        void Update()
        {
            _client.Process();
            _message.Process();
        }

        public void StartConnect()
        {
            _client.Connect();
        }

        public void SetEndPoint(string ip, int port)
        {
            _client.SetServerAddress(ip, port);
        }

        public void AddMessageParse(int msgID, NetMessageDelegate hand)
        {
            _message.AddMessageParse(msgID, hand);
        }

        public void RemoveMessageParse(int msgID, NetMessageDelegate hand)
        {
            _message.RemoveMessageParse(msgID, hand);
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }
    }
}
