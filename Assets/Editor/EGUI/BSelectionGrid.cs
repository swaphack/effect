using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
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
}
