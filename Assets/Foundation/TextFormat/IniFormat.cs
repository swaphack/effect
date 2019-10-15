using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Foundation.TextFormat
{
    public class IniFormat
    {
        /// <summary>
        /// 字典分隔符
        /// </summary>
        public const string MapSeparator = "=";
        /// <summary>
        /// 段落分隔符
        /// </summary>
        public const string LineEnd = "\n";
        /// <summary>
        /// 回车符号
        /// </summary>
        public const string ReturnEnd = "\r";

        /// <summary>
        /// 节名字开始
        /// </summary>
        public const string BlockStart = "[";
        /// <summary>
        /// 节名字结束
        /// </summary>
        public const string BlockEnd = "]";

        /// <summary>
        /// 待处理的文本
        /// </summary>
        private string _text;

        public override string ToString()
        {
            return _text;
        }

        private TextParser _parser;

        public IniFormat(string text)
        {
            _text = text;
            this.Init();
        }

        private void Init()
        {
            _parser = new TextParser();

            _parser.AddParseFunc(new FieldParser(MapSeparator, this.PushField));
            _parser.AddParseFunc(new FieldParser(LineEnd, this.PushField));
            _parser.AddParseFunc(new FieldParser(ReturnEnd, this.SeparateMapField));
            _parser.AddParseFunc(new FieldParser(BlockStart, this.PushMap));
            _parser.AddParseFunc(new FieldParser(BlockEnd, this.PopMap));
            _parser.AddParseFunc(new FieldParser(null, this.PushText));
        }

        /// <summary>
        /// 临时文本
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

                if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
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
                    else
                    {
                        temp[key] = GetTempText();
                    }
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

            if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
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

        private bool PushMap(string str)
        {
            var map = new Dictionary<Object, Object>();
            _stack.Push(map);

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
            if (text.StartsWith("\""))
            {
                text = text.TrimStart('\"');
            }

            if (text.EndsWith("\""))
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
