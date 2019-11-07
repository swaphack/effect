using System;
using System.Collections.Generic;

namespace Game.Foundation.Net
{
    public class NetMultiMessage : IProcess
    {
        private Dictionary<NetClient, NetMessage> _messages = new Dictionary<NetClient, NetMessage>();
        /// <summary>
        /// 信息处理
        /// </summary>
        private Dictionary<int, List<NetClientMessageDelegate>> _msgHandlers = new Dictionary<int, List<NetClientMessageDelegate>>();

        private void OnReceiveMessage(NetClient client, int msgID, byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }

            if (!_msgHandlers.ContainsKey(msgID))
            {
                return;
            }

            var lstHandler = _msgHandlers[msgID];
            var aryHandler = new NetClientMessageDelegate[lstHandler.Count];
            lstHandler.CopyTo(aryHandler);
            foreach (var item in aryHandler)
            {
                item(client, data);
            }
        }

        /// <summary>
        /// 添加客户端消息监听
        /// </summary>
        /// <param name="client"></param>
        /// <param name="msg"></param>
        public void AddMessage(NetClient client, NetMessage msg)
        {
            if (client == null || msg == null)
            {
                return;
            }

            client.BufferStream.OnBuffReceived = msg.AddBuffer;
            msg.OnReceiveMessage = (int msgID, byte[] data) =>
            {
                this.OnReceiveMessage(client, msgID, data);
            };

            _messages[client] = msg;
        }
        /// <summary>
        /// 移除客户端消息监听
        /// </summary>
        /// <param name="client"></param>
        public void RemoveMessage(NetClient client)
        {
            if (client == null)
            {
                return;
            }

            if (_messages.ContainsKey(client))
            {
                _messages.Remove(client);
            }
        }

        /// <summary>
        /// 添加消息处理
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="hand"></param>
        public void AddMessageParse(int msgID, NetClientMessageDelegate hand)
        {
            if (hand == null)
            {
                return;
            }

            if (!_msgHandlers.ContainsKey(msgID))
            {
                _msgHandlers.Add(msgID, new List<NetClientMessageDelegate>());
            }

            _msgHandlers[msgID].Add(hand);
        }

        /// <summary>
        /// 移除消息处理
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="hand"></param>
        public void RemoveMessageParse(int msgID, NetClientMessageDelegate hand)
        {
            if (hand == null)
            {
                return;
            }

            if (!_msgHandlers.ContainsKey(msgID))
            {
                return;
            }

            if (_msgHandlers[msgID].Contains(hand))
            {
                _msgHandlers[msgID].Remove(hand);
            }
        }

        /// <summary>
        /// 移除所有处理
        /// </summary>
        public void Clear()
        {
            _msgHandlers.Clear();
        }

        public void Process()
        {
            List<NetMessage> messages = new List<NetMessage>();
            foreach (var item in _messages)
            {
                messages.Add(item.Value);
            }

            foreach (var item in messages)
            {
                item.Process();
            }
        }
    }
}
