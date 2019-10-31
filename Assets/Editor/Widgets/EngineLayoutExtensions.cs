using System;
using UnityEngine;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// 区域
    /// </summary>
    public class GUIArea : Layout
    {
        private Rect _screenRect = new Rect();

        public Rect ScreenRect
        {
            get
            {
                return _screenRect;
            }
        }

        /// <summary>
        /// 开始绘制
        /// </summary>
        protected override void BeginDraw()
        {
            GUILayout.BeginArea(ScreenRect, Content);
        }

        /// <summary>
        /// 结束绘制
        /// </summary>
        protected override void EndDraw()
        {
            GUILayout.EndArea();
        }
    }

    public class GUIScrollView : Layout
    {
        /// <summary>
        /// 位置
        /// </summary>
        private Vector2 _scrollPosition;

        private Vector2 ScrollPosition
        {
            get { return _scrollPosition; }
            set { _scrollPosition = value; }
        }
        private bool AlwaysShowHorizontal { get; set; }
        private bool AlwaysShowVertical { get; set; }

        private GUIStyle GUIHorizontalScrollbar { get; }
        private GUIStyle GUIVerticalScrollbar { get; }

        public GUIScrollView()
        {
            GUIHorizontalScrollbar = new GUIStyle();
            GUIVerticalScrollbar = new GUIStyle();
        }

        protected override void BeginDraw()
        {
            Vector2 pos = GUILayout.BeginScrollView(ScrollPosition, AlwaysShowHorizontal, AlwaysShowVertical, GUIHorizontalScrollbar, GUIVerticalScrollbar, Option.Values);
            if (pos != ScrollPosition)
            {
                ScrollPosition = pos;
                this.DipatchEvent();
            }
        }

        protected override void EndDraw()
        {
            GUILayout.EndScrollView();
        }
    }
}
