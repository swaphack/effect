
using UnityEditor;
using UnityEngine;


namespace Assets.Editor.EGUI
{
    public class EHorizontalLine : ERect
    {
        /// <summary>
        /// 填充
        /// </summary>
        private float _padding;
        /// <summary>
        /// 厚度
        /// </summary>
        private float _thickness;
        /// <summary>
        /// 填充
        /// </summary>
        public float Padding
        {
            get { return _padding; }
            set { _padding = value; }
        }
        /// <summary>
        /// 厚度
        /// </summary>
        public float Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        public EHorizontalLine()
        {
            _padding = 0;
            _thickness = 1;
        }

        protected override void BeginDraw()
        {
            var r = EditorGUILayout.GetControlRect(GUILayout.Height(Padding + Thickness));
            r.height = Thickness;
            r.x += Padding;
            r.width -= 2 * Padding;

            Rect = r;
        }
    }
}
