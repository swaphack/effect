using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.TextFormat
{
    /// <summary>
    /// 字段解析
    /// </summary>
    public class FieldParser : IFieldParse
    {
        /// <summary>
        /// 关键字
        /// </summary>
        private string _keyword;
        /// <summary>
        /// 解析方法
        /// </summary>
        private ParseFieldFunc _func;

        public ParseFieldFunc Func { get { return _func; } }
        public string KeyWord { get { return _keyword; } }

        public FieldParser(string key, ParseFieldFunc func)
        {
            this._keyword = key;
            this._func = func;
        }

        public bool Match(string str, int offset, ref int count)
        {
            if (Func == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(str))
            {
                return false;
            }


            if (string.IsNullOrEmpty(this.KeyWord))
            {
                var temp = new string(str[offset], 1);
                if (!Func(temp))
                {
                    return false;
                }
                count = 1;
                return true;
            }
            else if (this.KeyWord == str.Substring(offset, this.KeyWord.Length))
            {
                if (!Func(this.KeyWord))
                {
                    return false;
                }
                count = this.KeyWord.Length;
                return true;
            }
            return false;
        }
    }
}
