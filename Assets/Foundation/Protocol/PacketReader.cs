using Game.Foundation.Data;

namespace Game.Foundation.Protocol
{
    /// <summary>
    /// 包数据解析
    /// </summary>
    public class PacketReader
    {
        private DByteReader _method;
        public PacketReader(byte[] buff)
        {
            _method = new DByteReader(buff);
        }

        public object ReadObject(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (_method.Read(ref obj))
            {
                return obj;
            }

            return null;
        }

        public T ReadObject<T>()
        {
            T t = default(T);
            var obj = (object)t;
            if (!_method.Read(ref obj))
            {
                return t;
            }
            return (T)t;
        }
    }
}
