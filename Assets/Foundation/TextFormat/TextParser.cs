﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.TextFormat
{
    /// <summary>
    /// 文本字符格式解析
    /// </summary>
    public class TextParser
    {
        /// <summary>
        /// 待处理的文本
        /// </summary>
        private string _text;
        /// <summary>
        /// 解析后的树形结构
        /// </summary>
        private object _tree;
        /// <summary>
        /// 字符解析
        /// </summary>
        private List<IFieldParse> _funcs;

        public object Tree
        {
            get
            {
                return _tree;
            }
            protected set
            {
                _tree = value;
            }
        }

        public override string ToString()
        {
            return _text;
        }

        public TextParser()
        {
            _funcs = new List<IFieldParse>();
        }

        public void AddParseFunc(IFieldParse func)
        {
            if (func == null)
            {
                return;
            }
            if (_funcs.Contains(func))
            {
                return;
            }
            else
            {
                _funcs.Add(func);
            }
        }

        public bool Parse(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            _text = text;

            int index = 0;
            while (index < text.Length)
            {
                int count = 0;
                foreach (var item in _funcs)
                {
                    if (item.Match(text, index, ref count))
                    {
                        break;
                    }
                }

                if (count != 0)
                {
                    index += count;
                }
                else
                {
                    return false;
                }
                
            }

            return true;
        }
    }
}
