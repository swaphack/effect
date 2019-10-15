using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
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
}
