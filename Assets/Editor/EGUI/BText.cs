using UnityEngine;

namespace Assets.Editor.EGUI
{
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
}
