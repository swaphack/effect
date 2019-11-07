using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Game.Editor.Widgets
{
    public static class UIWidgetHelper
    {
        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static UIFieldWidget CreateWidget(string name, Object value)
        {
            var type = value.GetType();
            TypeCode code = Type.GetTypeCode(type);
            UIFieldWidget widget = null;
            switch (code)
            {
                case TypeCode.Boolean:
                    widget = new UIBooleanFieldWidget(name, value); break;
                case TypeCode.Char:
                    widget = new UITextFieldWidget(name, value); break;
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    widget = new UIIntFieldWidget(name, value); break;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    widget = new UILongFieldWidget(name, value); break;
                case TypeCode.Single:
                    widget = new UIFloatFieldWidget(name, value); break;
                case TypeCode.Double:
                    widget = new UIDoubleFieldWidget(name, value); break;
                case TypeCode.String:
                    widget = new UITextFieldWidget(name, value); break;
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            widget = new UIListFieldWidget(name, value);
                        }
                        else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            widget = new UIDictionaryFieldWidget(name, value);
                        }
                    }
                    else
                    {
                        if (type == typeof(Enum))
                        {
                            widget = new UIEnumFieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Vector2))
                        {
                            widget = new UIVector2FieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Vector3))
                        {
                            widget = new UIVector3FieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Vector4))
                        {
                            widget = new UIVector4FieldWidget(name, value);
                        }
                        else if (type == typeof(UnityEngine.Bounds))
                        {
                            widget = new UIBoundsFieldWidget(name, value);
                        }
                        else
                        {
                            widget = new UIObjectFieldWidget(name, value);
                        }
                    }
                    break;
                default:
                    break;
            }
            if (widget != null)
            {
                widget.OnValueChanged = (object val) =>
                {
                    value = val;
                };
            }
            return widget;
        }

        public static UIFieldWidget CreateWidget(object target, FieldInfo fieldInfo)
        {
            var type = fieldInfo.FieldType;
            TypeCode code = Type.GetTypeCode(type);
            UIFieldWidget widget = null;
            switch (code)
            {
                case TypeCode.Boolean:
                    widget = new UIBooleanFieldWidget(target, fieldInfo); break;
                case TypeCode.Char:
                    widget = new UITextFieldWidget(target, fieldInfo); break;
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                    widget = new UIIntFieldWidget(target, fieldInfo); break;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                    widget = new UILongFieldWidget(target, fieldInfo); break;
                case TypeCode.Single:
                    widget = new UIFloatFieldWidget(target, fieldInfo); break;
                case TypeCode.Double:
                    widget = new UIDoubleFieldWidget(target, fieldInfo); break;
                case TypeCode.String:
                    widget = new UITextFieldWidget(target, fieldInfo); break;
                case TypeCode.Object:
                    if (type.IsGenericType)
                    {
                        if (typeof(List<>) == type.GetGenericTypeDefinition())
                        {
                            widget = new UIListFieldWidget(target, fieldInfo);
                        }
                        else if (typeof(Dictionary<,>) == type.GetGenericTypeDefinition())
                        {
                            widget = new UIDictionaryFieldWidget(target, fieldInfo);
                        }
                    }
                    else
                    {
                        widget = new UIObjectFieldWidget(target, fieldInfo);
                    }
                    break;
                default:
                    break;
            }
            if (widget != null)
            {
                widget.OnValueChanged = (object value) =>
                {
                    fieldInfo.SetValue(target, value);
                };
            }
            return widget;
        }
    }
}
