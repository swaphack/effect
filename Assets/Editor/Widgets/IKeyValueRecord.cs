using System;
using System.Reflection;

namespace Game.Editor.Widgets
{
    public interface IKeyValueRecord
    {
        /// <summary>
        /// 名字
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 值
        /// </summary>
        object Value { get; set; }

    }

    /// <summary>
    /// 公共记录
    /// </summary>
    public class CommonRecord : IKeyValueRecord
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string _name;

        /// <summary>
        /// 值
        /// </summary>
        private object _value;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public CommonRecord(string name, object value)
        {
            _name = name;
            _value = value;
        }
    }

    /// <summary>
    /// 字段记录
    /// </summary>
    public class FieldRecord : IKeyValueRecord
    {
        /// <summary>
        /// 字段信息
        /// </summary>
        private FieldInfo _fieldInfo;
        /// <summary>
        /// 对象
        /// </summary>
        private object _object;

        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get { return _fieldInfo.GetValue(_object); }
            set { _fieldInfo.SetValue(_object, value); }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _fieldInfo.Name; }
        }

        public FieldRecord(object obj, FieldInfo fieldInfo)
        {
            _object = obj;
            _fieldInfo = fieldInfo;
        }
    }
}
