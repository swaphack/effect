using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
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
}
