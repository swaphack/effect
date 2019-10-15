using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
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
}
