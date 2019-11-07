using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Game.Editor.Widgets
{
    /// <summary>
    /// 属性字段控件
    /// </summary>
    public class UIFieldWidget : UIWidget, IKeyValueRecord
    {
        public delegate void FieldValueDelegate(object value);

        /// <summary>
        /// 控件信息
        /// </summary>
        private IKeyValueRecord _record;
        /// <summary>
        /// 值改变时的处理
        /// </summary>
        public FieldValueDelegate OnValueChanged { get; set; }
        /// <summary>
        /// 值改变时的内部处理
        /// </summary>
        protected FieldValueDelegate OnInternalValueChanged { get; set; }

        public string Name
        {
            get
            {
                return _record.Name;
            }
        }

        public object Value
        {
            get
            {
                if (_record == null)
                {
                    return null;
                }
                return _record.Value;
            }
            set
            {
                if (_record == null)
                {
                    return;
                }
                _record.Value = value;
                OnInternalValueChanged?.Invoke(value);
                OnValueChanged?.Invoke(value);
            }
        }

        public UIFieldWidget(IKeyValueRecord record)
        {
            _record = record;
            this.InitWidget();
        }

        public UIFieldWidget(string name, object value)
            :this(new CommonRecord(name, value))
        { 
        }

        public UIFieldWidget(object target, FieldInfo fieldInfo)
            : this(new FieldRecord(target, fieldInfo))
        { 
        }

        /// <summary>
        /// 获取限定类型的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetValue<T>()
        {
            return (T)Value;
        }

        /// <summary>
        /// 将获取的值，转为字符串类型
        /// </summary>
        /// <returns></returns>
        protected string GetStringValue()
        {
            return Convert.ToString(Value);
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value"></param>
        protected void SetValue(object value)
        {
            Value = value;
        }

        protected void InitWidget()
        {
            InnerSpace = 0;

            GUILabel label = new GUILabel();
            label.FontSize = 9;
            label.Text = Name;
            this.AddLabel(label);

            this.InitField();
        }

        protected void AddLabel(Widget w)
        {
            w.Option.MinWidth = 50;
            w.Option.MaxWidth = 150;
            w.Option.ExpandWidth = true;
            this.Add(w);
        }

        private Widget _fieldWidget;

        public T GetFieldWidget<T>() where T : Widget
        {
            if (_fieldWidget is T)
            {
                return (T)_fieldWidget;
            }

            return null;
        }

        protected void AddField(Widget w)
        {
            w.Option.ExpandWidth = true;
            _fieldWidget = w;
            this.Add(w);
        }

        protected virtual void InitField()
        {
 
        }
    }

    /// <summary>
    /// Boolean控件
    /// </summary>
    public class UIBooleanFieldWidget : UIFieldWidget
    {
        public UIBooleanFieldWidget(string name, object value)
            :base(name,  value)
        { 
        }

        public UIBooleanFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EditorToggle toggle = new EditorToggle();
            toggle.Value = GetValue<Boolean>();
            toggle.TriggerHandler = (Widget w) =>
            {
                this.SetValue(toggle.Value);
            };
            this.AddField(toggle);
        }
    }

    /// <summary>
    /// 整型控件
    /// </summary>
    public class UIIntFieldWidget : UIFieldWidget
    {
        public UIIntFieldWidget(string name, object value)
            :base(name,  value)
        { 
        }

        public UIIntFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EditorIntField intField = new EditorIntField();
            intField.Value = this.GetValue<int>();
            intField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(intField.Value);
            };
            this.AddField(intField);
        }
    }

    /// <summary>
    /// 长整型控件
    /// </summary>
    public class UILongFieldWidget : UIFieldWidget
    {
        public UILongFieldWidget(string name, object value)
            : base(name,  value)
        {
        }

        public UILongFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorLongField longField = new EditorLongField();
            longField.Value = this.GetValue<long>();
            longField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(longField.Value);
            };
            this.AddField(longField);
        }
    }

    /// <summary>
    /// 单精度浮点控件
    /// </summary>
    public class UIFloatFieldWidget : UIFieldWidget
    {
        public UIFloatFieldWidget(string name, object value)
            :base(name,  value)
        { 
        }

        public UIFloatFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EditorFloatField floatField = new EditorFloatField();
            floatField.Value = this.GetValue<float>();
            floatField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(floatField.Value);
            };
            this.AddField(floatField);
        }
    }

    /// <summary>
    /// 双精度浮点控件
    /// </summary>
    public class UIDoubleFieldWidget : UIFieldWidget
    {
        public UIDoubleFieldWidget(string name, object value)
            :base(name,  value)
        { 
        }

        public UIDoubleFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EditorDoubleField doubleField = new EditorDoubleField();
            doubleField.Value = this.GetValue<double>();
            doubleField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(doubleField.Value);
            };
            this.AddField(doubleField);
        }
    }
    /// <summary>
    /// 标签控件
    /// </summary>
    public class UILabelFieldWidget : UIFieldWidget
    {
        public UILabelFieldWidget(string name, object value)
            : base(name,  value)
        {
        }

        public UILabelFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorLabelField labelField = new EditorLabelField();
            labelField.Value = this.GetStringValue();
            labelField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(labelField.Value);
            };
            this.AddField(labelField);
        }
    }
    /// <summary>
    /// 文本控件
    /// </summary>
    public class UITextFieldWidget : UIFieldWidget
    {
        public UITextFieldWidget(string name, object value)
            :base(name,  value)
        { 
        }

        public UITextFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EditorTextField textField = new EditorTextField();
            textField.Value = this.GetStringValue();
            textField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(textField.Value);
            };
            this.AddField(textField);
        }
    }

    /// <summary>
    /// 文本控件
    /// </summary>
    public class UITextAreaWidget : UIFieldWidget
    {
        public UITextAreaWidget(string name, object value)
            : base(name, value)
        {
        }

        public UITextAreaWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorTextArea textField = new EditorTextArea();
            textField.Value = this.GetStringValue();
            textField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(textField.Value);
            };
            this.AddField(textField);
        }
    }

    /// <summary>
    /// 列表控件
    /// </summary>
    public class UIListFieldWidget : UIFieldWidget
    {
        public UIListFieldWidget(string name, object value)
            :base(name,  value)
        {
            Direction = LayoutDirection.Vertical;
        }

        public UIListFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            Direction = LayoutDirection.Vertical;
        }

        protected override void InitField()
        {
            var obj = GetValue<IList>();
            int i = 0;
            foreach (var item in obj)
            {
                var field = UIWidgetHelper.CreateWidget(string.Format("{0}", i), item);
                if (field != null)
                {
                    this.AddField(field);
                }
                i++;
            }
        }
    }

    /// <summary>
    /// 字典控件
    /// </summary>
    public class UIDictionaryFieldWidget : UIFieldWidget
    {
        public UIDictionaryFieldWidget(string name, object value)
            :base(name,  value)
        {
            Direction = LayoutDirection.Vertical;
        }

        public UIDictionaryFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            Direction = LayoutDirection.Vertical;
        }

        protected override void InitField()
        {
            var obj = this.GetValue<IDictionary>();
            var keys = obj.Keys;

            foreach (var item in keys)
            {
                EditorVerticalLayout vl = new EditorVerticalLayout();
                this.AddField(vl);
                {
                    var field = UIWidgetHelper.CreateWidget("Key", item);
                    if (field != null)
                    {
                        vl.Add(field);
                    }
                }
                {
                    var field = UIWidgetHelper.CreateWidget("Value", obj[item]);
                    if (field != null)
                    {
                        vl.Add(field);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 对象控件
    /// </summary>
    public class UIObjectFieldWidget : UIFieldWidget
    {
        public UIObjectFieldWidget(string name, object value)
            :base(name,  value)
        {
            Direction = LayoutDirection.Vertical;
        }

        public UIObjectFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            Direction = LayoutDirection.Vertical;
        }

        protected override void InitField()
        {
            var obj = this.Value;

            var fields = obj.GetType().GetFields();
            foreach (var item in fields)
            {
                var field = UIWidgetHelper.CreateWidget(obj, item);
                if (field != null)
                {
                    field.OnValueChanged = (object value) =>
                    {
                        item.SetValue(obj, value);
                        Value = obj;
                    };
                    this.AddField(field);
                }
            }
        }
    }

    /// <summary>
    /// 枚举控件
    /// </summary>
    public class UIEnumFieldWidget : UIFieldWidget
    {
        public UIEnumFieldWidget(string name, object value)
            :base(name,  value)
        {
        }

        public UIEnumFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorEnumPopup popup = new EditorEnumPopup();
            popup.Value = this.GetValue<Enum>();
            popup.TriggerHandler = (Widget w) =>
            {
                this.SetValue(popup.Value);
            };
            this.AddField(popup);
        }
    }

    /// <summary>
    /// 二维坐标
    /// </summary>
    public class UIVector2FieldWidget : UIFieldWidget
    {
        public UIVector2FieldWidget(string name, object value)
            : base(name,  value)
        {
        }

        public UIVector2FieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorVector2Field field = new EditorVector2Field();
            field.Value = this.GetValue<UnityEngine.Vector2>();
            field.TriggerHandler = (Widget w) =>
            {
                this.SetValue(field.Value);
            };
            this.AddField(field);
        }
    }

    /// <summary>
    /// 三维坐标
    /// </summary>
    public class UIVector3FieldWidget : UIFieldWidget
    {
        public UIVector3FieldWidget(string name, object value)
            : base(name,  value)
        {
        }

        public UIVector3FieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorVector3Field field = new EditorVector3Field();
            field.Value = this.GetValue<UnityEngine.Vector3>();
            field.TriggerHandler = (Widget w) =>
            {
                this.SetValue(field.Value);
            };
            this.AddField(field);
        }
    }

    /// <summary>
    /// 四维坐标
    /// </summary>
    public class UIVector4FieldWidget : UIFieldWidget
    {
        public UIVector4FieldWidget(string name, object value)
            : base(name,  value)
        {
        }

        public UIVector4FieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorVector4Field field = new EditorVector4Field();
            field.Value = this.GetValue<UnityEngine.Vector4>();
            field.TriggerHandler = (Widget w) =>
            {
                this.SetValue(field.Value);
            };
            this.AddField(field);
        }
    }

    /// <summary>
    /// 边框控件
    /// </summary>
    public class UIBoundsFieldWidget : UIFieldWidget
    {
        public UIBoundsFieldWidget(string name, object value)
            : base(name,  value)
        {
        }

        public UIBoundsFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorBoundsField field = new EditorBoundsField();
            field.Bounds = this.GetValue<UnityEngine.Bounds>();
            field.TriggerHandler = (Widget w) =>
            {
                this.SetValue(field.Bounds);
            };
            this.AddField(field);
        }
    }

    /// <summary>
    /// 多选项列表控件
    /// </summary>
    public class UIIntPopupFieldWidget : UIFieldWidget
    {
        /// <summary>
        /// 描述
        /// </summary>
        private string[] _describes;
        /// <summary>
        /// 索引
        /// </summary>
        private int[] _indexs;

        /// <summary>
        /// 描述
        /// </summary>
        public string[] Describes
        {
            get { return _describes; }
            set { _describes = value; }
        }
        /// <summary>
        /// 索引
        /// </summary>
        public int[] Indexs
        {
            get { return _indexs; }
            set { _indexs = value; }
        }

        public UIIntPopupFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UIIntPopupFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        private EditorIntPopup _intPopup;
        protected override void BeginDraw()
        {
            base.BeginDraw();

            _intPopup.AddRange(Describes, Indexs);
        }

        protected override void InitField()
        {
            EditorIntPopup intPopup = new EditorIntPopup();
            intPopup.Value = this.GetValue<int>();
            intPopup.TriggerHandler = (Widget w) =>
            {
                this.SetValue(intPopup.Value);
            };
            this.AddField(intPopup);

            _intPopup = intPopup;
        }
    }

    /// <summary>
    /// 可调整数值的整型控件
    /// </summary>
    public class UIIntSlideFieldWidget : UIFieldWidget
    {
        /// <summary>
        /// 当大值
        /// </summary>
        private int _maxValue;
        /// <summary>
        /// 最小值
        /// </summary>
        private int _minValue;

        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public UIIntSlideFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UIIntSlideFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        private EditorIntSlider _intSlider;
        protected override void BeginDraw()
        {
            base.BeginDraw();

            _intSlider.MinValue = MinValue;
            _intSlider.MaxValue = MaxValue;
        }

        protected override void InitField()
        {
            EditorIntSlider intSlider = new EditorIntSlider();
            
            intSlider.Value = this.GetValue<int>();
            intSlider.TriggerHandler = (Widget w) =>
            {
                this.SetValue(intSlider.Value);
            };
            this.AddField(intSlider);

            _intSlider = intSlider;
        }
    }

    /// <summary>
    /// Color控件
    /// </summary>
    public class UIColorFieldWidget : UIFieldWidget
    {
        public UIColorFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UIColorFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorColorField colorField = new EditorColorField();
            colorField.Color = GetValue<UnityEngine.Color>();
            colorField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(colorField.Color);
            };
            this.AddField(colorField);
        }
    }

    /// <summary>
    /// Material控件
    /// </summary>
    public class UIMaterialFieldWidget : UIFieldWidget
    {
        public UIMaterialFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UIMaterialFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorObjectField objectField = new EditorObjectField();
            objectField.TargetType = typeof(UnityEngine.Material);
            objectField.Target = GetValue<UnityEngine.Material>();
            objectField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(objectField.Target);
            };
            this.AddField(objectField);
        }
    }

    /// <summary>
    /// Shader控件
    /// </summary>
    public class UIShaderFieldWidget : UIFieldWidget
    {
        public UIShaderFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UIShaderFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorObjectField objectField = new EditorObjectField();
            objectField.TargetType = typeof(UnityEngine.Shader);
            objectField.Target = GetValue<UnityEngine.Shader>();
            objectField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(objectField.Target);
            };
            this.AddField(objectField);
        }
    }

    /// <summary>
    /// Texture控件
    /// </summary>
    public class UITextureFieldWidget : UIFieldWidget
    {
        public UITextureFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UITextureFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorObjectField objectField = new EditorObjectField();
            objectField.TargetType = typeof(UnityEngine.Texture);
            objectField.Target = GetValue<UnityEngine.Texture>();
            objectField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(objectField.Target);
            };
            this.AddField(objectField);
        }
    }

    /// <summary>
    /// Sprite控件
    /// </summary>
    public class UISpriteFieldWidget : UIFieldWidget
    {
        public UISpriteFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UISpriteFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorObjectField objectField = new EditorObjectField();
            objectField.TargetType = typeof(UnityEngine.Sprite);
            objectField.Target = GetValue<UnityEngine.Sprite>();
            objectField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(objectField.Target);
            };
            this.AddField(objectField);
        }
    }

    /// <summary>
    /// Gameobject控件
    /// </summary>
    public class UIGameobjectFieldWidget : UIFieldWidget
    {
        public UIGameobjectFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UIGameobjectFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorObjectField objectField = new EditorObjectField();
            objectField.TargetType = typeof(UnityEngine.GameObject);
            objectField.Target = GetValue<UnityEngine.GameObject>();
            objectField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(objectField.Target);
            };
            this.AddField(objectField);
        }
    }

    /// <summary>
    /// Transform控件
    /// </summary>
    public class UITransformFieldWidget : UIFieldWidget
    {
        public UITransformFieldWidget(string name, object value)
            : base(name, value)
        {
        }

        public UITransformFieldWidget(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EditorObjectField objectField = new EditorObjectField();
            objectField.TargetType = typeof(UnityEngine.Transform);
            objectField.Target = GetValue<UnityEngine.Transform>();
            objectField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(objectField.Target);
            };
            this.AddField(objectField);
        }
    }
}
