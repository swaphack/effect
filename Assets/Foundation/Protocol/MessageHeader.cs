using System;

namespace Assets.Foundation.Protocol
{
    /// <summary>
    /// 消息头
    /// </summary>
    public class MessageHeader
    {
        /// <summary>
        /// 消息长度
        /// </summary>
        public int Length;
        /// <summary>
        /// 消息编号
        /// </summary>
        public int ID;

        public MessageHeader()
        { 
        }

        public MessageHeader(int id)
        {
            this.ID = id;
        }

        /// <summary>
        /// 生成包
        /// </summary>
        /// <returns></returns>
        public byte[] Pack()
        { 
            var writer = new PacketWriter();
            writer.WriteObject(this);
            byte[] data = writer.ToBytes();
            int length = data.Length;
            var bitLength = BitConverter.GetBytes(length);
            for (int i = 0; i < bitLength.Length; i++)
            {
                data[i] = bitLength[i];
            }

            return data;
        }

        /// <summary>
        /// 解包
        /// </summary>
        /// <param name="data"></param>
        public bool Unpack(byte[] data)
        {
            if (data == null)
            {
                return false;
            }
            var bitLength = BitConverter.GetBytes(Length);
            if (data.Length < bitLength.Length)
            {
                return false;
            }
            var reader = new PacketReader(data);
            reader.ReadObject(this);
            if (data.Length < this.Length)
            {
                return false;
            }

            return true;
        }

        public T To<T>() where T : MessageHeader
        {
            return this as T;
        }
    }

    public static class ExtMessage
    {
        /// <summary>
        /// 将二进制转为message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T ToMessage<T>(this byte[] data) where T : MessageHeader, new()
        {
            T t = new T();
            t.Unpack(data);
            return t;
        }

        public static T ReadObject<T>(this PacketReader reader, T obj) where T : MessageHeader
        {
            return (T)reader.ReadObject(obj);
        }

        public static PacketWriter WriteObject<T>(this PacketWriter writer, T value) where T : MessageHeader
        {
            writer.WriteObject<T>(value);
            return writer;
        }
    }
}
