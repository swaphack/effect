using Assets.Foundation.Common;
using Assets.Foundation.Protocol;
using System;
using System.Collections.Generic;
using System.IO;

namespace Assets.Foundation.Net
{
    /// <summary>
    /// 接收到的数据
    /// </summary>
    public class NetMessage : IProcess
    {
        /// <summary>
        /// 接收流
        /// </summary>
        private MemoryStream _recvStream = new MemoryStream();

        /// <summary>
        /// 信息处理
        /// </summary>
        private Dictionary<int, List<NetMessageDelegate>> _msgHandlers = new Dictionary<int, List<NetMessageDelegate>>();

        private NetIDMessageDelegate _receiveMessage;

        public NetIDMessageDelegate OnReceiveMessage
        {
            get
            {
                return _receiveMessage;
            }
            set
            {
                _receiveMessage = value;
            }
        }

        /// <summary>
        /// 添加消息处理
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="hand"></param>
        public void AddMessageParse(int msgID, NetMessageDelegate hand)
        {
            if (hand == null)
            {
                return;
            }

            if (!_msgHandlers.ContainsKey(msgID))
            {
                _msgHandlers.Add(msgID, new List<NetMessageDelegate>());
            }

            _msgHandlers[msgID].Add(hand);
        }

        /// <summary>
        /// 移除消息处理
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="hand"></param>
        public void RemoveMessageParse(int msgID, NetMessageDelegate hand)
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

        /// <summary>
        /// 派发消息
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="data"></param>
        private void DispatchMessage(int msgID, byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }

            if (OnReceiveMessage != null)
            {
                OnReceiveMessage(msgID, data);
            }

            if (!_msgHandlers.ContainsKey(msgID))
            {
                return;
            }

            var lstHandler = _msgHandlers[msgID];
            var aryHandler = new NetMessageDelegate[lstHandler.Count];
            lstHandler.CopyTo(aryHandler);
            foreach (var item in aryHandler)
            {
                item(data);
            }
        }
        /// <summary>
        /// 派发消息
        /// </summary>
        /// <param name="msg"></param>
        public void DispatchMessage(MessageHeader msg)
        {
            if (msg == null)
            {
                return;
            }

            byte[] data = msg.Pack();

            DispatchMessage(msg.ID, data);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        public int AddBuffer(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return -1;
            }
            _recvStream.Write(data, 0, data.Length);
            return data.Length;
        }

        /// <summary>
        /// 处理数据，并解包
        /// </summary>
        public void Process()
        {
            if (_recvStream.Length == 0)
            {
                return;
            }
            byte[] array = _recvStream.ToArray();
            MessageHeader msg = new MessageHeader();
            if (!msg.Unpack(array))
            {
                return;
            }

            byte[] data = new byte[msg.Length];
            Array.Copy(array, data, msg.Length);
            DispatchMessage(msg.ID, data);
            _recvStream = new MemoryStream(array, msg.Length, array.Length - msg.Length);
        }
    }
}
