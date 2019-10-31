using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Assets.Foundation.Data
{
    public class DXmlWriter : IPairWriter
    {
        private XmlNode _root;

        public DXmlWriter(XmlNode root)
        {
            _root = root;
        }

        public bool Write(string name, bool value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, byte value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, sbyte value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, char value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, short value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, ushort value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, int value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, uint value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, long value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, ulong value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, float value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, double value)
        {
            return this.Write(name, Convert.ToString(value));
        }

        public bool Write(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            var element = _root.OwnerDocument.CreateElement(name);
            element.InnerText = value;
            _root.AppendChild(element);
            return true;
        }

        public bool Write(string name, IList value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (value == null)
            {
                return false;
            }

            var node = _root.OwnerDocument.CreateElement(name);
            for (int i = 0; i < value.Count; i++)
            {
                DXmlWriter writer = new DXmlWriter(node);
                if (!writer.Write("Item", value[i]))
                {
                    return false;
                }
            }
            _root.AppendChild(node);

            return true;
        }

        public bool Write(string name, IDictionary value)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            if (value == null)
            {
                return false;
            }

            var node = _root.OwnerDocument.CreateElement(name);
            var e = value.GetEnumerator();
            while (e.MoveNext())
            {
                DXmlWriter keyWriter = new DXmlWriter(node);
                if (!keyWriter.Write("Key", e.Key))
                {
                    return false;
                }

                DXmlWriter valueWriter = new DXmlWriter(node);
                if (!valueWriter.Write("Value", e.Value))
                {
                    return false;
                }
            }
            _root.AppendChild(node);

            return true;
        }

        private bool WriteObject(string name, Object obj)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

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

            var node = _root.OwnerDocument.CreateElement(name);
            DXmlWriter writer = new DXmlWriter(node);
            for (int i = 0; i < listField.Count; i++)
            {
                var field = listField[i];
                var value = field.GetValue(obj);
                if (value == null)
                {
                    value = DataHelper.Create(field.FieldType);
                }
                if (!writer.Write(field.Name, value))
                {
                    return false;
                }
            }
            _root.AppendChild(node);

            return true;
        }

        public bool Write(string name, Object obj)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
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
                        if (!Write(name, value)) return false;
                        break;
                    }
                case TypeCode.Char:
                    {
                        char value = (char)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.SByte:
                    {
                        sbyte value = (sbyte)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.Byte:
                    {
                        byte value = (byte)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.Int16:
                    {
                        short value = (short)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.UInt16:
                    {
                        ushort value = (ushort)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.Int32:
                    {
                        int value = (int)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.UInt32:
                    {
                        uint value = (uint)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.Int64:
                    {
                        long value = (long)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.UInt64:
                    {
                        ulong value = (ulong)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.Single:
                    {
                        float value = (float)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.Double:
                    {
                        double value = (double)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.String:
                    {
                        string value = (string)obj;
                        if (!Write(name, value)) return false;
                        break;
                    };
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IList)obj;
                            return this.Write(name, temp);
                        }
                        else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IDictionary)obj;
                            return this.Write(name, temp);
                        }
                    }
                    else
                    {
                        return this.WriteObject(name, obj);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
