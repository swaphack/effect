using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EBoundsField : Widget
    {
        private Bounds _bounds;

        public Bounds Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }

        public EBoundsField()
        {
            _bounds = new Bounds();
        }

        protected override void OnDraw()
        {
            Bounds bounds = EditorGUILayout.BoundsField(Content, Bounds, Option.Values);
            if (bounds != Bounds)
            {
                _bounds = bounds;
                this.DipatchEvent();
            }
        }
    }

    public class EColorField : Widget
    {
        private Color _color;

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public EColorField()
        {
            _color = new Color();
        }

        protected override void OnDraw()
        {
            Color color = EditorGUILayout.ColorField(Content, Color, Option.Values);
            if (color != Color)
            {
                _color = color;
                this.DipatchEvent();
            }
        }
    }

    public class ECurveField : Widget
    {
        private AnimationCurve _animationCurve;

        public AnimationCurve AnimationCurve
        {
            get
            {
                return _animationCurve;
            }
        }

        public ECurveField()
        {
            _animationCurve = new AnimationCurve();
        }

        protected override void OnDraw()
        {
            AnimationCurve animationCurve = EditorGUILayout.CurveField(Content, AnimationCurve, Option.Values);
            if (animationCurve != AnimationCurve)
            {
                _animationCurve = animationCurve;
                this.DipatchEvent();
            }
        }
    }

    public class EDelayedDoubleField : Widget
    {
        private double _value;

        public double Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            double value = EditorGUILayout.DelayedDoubleField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EDelayedFloatField : Widget
    {
        private float _value;

        public float Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            float value = EditorGUILayout.DelayedFloatField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EDelayedIntField : Widget
    {
        private int _value;

        public int Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.DelayedIntField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EDelayedTextField : Widget
    {

        protected override void OnDraw()
        {
            string value = EditorGUILayout.DelayedTextField(Content, Text, Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }

    public class EDoubleField : Widget
    {
        private double _value;

        public double Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            double value = EditorGUILayout.DoubleField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EDropdownButton : Widget
    {
        private FocusType _value;

        public FocusType Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            bool result = EditorGUILayout.DropdownButton(Content, Value, Option.Values);
            FocusType value = result ? FocusType.Keyboard : FocusType.Passive;
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EEnumPopup : Widget
    {
        private Enum _value;

        public Enum Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Enum value = EditorGUILayout.EnumPopup(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EFloatField : Widget
    {
        private float _value;

        public float Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            float value = EditorGUILayout.FloatField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EFoldout : Widget
    {
        private bool _foldout;

        private bool _toggleOnLabelClick;

        public bool Foldout
        {
            get { return _foldout; }

            set { _foldout = value; }
        }

        public bool ToggleOnLabelClick
        {
            get { return _toggleOnLabelClick; }

            set { _toggleOnLabelClick = value; }
        }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.Foldout(Foldout, Content, ToggleOnLabelClick);
            if (value != Foldout)
            {
                Foldout = value;
                this.DipatchEvent();
            }
        }
    }

    public class EHelpBox : Widget
    {
        private string _message;
        private MessageType _type;

        public string Message
        {
            get { return _message; }

            set { _message = value; }
        }

        public MessageType Type
        {
            get { return _type; }

            set { _type = value; }
        }

        protected override void OnDraw()
        {
            EditorGUILayout.HelpBox(Message, Type);
        }
    }

    public class EHorizontalLine : ERect
    {
        /// <summary>
        /// 填充
        /// </summary>
        private float _padding;
        /// <summary>
        /// 厚度
        /// </summary>
        private float _thickness;
        /// <summary>
        /// 填充
        /// </summary>
        public float Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }
        /// <summary>
        /// 厚度
        /// </summary>
        public float Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        public EHorizontalLine()
        {
            _padding = 0;
            _thickness = 1;
        }

        protected override void BeginDraw()
        {
            var r = EditorGUILayout.GetControlRect(GUILayout.Height(Padding + Thickness));
            r.height = Thickness;
            r.x += Padding;
            r.width -= 2 * Padding;

            Rect = r;
        }
    }

    public class EInspectorTitlebar : Widget
    {

        private bool _foldout;

        private bool _expandable;

        private UnityEngine.Object[] _targetObjs;

        public bool Foldout
        {
            get { return _foldout; }

            set { _foldout = value; }
        }

        public bool Expandable
        {
            get { return _expandable; }

            set { _expandable = value; }
        }

        public UnityEngine.Object[] TargetObjs
        {
            get { return _targetObjs; }

            set { _targetObjs = value; }
        }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.InspectorTitlebar(Foldout, TargetObjs, Expandable);
            if (value != Foldout)
            {
                Foldout = value;
                this.DipatchEvent();
            }
        }
    }

    public class EIntField : Widget
    {
        private int _value;
        public int Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.IntField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EIntPopup : Widget
    {
        public class DisplayItem
        {
            public GUIContent item;
            public int value;

            public DisplayItem(GUIContent item, int value)
            {
                this.item = item;
                this.value = value;
            }

            public DisplayItem(string text, int value)
                :this(new GUIContent(text), value)
            {
            }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        private int _value;

        private List<DisplayItem> _displayItems;
        /// <summary>
        /// 当前值
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public EIntPopup()
        {
            _displayItems = new List<DisplayItem>();
        }

        public void AddRange(string[] textAry, int[] valueAry)
        {
            if (textAry == null || textAry.Length == 0
                || valueAry == null || valueAry.Length == 0)
            {
                return;
            }

            if (textAry.Length > valueAry.Length)
            {
                Debug.LogError("Not Match Length With Input Text And Value");
                return;
            }

            for (var i = 0; i < textAry.Length; i++)
            {
                this.Add(textAry[i], valueAry[i]);
            }
        }

        public void Add(string text, int value)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            this.Add(new GUIContent(text), value);
        }

        public void Add(GUIContent content, int value)
        {
            if (content == null)
            {
                return;
            }

            this.Add(new DisplayItem(content, value));
        }

        public void Add(DisplayItem item)
        {
            if (item == null)
            {
                return;
            }

            _displayItems.Add(item);
        }

        public void Remove(DisplayItem item)
        {
            if (item == null)
            {
                return;
            }

            _displayItems.Remove(item);
        }

        public void Clear()
        {
            _displayItems.Clear();
        }

        protected override void OnDraw()
        {
            List<GUIContent> displayedOptions = new List<GUIContent>();
            List<int> optionValues = new List<int>();

            foreach(var item in _displayItems)
            {
                displayedOptions.Add(item.item);
                optionValues.Add(item.value);
            }

            int value = EditorGUILayout.IntPopup(Content, Value, displayedOptions.ToArray(), optionValues.ToArray(), Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EIntSlider : Widget
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private int _value;
        /// <summary>
        /// 最小值
        /// </summary>
        private int _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private int _maxValue = 100;

        /// <summary>
        /// 当前值
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
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

        protected override void OnDraw()
        {
            int value = EditorGUILayout.IntSlider(Content, Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EKnob : Widget
    {
        public Vector2 _knobSize;
        
        public float _value;
        
        public float _minValue;
        
        public float _maxValue;
        
        public string _unit;
        
        public Color _backgroundColor;
        
        public Color _activeColor;
        
        public bool _showValue;
        
        public Vector2 KnobSize
        {
            get { return _knobSize; }
            set { _knobSize = value; }
        }

        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public float MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        public float MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public Color ActiveColor
        {
            get { return _activeColor; }
            set { _activeColor = value; }
        }

        public bool ShowValue
        {
            get { return _showValue; }
            set { _showValue = value; }
        }

        protected override void OnDraw()
        {
            float value = EditorGUILayout.Knob(KnobSize, Value, MinValue, MaxValue, Unit, BackgroundColor, ActiveColor, ShowValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }


    public class ELabelField : BText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.label;
        }

        protected override void OnDraw()
        {
            EditorGUILayout.LabelField(Content, Style, Option.Values);
        }
    }

    public class ELayerField : Widget
    {
        private int _value;
        public int Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.LayerField(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class ELongField : Widget
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        private long _value;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public long Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            long value = EditorGUILayout.LongField(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EMaskField : Widget
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        private int _mask;
        /// <summary>
        /// 显示项
        /// </summary>
        private string[] _displayedOptions;

        /// <summary>
        /// 当前选中项
        /// </summary>
        public int Mask
        {
            get { return _mask; }

            set { _mask = value; }
        }
        /// <summary>
        /// 显示项
        /// </summary>
        public string[] DisplayedOptions
        {
            get { return _displayedOptions; }

            set { _displayedOptions = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.MaskField(Content, Mask, DisplayedOptions,Option.Values);
            if (value != Mask)
            {
                Mask = value;
                this.DipatchEvent();
            }
        }
    }

    public class EMinMaxSlider : Widget
    {
        /// <summary>
        /// 最小限定值
        /// </summary>
        private float _minLimit;
        /// <summary>
        /// 最大限定值
        /// </summary>
        private float _maxLimit;
        /// <summary>
        /// 最小值
        /// </summary>
        private float _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private float _maxValue = 100;

        /// <summary>
        /// 最小限定值
        /// </summary>
        public float MinLimit
        {
            get { return _minLimit; }
            set { _minLimit = value; }
        }
        /// <summary>
        /// 最大限定值
        /// </summary>
        public float MaxLimit
        {
            get { return _maxLimit; }
            set { _maxLimit = value; }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        protected override void OnDraw()
        {
            EditorGUILayout.MinMaxSlider(Content, ref _minValue, ref _maxValue, MinLimit, MaxLimit, Option.Values);
        }
    }


    public class EObjectField : Widget
    {
        private UnityEngine.Object _target;
        private Type _targetType;
        private bool _allowSceneObjects;

        public UnityEngine.Object Target
        {
            get { return _target; }

            set { _target = value; }
        }

        public Type TargetType
        {
            get { return _targetType; }

            set { _targetType = value; }
        }

        public bool AllowSceneObjects
        {
            get { return _allowSceneObjects; }

            set { _allowSceneObjects = value; }
        }

        public EObjectField()
        {
            AllowSceneObjects = true;
        }

        protected override void OnDraw()
        {
            UnityEngine.Object obj = EditorGUILayout.ObjectField(Content, Target, TargetType, AllowSceneObjects, Option.Values);
            if (obj != Target)
            {
                Target = obj;
                this.DipatchEvent();
            }
        }
    }


    public class EPasswordField : Widget
    {
        private string _password;

        public string Password
        {
            get { return _password; }

            set { _password = value; }
        }

        protected override void OnDraw()
        {
            string value = EditorGUILayout.PasswordField(Content, Password,Option.Values);
            if (value != Password)
            {
                Password = value;
                this.DipatchEvent();
            }
        }
    }


    public class EPopup : Widget
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        private int _selectedIndex;
        /// <summary>
        /// 显示项
        /// </summary>
        private GUIContent[] _displayedOptions;
        /// <summary>
        /// 当前选中项
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }

            set { _selectedIndex = value; }
        }
        /// <summary>
        /// 显示项
        /// </summary>
        public GUIContent[] DisplayedOptions
        {
            get { return _displayedOptions; }

            set { _displayedOptions = value; }
        }

        protected override void OnDraw()
        {
            int value = EditorGUILayout.Popup(Content, SelectedIndex, DisplayedOptions, Option.Values);
            if (value != SelectedIndex)
            {
                SelectedIndex = value;
                this.DipatchEvent();
            }
        }
    }

    public class EPrefixLabel : Widget
    {
        public EPrefixLabel()
        {
        }

        protected override void OnDraw()
        {
            EditorGUILayout.PrefixLabel(Content);
        }
    }


    /// <summary>
    /// 图片预览
    /// </summary>
    public class EPreviewTexture : Widget
    {
        private Rect _position;
        private Texture _image;
        private Material _mat;
        
        private ScaleMode _scaleMode;
        private float _imageAspect;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public UnityEngine.Material Mat
        {
            get { return _mat; }
            set { _mat = value; }
        }

        public UnityEngine.ScaleMode ScaleMode
        {
            get { return _scaleMode; }
            set { _scaleMode = value; }
        }

        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }

        public EPreviewTexture()
        {
            ScaleMode = UnityEngine.ScaleMode.ScaleToFit;
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawPreviewTexture(Position, Image, Mat, ScaleMode, ImageAspect);
        }
    }

    public class ETextureAlpha : Widget
    {
        private Rect _position;
        private Texture _image;
        private ScaleMode _scaleMode;
        private float _imageAspect;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public UnityEngine.ScaleMode ScaleMode
        {
            get { return _scaleMode; }
            set { _scaleMode = value; }
        }

        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawTextureAlpha(Position, Image, ScaleMode, ImageAspect);
        }
    }

    public class ETextureTransparent : Widget
    {
        private Rect _position;
        private Texture _image;
        private ScaleMode _scaleMode;
        private float _imageAspect;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public UnityEngine.ScaleMode ScaleMode
        {
            get { return _scaleMode; }
            set { _scaleMode = value; }
        }

        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawTextureTransparent(Position, Image, ScaleMode, ImageAspect);
        }
    }


    public class EPropertyField : Widget
    {
        private SerializedProperty _value;
        private bool _includeChildren;

        public SerializedProperty Value
        {
            get { return _value; }

            set { _value = value; }
        }

        public bool IncludeChildren
        {
            get { return _includeChildren; }

            set { _includeChildren = value; }
        }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.PropertyField(Value, Content, IncludeChildren, Option.Values);
            if (value != IncludeChildren)
            {
                IncludeChildren = value;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 绘制矩形
    /// </summary>
    public class ERect : Widget
    {
        /// <summary>
        /// 矩形位置和大小
        /// </summary>
        private Rect _rect;
        /// <summary>
        /// 颜色
        /// </summary>
        private Color _color;

        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        /// <summary>
        /// 矩形位置和大小
        /// </summary>
        public Rect Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public ERect()
        {
            _color = Color.gray;
            _rect = new Rect();
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawRect(Rect, Color);
        }
    }

    public class ERectField : Widget
    {
        private Rect _value;

        public Rect Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Rect value = EditorGUILayout.RectField(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class ESelectableLabel : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.SelectableLabel(Text, Option.Values);
        }
    }







}
