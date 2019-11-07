using Game.Foundation.Data;

namespace Game.Foundation.Protocol
{
    /// <summary>
    /// 写入包数据
    /// </summary>
    public class PacketWriter
    {
        private DByteWriter _method;

        public PacketWriter()
        {
            _method = new DByteWriter();
        }

        public byte[] ToBytes()
        {
            return _method.ToBytes();
        }

        public void WriteObject(object value)
        {
            _method.Write(value);
        }

        public void WriteObject<T>(T value)
        {
            _method.Write(value);
        }
    }
}
