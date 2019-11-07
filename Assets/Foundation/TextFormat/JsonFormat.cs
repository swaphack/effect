using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Foundation.TextFormat
{
    /// <summary>
    /// json文本读取
    /// </summary>
    public class JsonFormat
    {
        /// <summary>
        /// 普通分隔符
        /// </summary>
        public const string NormalSeparator = ",";
        /// <summary>
        /// 列表分隔符
        /// </summary>
        public const string ListStart = "[";
        /// <summary>
        /// 列表分隔符
        /// </summary>
        public const string ListEnd = "]";
        /// <summary>
        /// 字典分隔符
        /// </summary>
        public const string MapStart = "{";
        /// <summary>
        /// 字典分隔符
        /// </summary>
        public const string MapEnd = "}";
        /// <summary>
        /// 字典分隔符
        /// </summary>
        public const string MapSeparator = ":";

        /// <summary>
        /// 待处理的文本
        /// </summary>
        private string _text;

        public override string ToString()
        {
            return _text;
        }

        private TextParser _parser;

        public JsonFormat(string text)
        {
            _text = text;
            this.Init();
        }

        private void Init()
        {
            _parser = new TextParser();

            _parser.AddParseFunc(new FieldParser(NormalSeparator, this.PushField));
            _parser.AddParseFunc(new FieldParser(ListStart, this.PushList));
            _parser.AddParseFunc(new FieldParser(ListEnd, this.PopList));
            _parser.AddParseFunc(new FieldParser(MapStart, this.PushMap));
            _parser.AddParseFunc(new FieldParser(MapEnd, this.PopMap));
            _parser.AddParseFunc(new FieldParser(MapSeparator, this.SeparateMapField));
            _parser.AddParseFunc(new FieldParser(null, this.PushText));
        }

        /// <summary>
        /// 零时文本
        /// </summary>
        private string _tempText = "";
        /// <summary>
        /// 结构树
        /// </summary>
        private object _tree;
        /// <summary>
        /// 堆栈
        /// </summary>
        private Stack<object> _stack = new Stack<object>();

        public object Tree
        {
            get
            {
                return _tree;
            }
        }

        private bool PushField(string str)
        {
            do
            {
                if (string.IsNullOrEmpty(_tempText))
                {
                    return true;
                }

                var obj = _stack.Peek();
                var type = obj.GetType();
                if (!type.IsGenericType)
                {
                    return false;
                }

                if (typeof(List<>) == type.GetGenericTypeDefinition())
                {
                    var temp = (IList)obj;
                    temp.Add(GetTempText());
                }
                else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                {
                    var temp = (IDictionary)obj;
                    Object key = null;
                    foreach (var item in temp.Keys)
                    {
                        if (temp[item] == null)
                        {
                            key = item;
                        }
                    }
                    if (key == null)
                    {
                        return false;
                    }
                    temp[key] = GetTempText();
                }
            } while (false);
            return true;
        }

        private bool PopStack(string str)
        {
            if (_stack.Count == 1)
            {
                _tree = _stack.Pop();
                return true;
            }
            var top = _stack.Pop();
            var obj = _stack.Peek();
            var type = obj.GetType();
            if (!type.IsGenericType)
            {
                return false;
            }

            if (typeof(List<>) == type.GetGenericTypeDefinition())
            {
                var temp = (IList)obj;
                temp.Add(top);
            }
            else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
            {
                var temp = (IDictionary)obj;
                Object key = null;
                foreach (var item in temp.Keys)
                {
                    if (temp[item] == null)
                    {
                        key = item;
                    }
                }

                temp[key] = top;
            }

            return true;
        }

        private bool PushList(string str)
        {
            var lst = new List<Object>();
            _stack.Push(lst);

            return true;
        }

        private bool PopList(string str)
        {
            PushField(str);
            return PopStack(str);
        }

        private bool PushMap(string str)
        {
            var map = new Dictionary<Object, Object>();
            _stack.Push(map);

            return true;
        }

        private bool SeparateMapField(string str)
        {
            var map = (IDictionary)_stack.Peek();
            if (string.IsNullOrEmpty(_tempText))
            {
                return false;
            }

            map.Add(GetTempText(), null);

            return true;
        }

        private bool PopMap(string str)
        {
            PushField(str);
            return PopStack(str);
        }

        private bool PushText(string str)
        {
            _tempText += str;
            return true;
        }

        private string GetTempText()
        {
            string text = _tempText;
            if (text.StartsWith("\"", StringComparison.Ordinal))
            {
                text = text.TrimStart('\"');
            }

            if (text.EndsWith("\"", StringComparison.Ordinal))
            {
                text = text.TrimEnd('\"');
            }

            _tempText = "";
            return text;
        }

        public bool Parse()
        {
            return _parser.Parse(_text);
        }
    }
}
