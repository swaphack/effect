using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 区域
    /// </summary>
    public class BArea : Layout
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

    public class BScrollView : Layout
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

        /// <summary>
        /// 水平滑条方式
        /// </summary>
        private GUIStyle _horizontalScrollbar;
        /// <summary>
        /// 垂直滑条方式
        /// </summary>
        private GUIStyle _verticalScrollbar;

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

        private GUIStyle HorizontalScrollbar
        {
            get { return _horizontalScrollbar; }
        }
        private GUIStyle VerticalScrollbar
        {
            get { return _verticalScrollbar; }
        }

        public BScrollView()
        {
            _horizontalScrollbar = new GUIStyle();
            _verticalScrollbar = new GUIStyle();
        }

        protected override void BeginDraw()
        {
            Vector2 pos = GUILayout.BeginScrollView(ScrollPosition, AlwaysShowHorizontal, AlwaysShowVertical, HorizontalScrollbar, VerticalScrollbar, Option.Values);
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
