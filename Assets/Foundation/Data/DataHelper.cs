using System;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Foundation.Data
{
    public class DataHelper
    {
        /// <summary>
        /// 根据类型，创建对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object Create(Type type)
        {
            if (Type.GetTypeCode(type) == TypeCode.String)
            {
                return string.Empty;
            }
            return System.Activator.CreateInstance(type);
        }

        /// <summary>
        /// 获取类型中成员遍量
        /// 按照继承关系显示
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<FieldInfo> GetClassFields(Type type)
        {
            if (type == null)
            {
                return null;
            }
            Stack<Type> stackType = new Stack<Type>();
            var tempType = type;
            while (tempType != typeof(Object))
            {
                stackType.Push(tempType);
                tempType = tempType.BaseType;
            }

            List<FieldInfo> listField = new List<FieldInfo>();
            HashSet<string> hashName = new HashSet<string>();
            while (stackType.Count != 0)
            {
                var topType = stackType.Pop();
                FieldInfo[] fieldInfos = topType.GetFields();
                if (fieldInfos == null || fieldInfos.Length == 0)
                {
                    continue;
                }
                for (var i = 0; i < fieldInfos.Length; i++)
                {
                    if (!hashName.Contains(fieldInfos[i].Name))
                    {
                        hashName.Add(fieldInfos[i].Name);
                        listField.Add(fieldInfos[i]);
                    }
                }
            }

            return listField;
        }

        /// <summary>
        /// 将字符串转为对象
        /// </summary>
        /// <param name="text"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ConvertStringToObject(string text, ref object obj)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Type type = obj.GetType();
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean:
                    {
                        obj = Convert.ToBoolean(text);
                        return true;
                    }
                case TypeCode.Char:
                    {
                        obj = Convert.ToChar(text);
                        return true;
                    };
                case TypeCode.SByte:
                    {
                        obj = Convert.ToSByte(text);
                        return true;
                    };
                case TypeCode.Byte:
                    {
                        obj = Convert.ToByte(text);
                        return true;
                    };
                case TypeCode.Int16:
                    {
                        obj = Convert.ToInt16(text);
                        return true;
                    };
                case TypeCode.UInt16:
                    {
                        obj = Convert.ToUInt16(text);
                        return true;
                    };
                case TypeCode.Int32:
                    {
                        obj = Convert.ToInt32(text);
                        return true;
                    };
                case TypeCode.UInt32:
                    {
                        obj = Convert.ToUInt32(text);
                        return true;
                    };
                case TypeCode.Int64:
                    {
                        obj = Convert.ToInt64(text);
                        return true;
                    };
                case TypeCode.UInt64:
                    {
                        obj = Convert.ToUInt64(text);
                        return true;
                    };
                case TypeCode.Single:
                    {
                        obj = Convert.ToSingle(text);
                        return true;
                    };
                case TypeCode.Double:
                    {
                        obj = Convert.ToDouble(text);
                        return true;
                    };
                case TypeCode.String:
                    {
                        obj = Convert.ToString(text);
                        return true;
                    };
                default:
                    return false;
            }
        }
    }
}
