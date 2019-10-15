using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assets.Editor.Widgets
{
    public class UIWidgetHelper
    {
        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static UIFieldWidget CreateWidget(string name, Object value)
        {
            var type = value.GetType();
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean:
                    return new UIBooleanFieldWidget(name, value);
                case TypeCode.Char:
                    return new UITextFieldWidget(name, value);
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return new UIIntFieldWidget(name, value);
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return new UILongFieldWidget(name, value);
                case TypeCode.Single:
                    return new UIFloatFieldWidget(name, value);
                case TypeCode.Double:
                    return new UIDoubleFieldWidget(name, value);
                case TypeCode.String:
                    return new UITextFieldWidget(name, value);
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            return new UIListFieldWidget(name, value);
                        }
                        else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            return new UIDictionaryFieldWidget(name, value);
                        }
                    }
                    else
                    {
                        if (type == typeof(Enum))
                        {
                            return new UIEnumFieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Vector2))
                        {
                            return new UIVector2FieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Vector3))
                        {
                            return new UIVector3FieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Vector4))
                        {
                            return new UIVector4FieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Bounds))
                        {
                            return new UIBoundsFieldWidget(name, value);
                        }
                        else
                        {
                            return new UIObjectFieldWidget(name, value);
                        }
                    }
                    break;
                default:
                    break;
            }
            return null;
        }

        public static UIFieldWidget CreateWidget(Object target, FieldInfo fieldInfo)
        {
            var type = fieldInfo.FieldType;
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean:
                    return new UIBooleanFieldWidget(target, fieldInfo);
                case TypeCode.Char:
                    return new UITextFieldWidget(target, fieldInfo);
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return new UIIntFieldWidget(target, fieldInfo);
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                    return new UIFloatFieldWidget(target, fieldInfo);
                case TypeCode.Double:
                    return new UIDoubleFieldWidget(target, fieldInfo);
                case TypeCode.String:
                    return new UITextFieldWidget(target, fieldInfo);
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            return new UIListFieldWidget(target, fieldInfo);
                        }
                        else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            return new UIDictionaryFieldWidget(target, fieldInfo);
                        }
                    }
                    else
                    {
                        return new UIObjectFieldWidget(target, fieldInfo);
                    }
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
