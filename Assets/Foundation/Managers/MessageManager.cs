using Assets.Foundation.Managers;
using Assets.Foundation.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Foundation.Managers
{
    /// <summary>
    /// 接收到的数据
    /// </summary>
    public class MessageManager : Singleton<MessageManager>
    {
        public delegate void BuffCallback(byte[] data);
        /// <summary>
        /// 接收流
        /// </summary>
        private MemoryStream _recvStream = new MemoryStream();

        /// <summary>
        /// 信息处理
        /// </summary>
        private Dictionary<int, List<BuffCallback>> _msgHandlers = new Dictionary<int, List<BuffCallback>>();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        public void AddBuff(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }
            _recvStream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// 处理数据，并解包
        /// </summary>
        private void progressUnpack()
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

        /// <summary>
        /// 添加消息处理
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="hand"></param>
        public void AddHand(int msgID, BuffCallback hand)
        {
            if (hand == null)
            {
                return;
            }

            if (!_msgHandlers.ContainsKey(msgID))
            {
                _msgHandlers.Add(msgID, new List<BuffCallback>());
            }

            _msgHandlers[msgID].Add(hand);
        }

        /// <summary>
        /// 移除消息处理
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="hand"></param>
        public void RemoveHand(int msgID, BuffCallback hand)
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
        /// 派发事件
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="data"></param>
        private void DispatchMessage(int msgID, byte[] data)
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
            var aryHandler = new BuffCallback[lstHandler.Count];
            lstHandler.CopyTo(aryHandler);
            foreach (var item in aryHandler)
            {
                item(data);
            }
        }

        public void DispatchMessage(MessageHeader msg)
        {
            if (msg == null)
            {
                return;
            }

            byte[] data = msg.Pack();

            DispatchMessage(msg.ID, data);
        }


        private readonly static object _recvLock = new object();

        void Update()
        {
            lock (_recvLock)
            {
                this.progressUnpack();
            }
        }
    }
}
