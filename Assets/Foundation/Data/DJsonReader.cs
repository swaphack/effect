using Game.Foundation.TextFormat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Game.Foundation.Data
{
    public class DJsonReader : IObjectReader
    {
        private object _tree;
        private int _index;

        public DJsonReader(string text)
        {
            var ts = new JsonFormat(text);
            if (ts.Parse())
            {
                _tree = ts.Tree;
                _index = 0;
            }
        }

        public DJsonReader()
        {
            _tree = new List<object>();
            _index = 0;
        }

        public DJsonReader Append(object obj)
        {
            if (obj == null)
            {
                return this;
            }

            var list = _tree as IList;
            if (list == null)
            {
                return this;
            }
            list.Add(obj);

            return this;
        }

        private object Current
        {
            get
            {
                if (_tree == null)
                {
                    return null;
                }


                if (!(_tree is IList))
                {
                    return null;
                }

                var list = _tree as IList;
                if (_index >= list.Count)
                {
                    return null;
                }

                var obj = list[_index];
                _index++;
                return obj;
            }
        }

        private string GetText()
        {
            var obj = Current;
            if (obj == null)
            {
                return null;
            }
            return obj as string;
        }

        public bool Read(ref bool value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToBoolean(text);
            return true;
        }

        public bool Read(ref byte value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToByte(text);
            return true;
        }

        public bool Read(ref sbyte value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToSByte(text);
            return true;
        }

        public bool Read(ref char value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToChar(text);
            return true;
        }

        public bool Read(ref short value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToInt16(text);
            return true;
        }

        public bool Read(ref ushort value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToUInt16(text);
            return true;
        }

        public bool Read(ref int value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToInt32(text);
            return true;
        }

        public bool Read(ref uint value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToUInt32(text);
            return true;
        }

        public bool Read(ref long value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToInt64(text);
            return true;
        }

        public bool Read(ref ulong value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToUInt64(text);
            return true;
        }

        public bool Read(ref float value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToSingle(text);
            return true;
        }

        public bool Read(ref double value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToDouble(text);
            return true;
        }

        public bool Read(ref string value)
        {
            string text = GetText();
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            value = Convert.ToString(text);
            return true;
        }

        public bool Read(ref IList value)
        {
            if (value == null)
            {
                return false;
            }

            var obj = Current;
            if (obj == null)
            {
                return false;
            }

            var list = obj as IList;
            if (list == null)
            {
                return false;
            }

            var type = value.GetType();
            var paramTypes = type.GetGenericArguments();
            for (var i = 0; i < list.Count; i++)
            {
                var data = DataHelper.Create(paramTypes[0]);
                var reader = new DJsonReader().Append(list[i]);
                if (!reader.Read(ref data))
                {
                    return false;
                }
                value.Add(data);
            }
            return true;
        }

        public bool Read(ref IDictionary value)
        {
            if (value == null)
            {
                return false;
            }

            var obj = Current;
            if (obj == null)
            {
                return false;
            }

            var map = obj as IDictionary;
            if (map == null)
            {
                return false;
            }

            var type = value.GetType();
            var paramTypes = type.GetGenericArguments();
            var keys = map.Keys;
            foreach (var item in keys)
            {
                var key = DataHelper.Create(paramTypes[0]);
                var keyReader = new DJsonReader().Append(item);
                if (!keyReader.Read(ref key))
                {
                    return false;
                }

                var val = DataHelper.Create(paramTypes[1]);
                var valReader = new DJsonReader().Append(map[item]);
                if (!valReader.Read(ref val))
                {
                    return false;
                }
                value.Add(key, val);
            }
            return true;
        }

        private bool ReadObject(ref Object value)
        {
            if (value == null)
            {
                return false;
            }

            var obj = Current;
            if (obj == null)
            {
                return false;
            }

            var map = obj as IDictionary;
            if (map == null)
            {
                return false;
            }

            var type = value.GetType();
            var listField = DataHelper.GetClassFields(type);
            if (listField == null)
            {
                return false;
            }
            for (int i = 0; i < listField.Count; i++)
            {
                var field = listField[i];
                string name = field.Name;
                if (!map.Contains(name))
                {
                    continue;
                }

                var val = map[name];

                Type memberType = field.FieldType;
                var item = DataHelper.Create(memberType);

                DJsonReader reader = new DJsonReader().Append(val);
                if (!reader.Read(ref item))
                {
                    return false;
                }

                field.SetValue(value, item);
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
                        if (Read(ref value)) { obj = value; }
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
                        return this.ReadObject(ref obj);
                    }
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
