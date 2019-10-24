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
        
        private Messages _messages;

        public Client()
        {
            _client = new NetClient();
            _messages = new Messages();

            _client.BufferStream.OnBuffReceived = _messages.AddBuffer;
            _client.OnStatusChanged = OnNetStatusChanged;
        }

        private void OnNetStatusChanged(NetSocket s)
        {

        }

        void Update()
        {
            _client.Process();
            _messages.Process();
        }

        public void StartConnect()
        {
            _client.Connect();
        }

        public void SetEndPoint(string ip, int port)
        {
            _client.SetServerAddress(ip, port);
        }

        public void AddHand(int msgID, NetMessageDelegate hand)
        {
            _messages.AddHand(msgID, hand);
        }

        public void RemoveHand(int msgID, NetMessageDelegate hand)
        {
            _messages.RemoveHand(msgID, hand);
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }
    }
}
