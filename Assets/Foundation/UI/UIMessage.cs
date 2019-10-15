using Assets.Foundation.Managers;
using Assets.Foundation.Protocol;
using Assets.Foundation.Tool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// ui消息控件
    /// </summary>
    public class UIMessage : MonoBehaviour
    {
        public delegate void DispatchMessage(MessageHeader msg);

        /// <summary>
        /// 消息类型
        /// </summary>
        private Dictionary<int, MessageHeader> _types = new Dictionary<int, MessageHeader>();
        /// <summary>
        /// 消息处理
        /// </summary>
        private Dictionary<int, DispatchMessage> _handlers = new Dictionary<int, DispatchMessage>();

        void OnDestroy()
        {
            this.RemoveAllMessageHands();
        }

        /// <summary>
        /// 添加消息处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public void AddMessageHand<T>(DispatchMessage callback) where T : MessageHeader, new()
        {
            if (callback == null)
            {
                return;
            }

            var t = new T();
            MessageManager.Instance.AddHand(t.ID, this.UnpackMessage);
            _handlers.Add(t.ID, callback);
            _types.Add(t.ID, t);
        }

        private void UnpackMessage(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }

            MessageHeader header = new MessageHeader();
            if (!header.Unpack(data))
            {
                return;
            }

            if (!_types.ContainsKey(header.ID))
            {
                return;
            }

            var t = _types[header.ID];
            if (!t.Unpack(data))
            {
                return;
            }

            if (_handlers.ContainsKey(t.ID))
            {
                _handlers[t.ID](t);
            }
        }

        /// <summary>
        /// 移除所有消息处理
        /// </summary>
        public void RemoveAllMessageHands()
        {
            if (!Singleton.isValid<MessageManager>())
            {
                return;
            }
            foreach (var item in _handlers)
            {
                MessageManager.Instance.RemoveHand(item.Key, this.UnpackMessage);
            }
        }
    }
}
