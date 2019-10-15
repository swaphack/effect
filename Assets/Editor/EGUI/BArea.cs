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
}
