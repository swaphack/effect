using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assets.Foundation.Data
{
    /// <summary>
    /// 将对象转为字节
    /// </summary>
    public class DByteWriter : IObjectWriter
    {
        private MemoryStream _stream;

        public MemoryStream Stream
        {
            get
            {
                return _stream;
            }
            protected set
            {
                _stream = value;
            }
        }

        public const int DEFAULT_LENGTH = 1024;

        public DByteWriter(Object obj)
        {
            Stream = new MemoryStream(DEFAULT_LENGTH);
            this.Write(obj);
        }

        public DByteWriter()
        {
            Stream = new MemoryStream(DEFAULT_LENGTH);
        }

        /// <summary>
        /// 转换成字节
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            if (_stream == null)
            {
                return null;
            }
            var value = new byte[_stream.Length];
            var bytes = _stream.ToArray();
            Array.Copy(bytes, value, _stream.Length);
            return value;
        }

        public bool Write(byte[] value)
        {
            if (value == null || value.Length == 0)
            {
                return false;
            }

            int capacity = Stream.Capacity;
            if (Stream.Position + value.Length > capacity)
            { // 扩容
                while (Stream.Position + value.Length > capacity)
                {
                    capacity *= 2;
                }
                var bytes = Stream.ToArray();
                var stream = new MemoryStream(capacity);
                stream.Write(bytes, 0, bytes.Length);
                Stream = stream;
            }
            
            Stream.Write(value, 0, value.Length);
            return true;
        }

        public bool Write(bool value)
        {
            return this.Write(value ? (byte)1 : (byte)0);
        }

        public bool Write(byte value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(sbyte value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(char value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(short value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(ushort value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(int value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(uint value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(long value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(ulong value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(float value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(double value)
        {
            return this.Write(BitConverter.GetBytes(value));
        }

        public bool Write(string value)
        {
            var temp = Encoding.UTF8.GetBytes(value);
            if (!this.Write(temp.Length))
            {
                return false;
            }

            return this.Write(temp);

        }

        public bool Write(IList value)
        {
            if (value == null)
            {
                return false;
            }
            var lst = (IList)value;
            var eKey = lst.GetEnumerator();

            if (!this.Write(lst.Count))
            {
                return false;
            }
            while (eKey.MoveNext())
            {
                if (!this.Write(eKey.Current))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Write(IDictionary value)
        {
            if (value == null)
            {
                return false;
            }
            var map = (IDictionary)value;
            var e = map.GetEnumerator();
            var keys = map.Keys;
            if (!this.Write(keys.Count))
            {
                return false;
            }
            while (e.MoveNext())
            {
                if (!this.Write(e.Key))
                {
                    return false;
                }
                if (!this.Write(e.Value))
                {
                    return false;
                }
            }
            return true;
        }

        private bool WriteObject(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var type = obj.GetType();
            var listField = DataHelper.GetClassFields(type);
            if (listField == null)
            {
                return false;
            }

            for (int i = 0; i < listField.Count; i++)
            {
                var field = listField[i];
                var n = field.GetValue(obj);
                if (!this.Write(n))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Write(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Type type = obj.GetType();
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean:
                    {
                        bool value = (bool)obj;
                        if (!Write(value)) return false;
                        break;
                    }
                case TypeCode.Char:
                    {
                        char value = (char)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.SByte:
                    {
                        sbyte value = (sbyte)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.Byte:
                    {
                        byte value = (byte)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.Int16:
                    {
                        short value = (short)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.UInt16:
                    {
                        ushort value = (ushort)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.Int32:
                    {
                        int value = (int)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.UInt32:
                    {
                        uint value = (uint)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.Int64:
                    {
                        long value = (long)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.UInt64:
                    {
                        ulong value = (ulong)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.Single:
                    {
                        float value = (float)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.Double:
                    {
                        double value = (double)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.String:
                    {
                        string value = (string)obj;
                        if (!Write(value)) return false;
                        break;
                    };
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IList)obj;
                            return this.Write(temp);
                        }
                        else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IDictionary)obj;
                            return this.Write(temp);
                        }
                    }
                    else
                    {
                        return WriteObject(obj);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
