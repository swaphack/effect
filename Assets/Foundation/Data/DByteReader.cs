using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Game.Foundation.Data
{
    /// <summary>
    /// 将字节转为对象
    /// </summary>
    public class DByteReader : IObjectReader
    {
        public MemoryStream Stream { get; protected set; }

        public DByteReader(byte[] buff)
        {
            if (buff == null || buff.Length == 0)
            {
                return;
            }
            Stream = new MemoryStream(buff);
        }


        public bool Read(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return false;
            }
            if (Stream.Position + data.Length > Stream.Length)
            {
                return false;
            }
            Stream.Read(data, 0, data.Length);
            return true;
        }

        public bool Read(ref bool value)
        {
            var temp = new byte[1];
            if (!this.Read(temp))
            {
                return false;
            }
            value = temp[0] == 1;
            return true;
        }

        public bool Read(ref byte value)
        {
            var temp = new byte[1];
            if (!this.Read(temp))
            {
                return false;
            }
            value = temp[0];
            return true;
        }

        public bool Read(ref sbyte value)
        {
            var temp = new byte[1];
            if (!this.Read(temp))
            {
                return false;
            }
            value = (sbyte)temp[0];
            return true;
        }

        public bool Read(ref char value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToChar(bytes, 0);
            return true;
        }

        public bool Read(ref short value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToInt16(bytes, 0);
            return true;
        }

        public bool Read(ref ushort value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToUInt16(bytes, 0);
            return true;
        }

        public bool Read(ref int value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToInt32(bytes, 0);
            return true;
        }

        public bool Read(ref uint value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToUInt32(bytes, 0);
            return true;
        }

        public bool Read(ref long value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToInt64(bytes, 0);
            return true;
        }

        public bool Read(ref ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToUInt64(bytes, 0);
            return true;
        }

        public bool Read(ref float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToSingle(bytes, 0);
            return true;
        }

        public bool Read(ref double value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (!this.Read(bytes))
            {
                return false;
            }
            value = BitConverter.ToDouble(bytes, 0);
            return true;
        }

        public bool Read(ref string value)
        {
            int length = 0;
            if (!Read(ref length))
            {
                return false;
            }
            byte[] buff = new byte[length];
            if (!this.Read(buff))
            {
                return false;
            }
            value =  Encoding.UTF8.GetString(buff);
            return true;
        }

        public bool Read(ref IList value)
        {
            if (value == null)
            {
                return false;
            }
            var type = value.GetType();
            var paramTypes = type.GetGenericArguments();
            int nCount = 0;
            if (!this.Read(ref nCount))
            {
                return false;
            }
            for (int i = 0; i < nCount; i++)
            {
                var first = DataHelper.Create(paramTypes[0]);
                if (!this.Read(ref first))
                {
                    return false;
                }
                value.Add(first);
            }
            return true;
        }

        public bool Read(ref IDictionary value)
        {
            if (value == null)
            {
                return false;
            }
            var type = value.GetType();
            var paramTypes = type.GetGenericArguments();
            int nCount = 0;
            if (!this.Read(ref nCount))
            {
                return false;
            }
            for (int i = 0; i < nCount; i++)
            {
                var first = DataHelper.Create(paramTypes[0]);
                this.Read(ref first);

                var second = DataHelper.Create(paramTypes[1]);
                this.Read(ref second);

                value.Add(first, second);
            }
            return true;
        }

        private bool ReadObject(ref Object obj)
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
                Type memberType = field.FieldType;
                var item = DataHelper.Create(memberType);
                if (!this.Read(ref item))
                {
                    return false;
                }
                field.SetValue(obj, item);
            }

            return true;
        }

        public bool Read(ref Object obj)
        {
            Type type = obj.GetType();
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean: 
                    {
                        bool value = default(bool);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    }
                case TypeCode.Char:
                    {
                        var value = default(char);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.SByte:
                    {
                        var value = default(sbyte);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Byte:
                    {
                        var value = default(byte);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Int16:
                    {
                        var value = default(short);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.UInt16:
                    {
                        var value = default(ushort);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Int32:
                    {
                        var value = default(int);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.UInt32:
                    {
                        var value = default(uint);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Int64:
                    {
                        var value = default(long);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.UInt64:
                    {
                        var value = default(ulong);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Single:
                    {
                        var value = default(float);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Double:
                    {
                        var value = default(double);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.String:
                    {
                        var value = default(string);
                        if (!Read(ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IList)obj;
                            return this.Read(ref temp);
                        }
                        if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IDictionary)obj;
                            return this.Read(ref temp);
                        }
                    }
                    else
                    {
                        return ReadObject(ref obj);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
