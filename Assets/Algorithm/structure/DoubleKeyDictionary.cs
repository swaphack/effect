using System;
using System.Collections.Generic;

namespace Game.Algorithm.Structure
{
    /// <summary>
    /// 双键字典
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public class DoubleKeyDictionary<Key, Value>
    {
        protected Dictionary<Key, Value> KeyValuePairs { get; } = new Dictionary<Key, Value>();
        protected Dictionary<Value, Key> ValueKeyPairs { get; } = new Dictionary<Value, Key>();

        /// <summary>
        /// 主键
        /// </summary>
        public Dictionary<Key, Value>.KeyCollection Keys => KeyValuePairs.Keys;
        /// <summary>
        /// 值
        /// </summary>
        public Dictionary<Key, Value>.ValueCollection Values => KeyValuePairs.Values;

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Key key, Value value)
        {
            KeyValuePairs[key] = value;
            ValueKeyPairs[value] = key;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(Key key)
        {
            if (!KeyValuePairs.ContainsKey(key))
            {
                return;
            }

            var value = KeyValuePairs[key];
            KeyValuePairs.Remove(key);
            ValueKeyPairs.Remove(value);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value GetValue(Key key)
        {
            if (ContainsKey(key))
            {
                return KeyValuePairs[key];
            }

            return default(Value);
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Key GetKey(Value value)
        {
            if (ContainsValue(value))
            {
                return ValueKeyPairs[value];
            }

            return default(Key);
        }

        /// <summary>
        /// 是否包含主键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(Key key)
        {
            return KeyValuePairs.ContainsKey(key);
        }

        /// <summary>
        /// 是否包含值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue(Value value)
        {
            return ValueKeyPairs.ContainsKey(value);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            KeyValuePairs.Clear();
            ValueKeyPairs.Clear();
        }
    }
}
