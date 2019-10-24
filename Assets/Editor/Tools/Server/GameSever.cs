using System.Collections;
using System.Collections.Generic;
using Assets.Foundation.Net;
using UnityEngine;

namespace Assets.Editor.Tools.Server
{
    public class GameSever
    {
        private NetServer _sever;
        private Dictionary<NetClient, Messages> _messages;

        public GameSever()
        {
            _sever = new NetServer();
            _messages = new Dictionary<NetClient, Messages>();
        }

        public void SetEndPoint(string address, int port)
        {
            _sever.SetServerAddress(address, port);
        }

        public void Start()
        {
            _sever.StartListen();
            _sever.Process();
        }
    }
}


