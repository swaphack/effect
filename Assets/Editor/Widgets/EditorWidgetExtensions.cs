using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Widgets
{
    public class EditorBoundsField : Widget
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

        public EditorBoundsField()
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

    public class EditorColorField : Widget
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

        public EditorColorField()
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

    public class EditorCurveField : Widget
    {
        public AnimationCurve AnimationCurve { get; private set; }

        public EditorCurveField()
        {
            AnimationCurve = new AnimationCurve();
        }

        protected override void OnDraw()
        {
            AnimationCurve animationCurve = EditorGUILayout.CurveField(Content, AnimationCurve, Option.Values);
            if (animationCurve != AnimationCurve)
            {
                AnimationCurve = animationCurve;
                this.DipatchEvent();
            }
        }
    }

    public class EditorDelayedDoubleField : Widget
    {
        public double Value { get; set; }

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

    public class EditorDelayedFloatField : Widget
    {
        public float Value { get; set; }

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

    public class EditorDelayedIntField : Widget
    {
        public int Value { get; set; }

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

    public class EditorDelayedTextField : Widget
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

    public class EditorDoubleField : Widget
    {
        public double Value { get; set; }

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

    public class EditorDropdownButton : Widget
    {
        public FocusType Value { get; set; }

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

    public class EditorEnumPopup : Widget
    {
        public Enum Value { get; set; }

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

    public class EditorFloatField : Widget
    {
        public float Value { get; set; }

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

    public class EditorFoldout : Widget
    {
        public bool Foldout { get; set; }

        public bool ToggleOnLabelClick { get; set; }

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

    public class EditorHelpBox : Widget
    {
        public string Message { get; set; }

        public MessageType Type { get; set; }

        protected override void OnDraw()
        {
            EditorGUILayout.HelpBox(Message, Type);
        }
    }

    public class EditorHorizontalLine : EditorRect
    {
        /// <summary>
        /// 填充
        /// </summary>
        public float Padding { get; set; }
        /// <summary>
        /// 厚度
        /// </summary>
        public float Thickness { get; set; }

        public EditorHorizontalLine()
        {
            Padding = 0;
            Thickness = 1;
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

    public class EditorInspectorTitlebar : Widget
    {
        public bool Foldout { get; set; }

        public bool Expandable { get; set; }

        public UnityEngine.Object[] TargetObjs { get; set; }

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

    public class EditorIntField : Widget
    {
        public int Value { get; set; }

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

    public class EditorIntPopup : Widget
    {
        public class DisplayItem
        {
            public GUIContent item { get; set; }
            public int value { get; set; }

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

        private List<DisplayItem> _displayItems;
        /// <summary>
        /// 当前值
        /// </summary>
        public int Value { get; set; }

        public EditorIntPopup()
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

    public class EditorIntSlider : Widget
    {

        /// <summary>
        /// 当前值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public int MinValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxValue { get; set; } = 100;

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

    public class EditorKnob : Widget
    {
        private Vector2 _knobSize;
        private Color _backgroundColor;

        private Color _activeColor;

        public Vector2 KnobSize
        {
            get { return _knobSize; }
            set { _knobSize = value; }
        }

        public float Value { get; set; }

        public float MinValue { get; set; }

        public string Unit { get; set; }

        public float MaxValue { get; set; }

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

        public bool ShowValue { get; set; }

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


    public class EditorLabelField : GUIText
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

    public class EditorLayerField : Widget
    {
        public int Value { get; set; }

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

    public class EditorLongField : Widget
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        public long Value { get; set; }

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

    public class EditorMaskField : Widget
    {

        /// <summary>
        /// 当前选中项
        /// </summary>
        public int Mask { get; set; }
        /// <summary>
        /// 显示项
        /// </summary>
        public string[] DisplayedOptions { get; set; }

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

    public class EditorMinMaxSlider : Widget
    {
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
        public float MinLimit { get; set; }
        /// <summary>
        /// 最大限定值
        /// </summary>
        public float MaxLimit { get; set; }
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


    public class EditorObjectField : Widget
    {
        public UnityEngine.Object Target { get; set; }

        public Type TargetType { get; set; }

        public bool AllowSceneObjects { get; set; }

        public EditorObjectField()
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


    public class EditorPasswordField : Widget
    {
        public string Password { get; set; }

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


    public class EditorPopup : Widget
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        public int SelectedIndex { get; set; }
        /// <summary>
        /// 显示项
        /// </summary>
        public GUIContent[] DisplayedOptions { get; set; }

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

    public class EditorPrefixLabel : Widget
    {
        public EditorPrefixLabel()
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
    public class EditorPreviewTexture : Widget
    {
        private Rect _position;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image { get; set; }

        public UnityEngine.Material Mat { get; set; }

        public UnityEngine.ScaleMode ScaleMode { get; set; }

        public float ImageAspect { get; set; }

        public EditorPreviewTexture()
        {
            ScaleMode = UnityEngine.ScaleMode.ScaleToFit;
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawPreviewTexture(Position, Image, Mat, ScaleMode, ImageAspect);
        }
    }

    public class EditorTextureAlpha : Widget
    {
        private Rect _position;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image { get; set; }

        public UnityEngine.ScaleMode ScaleMode { get; set; }

        public float ImageAspect { get; set; }

        protected override void OnDraw()
        {
            EditorGUI.DrawTextureAlpha(Position, Image, ScaleMode, ImageAspect);
        }
    }

    public class EditorTextureTransparent : Widget
    {
        private Rect _position;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image { get; set; }

        public UnityEngine.ScaleMode ScaleMode { get; set; }

        public float ImageAspect { get; set; }

        protected override void OnDraw()
        {
            EditorGUI.DrawTextureTransparent(Position, Image, ScaleMode, ImageAspect);
        }
    }


    public class EditorPropertyField : Widget
    {
        public SerializedProperty Value { get; set; }

        public bool IncludeChildren { get; set; }

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
    public class EditorRect : Widget
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

        public EditorRect()
        {
            _color = Color.gray;
            _rect = new Rect();
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawRect(Rect, Color);
        }
    }

    public class EditorRectField : Widget
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

    public class EditorSelectableLabel : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.SelectableLabel(Text, Option.Values);
        }
    }

}
