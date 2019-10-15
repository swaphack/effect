using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    class EPopup : Widget
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
}
