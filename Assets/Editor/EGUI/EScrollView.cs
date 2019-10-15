using System;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EScrollView : Layout
    {
        /// <summary>
        /// 位置
        /// </summary>
        private Vector2 _scrollPosition;
        /// <summary>
        /// 是否总显示水平滑条
        /// </summary>
        private bool _alwaysShowHorizontal;
        /// <summary>
        /// 是否总显示垂直滑条
        /// </summary>
        private bool _alwaysShowVertical;

        private Vector2 ScrollPosition
        {
            get { return _scrollPosition; }
            set { _scrollPosition = value; }
        }
        private bool AlwaysShowHorizontal
        {
            get { return _alwaysShowHorizontal; }
            set { _alwaysShowHorizontal = value; }
        }
        private bool AlwaysShowVertical
        {
            get { return _alwaysShowVertical; }
            set { _alwaysShowVertical = value; }
        }

        public EScrollView()
        {
        }

        protected override void BeginDraw()
        {
            Vector2 pos = EditorGUILayout.BeginScrollView(ScrollPosition, AlwaysShowHorizontal, AlwaysShowVertical,Option.Values);
            if (pos != ScrollPosition)
            {
                ScrollPosition = pos;
                this.DipatchEvent();
            }
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndScrollView();
        }
    }
}
