using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Assets.Foundation.Data
{
    /// <summary>
    /// json写入操作
    /// </summary>
    public class DJsonWriter : IObjectWriter
    {
        private StringBuilder _builder;

        public DJsonWriter()
        {
            _builder = new StringBuilder();
        }

        public override string ToString()
        {
            string value = _builder.ToString();
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            value = value.Substring(0, value.Length - 1);
            return value;
        }

        public bool Write(bool value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(byte value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(sbyte value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(char value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(short value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(ushort value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(int value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(uint value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(long value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(ulong value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(float value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        public bool Write(double value)
        {
            return this.writeObject(Convert.ToString(value));
        }

        private bool writeObject(string value)
        {
            _builder.AppendFormat("\"{0}\",", value);
            return true;
        }

        /// <summary>
        /// 写入文本
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Write(string value)
        {
            _builder.AppendFormat("{0},", value);
            return true;
        }

        public bool Write(IList value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            DJsonWriter writer = new DJsonWriter();
            var eKey = value.GetEnumerator();
            while (eKey.MoveNext())
            {
                if (!writer.Write(eKey.Current))
                {
                    return false;
                }
            }
            sb.Append(writer.ToString());

            sb.Append("]");

            return this.Write(sb.ToString());
        }

        public bool Write(IDictionary value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            DJsonWriter writer = new DJsonWriter();
            var e = value.GetEnumerator();
            while (e.MoveNext())
            {
                var keyWriter = new DJsonWriter();
                keyWriter.Write(e.Key);

                var valueWriter = new DJsonWriter();
                valueWriter.Write(e.Value);

                string str = string.Format("{0}:{1}", keyWriter.ToString(), valueWriter.ToString());
                if (!writer.Write(str))
                {
                    return false;
                }
            }
            sb.Append(writer.ToString());

            sb.Append("}");

            return this.Write(sb.ToString());
        }

        private bool WriteObject(object obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[{");

            var writer = new DJsonWriter();

            var type = obj.GetType();
            var listField = DataHelper.GetClassFields(type);
            if (listField == null)
            {
                return false;
            }
            for (int i = 0; i < listField.Count; i++)
            {
                var field = listField[i];

                var keyWriter = new DJsonWriter();
                keyWriter.Write(field.Name);

                var value = field.GetValue(obj);
                var valueWriter = new DJsonWriter();
                valueWriter.Write(value);

                string str = string.Format("{0}:{1}", keyWriter.ToString(), valueWriter.ToString());
                if (!writer.Write(str))
                {
                    return false;
                }
            }
            sb.Append(writer.ToString());
            sb.Append("}]");
            return this.Write(sb.ToString());
        }

        public bool Write(object obj)
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
                        return this.WriteObject(obj);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
