using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
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
}
