using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Foundation.TextFormat
{
    /// <summary>
    /// 解析字段
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public delegate bool ParseFieldFunc(string str);

    /// <summary>
    /// 字段解析接口
    /// </summary>
    public interface IFieldParse
    {
        ParseFieldFunc Func { get; }
        /// <summary>
        /// 关键字
        /// </summary>
        string KeyWord { get; }
        bool Match(string str, int offset, ref int count);
    }
}
