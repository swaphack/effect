using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
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
}
