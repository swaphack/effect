using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Assets.Foundation.Data
{
    /// <summary>
    /// 将xml转为对象
    /// </summary>
    public class DXmlReader : IPairReader
    {
        private XmlNode _node;

        public DXmlReader(XmlNode node)
        {
            _node = node;
        }

        public bool Read(string name, ref bool value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToBoolean(val);
            return true;
        }

        public bool Read(string name, ref byte value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToByte(val);
            return true;
        }

        public bool Read(string name, ref sbyte value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToSByte(val);
            return true;
        }

        public bool Read(string name, ref char value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToChar(val);
            return true;
        }

        public bool Read(string name, ref short value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToInt16(val);
            return true;
        }

        public bool Read(string name, ref ushort value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToUInt16(val);
            return true;
        }

        public bool Read(string name, ref int value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToInt32(val);
            return true;
        }

        public bool Read(string name, ref uint value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToUInt32(val);
            return true;
        }

        public bool Read(string name, ref long value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToInt64(val);
            return true;
        }

        public bool Read(string name, ref ulong value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToUInt64(val);
            return true;
        }

        public bool Read(string name, ref float value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToSingle(val);
            return true;
        }

        public bool Read(string name, ref double value)
        {
            string val = "";
            if (!Read(name, ref val)) return false;
            value = Convert.ToDouble(val);
            return true;
        }

        public bool Read(string name, ref string value)
        {
            if (_node == null)
            {
                return false;
            }

            if (_node.Name != name)
            {
                return false;
            }

            value = _node.InnerText;

            return true;
        }

        /// <summary>
        /// List
        /// 
        /// Sample:
        /// <Name>
        ///     <Item></Item>
        ///     <Item></Item>
        ///     <Item></Item>
        ///     <Item></Item>
        /// </Name>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Read(string name, ref IList value)
        {
            if (value == null)
            {
                return false;
            }

            if (_node == null)
            {
                return false;
            }

            if (_node.Name != name)
            {
                return false;
            }

            if (!_node.HasChildNodes)
            {
                return false;
            }

            var type = value.GetType();
            var paramTypes = type.GetGenericArguments();

            var nodeList = _node.ChildNodes;
            for (int i = 0; i < nodeList.Count; i++)
            {
                var first = DataHelper.Create(paramTypes[0]);
                DXmlReader reader = new DXmlReader(nodeList[i]);
                string strName = "Item";
                if (!reader.Read(strName, ref first))
                {
                    return false;
                }
                value.Add(first);
            }

            return true;
        }

        /// <summary>
        /// Dictionary
        /// 
        /// <Name>
        ///     <Key></Key>
        ///     <Value></Value>
        /// </Name>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Read(string name, ref IDictionary value)
        {
            if (value == null)
            {
                return false;
            }

            if (_node == null)
            {
                return false;
            }

            if (_node.Name != name)
            {
                return false;
            }

            if (!_node.HasChildNodes)
            {
                return false;
            }

            var type = value.GetType();
            var paramTypes = type.GetGenericArguments();

            var nodeList = _node.ChildNodes;
            for (int i = 0; i < nodeList.Count; i+=2)
            {
                var first = DataHelper.Create(paramTypes[0]);
                DXmlReader keyReader = new DXmlReader(nodeList[i]);
                string strKeyName = "Key";
                if (!keyReader.Read(strKeyName, ref first))
                {
                    return false;
                }

                var second = DataHelper.Create(paramTypes[1]);
                DXmlReader valueReader = new DXmlReader(nodeList[i + 1]);
                string strValueName = "Value";
                if (!valueReader.Read(strValueName, ref second))
                {
                    return false;
                }

                value.Add(first, second);
            }

            return true;
        }

        private bool ReadObject(string name, ref Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (_node == null)
            {
                return false;
            }

            if (_node.Name != name)
            {
                return false;
            }

            if (!_node.HasChildNodes)
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
                var fieldName = field.Name;

                if (_node[fieldName] == null)
                {
                    continue;
                }

                DXmlReader reader = new DXmlReader(_node[fieldName]);

                Type memberType = field.FieldType;
                var item = DataHelper.Create(memberType);
                if (!reader.Read(fieldName, ref item))
                {
                    return false;
                }
                field.SetValue(obj, item);
            }
            return true;
        }

        public bool Read(string name, ref Object obj)
        {
            Type type = obj.GetType();
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean:
                    {
                        bool value = default(bool);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    }
                case TypeCode.Char:
                    {
                        var value = default(char);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.SByte:
                    {
                        var value = default(sbyte);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Byte:
                    {
                        var value = default(byte);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Int16:
                    {
                        var value = default(short);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.UInt16:
                    {
                        var value = default(ushort);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Int32:
                    {
                        var value = default(int);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.UInt32:
                    {
                        var value = default(uint);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Int64:
                    {
                        var value = default(long);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.UInt64:
                    {
                        var value = default(ulong);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Single:
                    {
                        var value = default(float);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Double:
                    {
                        var value = default(double);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.String:
                    {
                        var value = default(string);
                        if (!Read(name, ref value)) return false;
                        obj = value;
                        break;
                    };
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IList)obj;
                            return this.Read(name, ref temp);
                        }
                        else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            var temp = (IDictionary)obj;
                            return this.Read(name, ref temp);
                        }
                    }
                    else
                    {
                        return this.ReadObject(name, ref obj);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
