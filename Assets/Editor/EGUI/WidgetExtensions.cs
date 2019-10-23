using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{

    /// <summary>
    /// 空白控件
    /// </summary>
    public class BSpace : IWidget
    {
        private float _pixels;

        public float Pixel{
            get {
                return _pixels;
            }
            set {
                _pixels = value;
            }
        }

        public void Draw()
        {
            this.BeginDraw();
            this.OnDraw();
            this.EndDraw();
        }

        public void BeginDraw()
        {
        }

        public void OnDraw()
        {
            GUILayout.Space(Pixel);
        }

        public void EndDraw()
        {
        }
    }

    /// <summary>
    /// 自适应的空白控件
    /// </summary>
    public class FlexibleSpace : IWidget
    {
        public void Draw()
        {
            this.BeginDraw();
            this.OnDraw();
            this.EndDraw();
        }

        public void BeginDraw()
        {
        }

        public void OnDraw()
        {
            GUILayout.FlexibleSpace();
        }

        public void EndDraw()
        {
        }
    }

    /// <summary>
    /// 盒子
    /// </summary>
    public class BBox : Widget
    {
        protected override void OnDraw()
        {
            GUILayout.Box(Content,Option.Values);
        }
    }

    /// <summary>
    /// 按钮
    /// </summary>
    public class BButton : Widget
    {
        public BButton()
        {
            
        }

        protected override void OnDraw()
        {
            if (GUILayout.Button(Content,Option.Values))
            {
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 文本
    /// </summary>
    public abstract class BText : Widget
    {
        /// <summary>
        /// 最大字符长度
        /// </summary>
        public const int TextMaxLength = 255;
        /// <summary>
        /// 对齐方式
        /// </summary>
        private TextAnchor _alignment;
        /// <summary>
        /// 字体
        /// </summary>
        private Font _font;
        /// <summary>
        /// 字体风格
        /// </summary>
        private FontStyle _fontStyle;
        /// <summary>
        /// 是否富文本
        /// </summary>
        private bool _richText;
        /// <summary>
        /// 是否自动对其
        /// </summary>
        private bool _wordWrap;
        /// <summary>
        /// 文本显示方式，当文本超出显示区域时，是否裁剪
        /// </summary>
        private TextClipping _clipping;
        /// <summary>
        /// 文本颜色
        /// </summary>
        private Color _color;
        /// <summary>
        /// 字体大小
        /// </summary>
        private int _fontSize;

        private GUIStyle _style;

        protected GUIStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// 对齐方式
        /// </summary>
        public TextAnchor Alignment 
        {
            get { return _alignment; }
            set { _alignment = value; }
        }

        /// <summary>
        /// 字体
        /// </summary>
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        /// <summary>
        /// 字体风格
        /// </summary>
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set { _fontStyle = value; }
        }

        /// <summary>
        /// 是否富文本
        /// </summary>
        public bool RichText
        {
            get { return _richText; }
            set { _richText = value; }
        }

        /// <summary>
        /// 是否自动对其
        /// </summary>
        public bool WordWrap
        {
            get { return _wordWrap; }
            set { _wordWrap = value; }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// 文本裁剪
        /// </summary>
        public TextClipping Clipping
        {
            get { return _clipping; }
            set { _clipping = value; }
        }
        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        public BText()
        {
            Alignment = TextAnchor.MiddleLeft;
            RichText = false;
            Color = Color.white;
            Clipping = UnityEngine.TextClipping.Overflow;
            FontSize = 14;
        }

        protected override void UpdateStyle()
        {
            Style.alignment = Alignment;
            Style.richText = RichText;
            Style.clipping = Clipping;
            Style.wordWrap = WordWrap;
            Style.fontStyle = FontStyle;
            if (Font != null)
            {
                Style.font = Font;
            }
            Style.fontSize = FontSize;
            Style.normal.textColor = Color;
        }
    }


    /// <summary>
    /// 文本
    /// </summary>
    public class BLabel : BText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.label;
        }

        protected override void OnDraw()
        {
            GUILayout.Label(Content, Style, Option.Values);
        }
    }

    /// <summary>
    /// 密码输入框
    /// </summary>
    public class BPasswordField : BText
    {
        /// <summary>
        /// 掩码
        /// </summary>
        private char _maskChar = '*';
        /// <summary>
        /// 最大长度
        /// </summary>
        private int _maxLength = TextMaxLength;
        /// <summary>
        /// 掩码
        /// </summary>
        public char MaskChar
        {
            get
            {
                return _maskChar;
            }
            set
            {
                _maskChar = value;
            }
        }
        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength
        {
            get
            {
                return _maxLength;
            }
            set
            {
                _maxLength = value;
            }
        }

        protected override void InitStyle()
        {
            Style = GUI.skin.textField;
        }

        protected override void OnDraw()
        {
            string text = GUILayout.PasswordField(Text, MaskChar, MaxLength, Style, Option.Values);
            if (text != Text)
            {
                Text = text;
                this.DipatchEvent();
            }
        }
    }


    /// <summary>
    /// 文本区域
    /// </summary>
    public class BTextArea : BText
    {
        /// <summary>
        /// 文本最大长度
        /// </summary>
        private int _maxLength = TextMaxLength;
        /// <summary>
        /// 文本最大长度
        /// </summary>
        public int MaxLength 
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        protected override void InitStyle()
        {
            Style = GUI.skin.textArea;
        }

        protected override void OnDraw()
        {
            string text = GUILayout.TextArea(Text, MaxLength, Style, Option.Values);
            if (text != Text)
            {
                Text = text;
                this.DipatchEvent();
            }
        }
    }

    public class BTextField : BText
    {
        /// <summary>
        /// 文本最大长度
        /// </summary>
        private int _maxLength = TextMaxLength;
        /// <summary>
        /// 文本最大长度
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        protected override void InitStyle()
        {
            Style = GUI.skin.textField;
        }

        protected override void OnDraw()
        {
            string text = GUILayout.TextArea(Text, MaxLength, Style, Option.Values);
            if (text != Text)
            {
                Text = text;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 按住后会重复执行单击操作的按钮
    /// </summary>
    public class BRepeatButton : BButton
    {
        protected override void OnDraw()
        {
            if (GUILayout.RepeatButton(Content,Option.Values))
            {
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 滑动条
    /// </summary>
    public abstract class BScrollbar : Widget
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private float _value;
        /// <summary>
        /// 最小值
        /// </summary>
        private float _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private float _maxValue = 100;
        /// <summary>
        /// 大小
        /// </summary>
        private float _size;
        /// <summary>
        /// 当前值
        /// </summary>
        public float Value
        {
            get { return _value; }
            set { _value = value; }
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
        /// <summary>
        /// 大小
        /// </summary>
        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }
    }
    /// <summary>
    /// 水平滑动条
    /// </summary>
    public class HorizontalScrollbar : BScrollbar
    {
        protected override void OnDraw()
        {
            float value = GUILayout.HorizontalScrollbar(Value, Size, MinValue, MaxValue,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 垂直滑动条
    /// </summary>
    public class VerticalScrollbar : BScrollbar
    {
        protected override void OnDraw()
        {
            float value = GUILayout.VerticalScrollbar(Value, Size, MinValue, MaxValue,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 选中格子
    /// </summary>
    public class BSelectionGrid : Widget
    {
        /// <summary>
        /// 选中索引
        /// </summary>
        private int _selected;
        /// <summary>
        /// 各个项
        /// </summary>
        private GUIContent[] _contents;
        /// <summary>
        /// 水平方向个数
        /// </summary>
        private int _horinzontalCount;
        /// <summary>
        /// 选中索引
        /// </summary>
        public int Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        /// <summary>
        /// 水平方向个数
        /// </summary>
        public int HorinzontalCount
        {
            get { return _horinzontalCount; }
            set { _horinzontalCount = value; }
        }
        /// <summary>
        /// 各个项
        /// </summary>
        public GUIContent[] Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }

        protected override void OnDraw()
        {
            int selected = GUILayout.SelectionGrid(Selected, Contents, HorinzontalCount,Option.Values);
            if (selected != Selected)
            {
                Selected = selected;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 滑杆条
    /// </summary>
    public class BSlider : Widget
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private float _value;
        /// <summary>
        /// 最小值
        /// </summary>
        private float _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private float _maxValue = 100;
        /// <summary>
        /// 滑杆
        /// </summary>
        private GUIStyle _thumb;

        /// <summary>
        /// 当前值
        /// </summary>
        public float Value
        {
            get { return _value; }
            set { _value = value; }
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

        public GUIStyle Thumb
        {
            get { return _thumb; }
        }

        public BSlider()
        {
            _thumb = new GUIStyle();
        }
    }

    /// <summary>
    /// 水平滑杆
    /// </summary>
    public class HorizontalSlider : BSlider
    {
        protected override void OnDraw()
        {
            float value = GUILayout.HorizontalSlider(Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 垂直滑杆
    /// </summary>
    public class VerticalSlider : BSlider
    {
        protected override void OnDraw()
        {
            float value = GUILayout.VerticalSlider(Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class BTexture : Widget
    {
        private Rect _position;
        private Texture _image;
        private ScaleMode _scaleMode;
        private bool _alphaBlend;
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
        
        public bool AlphaBlend
        {
            get { return _alphaBlend; }
            set { _alphaBlend = value; }
        }
        
        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }
        protected override void OnDraw()
        {
            GUI.DrawTexture(Position, Image, ScaleMode, AlphaBlend, ImageAspect);
        }
    }

    class BTextureWithTexCoords : Widget
    {
        private Rect _position;
        private Texture _image;
        private Rect _texCoords;
        
        private bool _alphaBlend;
        
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

        public UnityEngine.Rect TexCoords
        {
            get { return _texCoords; }
            set { _texCoords = value; }
        }

        public bool AlphaBlend
        {
            get { return _alphaBlend; }
            set { _alphaBlend = value; }
        }

        protected override void OnDraw()
        {
            GUI.DrawTextureWithTexCoords(Position, Image, TexCoords, AlphaBlend);
        }
    }

    public class BToggle : Widget
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        private bool _value;

        public bool Value
        {
            get { return _value; }
            set { _value = value; }
        }
        protected override void OnDraw()
        {
            bool value = GUILayout.Toggle(Value, Content,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }


    public class BToolbar : Widget
    {
        /// <summary>
        /// 选中索引
        /// </summary>
        private int _selected;
        /// <summary>
        /// 各个项
        /// </summary>
        private GUIContent[] _contents;
        /// <summary>
        /// 选中索引
        /// </summary>
        public int Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
        /// <summary>
        /// 各个项
        /// </summary>
        public GUIContent[] Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }
        protected override void OnDraw()
        {
            int selected = GUILayout.Toolbar(Selected, Contents,Option.Values);
            if (selected != Selected)
            {
                Selected = selected;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 窗口
    /// </summary>
    public class BWindow : Widget
    {
        /// <summary>
        /// 编号
        /// </summary>
        private int _id;
        /// <summary>
        /// 位置
        /// </summary>
        private Rect _clientRect;
        /// <summary>
        /// 触发事件
        /// </summary>
        private UnityEngine.GUI.WindowFunction _func;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public Rect ClientRect
        {
            get { return _clientRect; }
            set { _clientRect = value; }
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        public UnityEngine.GUI.WindowFunction Func
        {
            get { return _func; }
            set { _func = value; }
        }

        protected override void OnDraw()
        {
            Rect rect = GUILayout.Window(ID, ClientRect, Func, Content, Option.Values);
            if (rect != ClientRect)
            {
                ClientRect = rect;
                this.DipatchEvent();
            }
        }
    }
}
