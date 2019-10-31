using System;
using UnityEngine;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// 空白控件
    /// </summary>
    public class GUISpace : IWidget
    {
        public float Pixel { get; set; }

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
    public class GUIFlexibleSpace : IWidget
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
    public class GUIBox : Widget
    {
        protected override void OnDraw()
        {
            GUILayout.Box(Content,Option.Values);
        }
    }

    /// <summary>
    /// 按钮
    /// </summary>
    public class GUIButton : Widget
    {
        protected override void BeginDraw()
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
    public abstract class GUIText : Widget
    {
        /// <summary>
        /// 最大字符长度
        /// </summary>
        public const int TextMaxLength = 255;

        /// <summary>
        /// 文本颜色
        /// </summary>
        private Color _color;


        /// <summary>
        /// 对齐方式
        /// </summary>
        public TextAnchor Alignment { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 字体风格
        /// </summary>
        public FontStyle FontStyle { get; set; }

        /// <summary>
        /// 是否富文本
        /// </summary>
        public bool RichText { get; set; }

        /// <summary>
        /// 是否自动对其
        /// </summary>
        public bool WordWrap { get; set; }

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
        public TextClipping Clipping { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public string Value { set => Text = value; get => Text; } 

        public GUIText()
        {
            Alignment = TextAnchor.MiddleLeft;
            RichText = false;
            Color = Color.black;
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
    public class GUILabel : GUIText
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
    public class GUIPasswordField : GUIText
    {
        /// <summary>
        /// 掩码
        /// </summary>
        public char MaskChar { get; set; } = '*';
        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength { get; set; } = TextMaxLength;

        protected override void InitStyle()
        {
            Style = GUI.skin.textField;
        }

        protected override void OnDraw()
        {
            string text = GUILayout.PasswordField(Text, MaskChar, MaxLength, Style, Option.Values);
            if (text != Value)
            {
                Value = text;
                this.DipatchEvent();
            }
        }
    }


    /// <summary>
    /// 文本区域
    /// </summary>
    public class GUITextArea : GUIText
    {
        /// <summary>
        /// 文本最大长度
        /// </summary>
        public int MaxLength { get; set; } = TextMaxLength;

        protected override void InitStyle()
        {
            Style = GUI.skin.textArea;
        }

        protected override void OnDraw()
        {
            string text = GUILayout.TextArea(Text, MaxLength, Style, Option.Values);
            if (text != Value)
            {
                Value = text;
                this.DipatchEvent();
            }
        }
    }

    public class GUITextField : GUIText
    {
        /// <summary>
        /// 文本最大长度
        /// </summary>
        public int MaxLength { get; set; } = TextMaxLength;

        protected override void InitStyle()
        {
            Style = GUI.skin.textField;
        }

        protected override void OnDraw()
        {
            string text = GUILayout.TextArea(Text, MaxLength, Style, Option.Values);
            if (text != Value)
            {
                Value = text;
                this.DipatchEvent();
            }
        }
    }

    /// <summary>
    /// 按住后会重复执行单击操作的按钮
    /// </summary>
    public class GUIRepeatButton : GUIButton
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
    public abstract class GUIScrollbar : Widget
    {
        /// <summary>
        /// 当前值
        /// </summary>
        public float Value { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue { get; set; } = 100;
        /// <summary>
        /// 大小
        /// </summary>
        public float Size { get; set; }
    }
    /// <summary>
    /// 水平滑动条
    /// </summary>
    public class GUIHorizontalScrollbar : GUIScrollbar
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
    public class GUIVerticalScrollbar : GUIScrollbar
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
    public class GUISelectionGrid : Widget
    {
        /// <summary>
        /// 选中索引
        /// </summary>
        public int Selected { get; set; }

        /// <summary>
        /// 水平方向个数
        /// </summary>
        public int HorinzontalCount { get; set; }
        /// <summary>
        /// 各个项
        /// </summary>
        public GUIContent[] Contents { get; set; }

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
    public class GUISlider : Widget
    {

        /// <summary>
        /// 当前值
        /// </summary>
        public float Value { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue { get; set; } = 100;

        public GUIStyle Thumb { get; }

        public GUISlider()
        {
            Thumb = new GUIStyle();
        }
    }

    /// <summary>
    /// 水平滑杆
    /// </summary>
    public class GUIHorizontalSlider : GUISlider
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
    public class GUIVerticalSlider : GUISlider
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

    public class GUITexture : Widget
    {
        private Rect _position;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image { get; set; }

        public UnityEngine.ScaleMode ScaleMode { get; set; }

        public bool AlphaBlend { get; set; }

        public float ImageAspect { get; set; }
        protected override void OnDraw()
        {
            GUI.DrawTexture(Position, Image, ScaleMode, AlphaBlend, ImageAspect);
        }
    }

    public class GUITextureWithTexCoords : Widget
    {
        private Rect _position;
        private Rect _texCoords;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image { get; set; }

        public UnityEngine.Rect TexCoords
        {
            get { return _texCoords; }
            set { _texCoords = value; }
        }

        public bool AlphaBlend { get; set; }

        protected override void OnDraw()
        {
            GUI.DrawTextureWithTexCoords(Position, Image, TexCoords, AlphaBlend);
        }
    }

    public class GUIToggle : Widget
    {
        public bool Value { get; set; }
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


    public class GUIToolbar : Widget
    {
        /// <summary>
        /// 选中索引
        /// </summary>
        public int Selected { get; set; }
        /// <summary>
        /// 各个项
        /// </summary>
        public GUIContent[] Contents { get; set; }
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
    public class GUIWindow : Widget
    {
        /// <summary>
        /// 位置
        /// </summary>
        private Rect _clientRect;

        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public Rect NetClientRect
        {
            get { return _clientRect; }
            set { _clientRect = value; }
        }
        /// <summary>
        /// 触发事件
        /// </summary>
        public UnityEngine.GUI.WindowFunction Func { get; set; }

        protected override void OnDraw()
        {
            Rect rect = GUILayout.Window(ID, NetClientRect, Func, Content, Option.Values);
            if (rect != NetClientRect)
            {
                NetClientRect = rect;
                this.DipatchEvent();
            }
        }
    }
}
