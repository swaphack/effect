using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 绘制矩形
    /// </summary>
    public class ERect : Widget
    {
        /// <summary>
        /// 矩形位置和大小
        /// </summary>
        private Rect _rect;
        /// <summary>
        /// 颜色
        /// </summary>
        private Color _color;

        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        /// <summary>
        /// 矩形位置和大小
        /// </summary>
        public Rect Rect
        {
            get { return _rect; }
            set { _rect = value; }
        }

        public ERect()
        {
            _color = Color.gray;
            _rect = new Rect();
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawRect(Rect, Color);
        }
    }
}
