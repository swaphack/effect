using Assets.Editor.EGUI;
using System;
using System.Collections;
using System.Reflection;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// 属性字段控件
    /// </summary>
    public class UIFieldWidget : UIWidget, IKeyValueRecord
    {
        public delegate void FieldValueFunc(object value);

        /// <summary>
        /// 控件信息
        /// </summary>
        private IKeyValueRecord _record;
        /// <summary>
        /// 值改变时的回调
        /// </summary>
        private FieldValueFunc _valueChangedFunc;
        public FieldValueFunc OnValueChanged
        {
            get { return _valueChangedFunc; }
            set { _valueChangedFunc = value; }
        }
        public string Name
        {
            get
            {
                return _record.Name;
            }
        }

        public Object Value
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
                if (OnValueChanged != null)
                {
                    OnValueChanged(value);
                }
            }
        }

        public UIFieldWidget(IKeyValueRecord record)
        {
            _record = record;
            this.InitWidget();
        }

        public UIFieldWidget(string name, Object value)
            :this(new CommonRecord(name, value))
        { 
        }

        public UIFieldWidget(Object target, FieldInfo fieldInfo)
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

            BLabel label = new BLabel();
            label.FontSize = 9;
            label.Text = Name;
            this.AddLabel(label);

            this.InitField();
        }

        protected void AddLabel(Widget w)
        {
            w.Option.Width = 1;
            w.Option.ExpandWidth = false;
            this.Add(w);
        }

        protected void AddField(Widget w)
        {
            w.Option.ExpandWidth = true;
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
        public UIBooleanFieldWidget(string name, Object value)
            :base(name,  value)
        { 
        }

        public UIBooleanFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EToggle toggle = new EToggle();
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
        public UIIntFieldWidget(string name, Object value)
            :base(name,  value)
        { 
        }

        public UIIntFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EIntField intField = new EIntField();
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
        public UILongFieldWidget(string name, Object value)
            : base(name,  value)
        {
        }

        public UILongFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            ELongField longField = new ELongField();
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
        public UIFloatFieldWidget(string name, Object value)
            :base(name,  value)
        { 
        }

        public UIFloatFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EFloatField floatField = new EFloatField();
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
        public UIDoubleFieldWidget(string name, Object value)
            :base(name,  value)
        { 
        }

        public UIDoubleFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            EDoubleField doubleField = new EDoubleField();
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
        public UILabelFieldWidget(string name, Object value)
            : base(name,  value)
        {
        }

        public UILabelFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            ELabelField labelField = new ELabelField();
            labelField.Text = this.GetStringValue();
            labelField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(labelField.Text);
            };
            this.AddField(labelField);
        }
    }
    /// <summary>
    /// 文本控件
    /// </summary>
    public class UITextFieldWidget : UIFieldWidget
    {
        public UITextFieldWidget(string name, Object value)
            :base(name,  value)
        { 
        }

        public UITextFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        { 
        }

        protected override void InitField()
        {
            ETextField textField = new ETextField();
            textField.Text = this.GetStringValue();
            textField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(textField.Text);
            };
            this.AddField(textField);
        }
    }

    /// <summary>
    /// 文本控件
    /// </summary>
    public class UITextAreaWidget : UIFieldWidget
    {
        public UITextAreaWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UITextAreaWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            ETextArea textField = new ETextArea();
            textField.Text = this.GetStringValue();
            textField.TriggerHandler = (Widget w) =>
            {
                this.SetValue(textField.Text);
            };
            this.AddField(textField);
        }
    }

    /// <summary>
    /// 列表控件
    /// </summary>
    public class UIListFieldWidget : UIFieldWidget
    {
        public UIListFieldWidget(string name, Object value)
            :base(name,  value)
        {
            Direction = LayoutDirection.Vertical;
        }

        public UIListFieldWidget(Object target, FieldInfo fieldInfo)
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
        public UIDictionaryFieldWidget(string name, Object value)
            :base(name,  value)
        {
            Direction = LayoutDirection.Vertical;
        }

        public UIDictionaryFieldWidget(Object target, FieldInfo fieldInfo)
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
                EVerticalLayout vl = new EVerticalLayout();
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
        public UIObjectFieldWidget(string name, Object value)
            :base(name,  value)
        {
            Direction = LayoutDirection.Vertical;
        }

        public UIObjectFieldWidget(Object target, FieldInfo fieldInfo)
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
        public UIEnumFieldWidget(string name, Object value)
            :base(name,  value)
        {
        }

        public UIEnumFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EEnumPopup popup = new EEnumPopup();
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
        public UIVector2FieldWidget(string name, Object value)
            : base(name,  value)
        {
        }

        public UIVector2FieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EVector2Field field = new EVector2Field();
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
        public UIVector3FieldWidget(string name, Object value)
            : base(name,  value)
        {
        }

        public UIVector3FieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EVector3Field field = new EVector3Field();
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
        public UIVector4FieldWidget(string name, Object value)
            : base(name,  value)
        {
        }

        public UIVector4FieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EVector4Field field = new EVector4Field();
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
        public UIBoundsFieldWidget(string name, Object value)
            : base(name,  value)
        {
        }

        public UIBoundsFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EBoundsField field = new EBoundsField();
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

        public UIIntPopupFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UIIntPopupFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        private EIntPopup _intPopup;
        protected override void BeginDraw()
        {
            base.BeginDraw();

            _intPopup.AddRange(Describes, Indexs);
        }

        protected override void InitField()
        {
            EIntPopup intPopup = new EIntPopup();
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

        public UIIntSlideFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UIIntSlideFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        private EIntSlider _intSlider;
        protected override void BeginDraw()
        {
            base.BeginDraw();

            _intSlider.MinValue = MinValue;
            _intSlider.MaxValue = MaxValue;
        }

        protected override void InitField()
        {
            EIntSlider intSlider = new EIntSlider();
            
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
        public UIColorFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UIColorFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EColorField colorField = new EColorField();
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
        public UIMaterialFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UIMaterialFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EObjectField objectField = new EObjectField();
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
    /// Texture控件
    /// </summary>
    public class UITextureFieldWidget : UIFieldWidget
    {
        public UITextureFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UITextureFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EObjectField objectField = new EObjectField();
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
        public UISpriteFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UISpriteFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EObjectField objectField = new EObjectField();
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
    /// GameObject控件
    /// </summary>
    public class UIGameObjectFieldWidget : UIFieldWidget
    {
        public UIGameObjectFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UIGameObjectFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EObjectField objectField = new EObjectField();
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
        public UITransformFieldWidget(string name, Object value)
            : base(name, value)
        {
        }

        public UITransformFieldWidget(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        protected override void InitField()
        {
            EObjectField objectField = new EObjectField();
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
