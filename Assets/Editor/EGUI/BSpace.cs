using System;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 空白控件
    /// </summary>
    public class BSpace : IWidget
    {
        private float _pixels;

        public float Pixel{
            get {
                return _pixels;
            }
            set {
                _pixels = value;
            }
        }

        public void Draw()
        {
            this.BeginDraw();
            this.OnDraw();
            this.EndDraw();
        }

        public void BeginDraw()
        {
        }

        public void OnDraw()
        {
            GUILayout.Space(Pixel);
        }

        public void EndDraw()
        {
        }
    }

    /// <summary>
    /// 自适应的空白控件
    /// </summary>
    public class FlexibleSpace : IWidget
    {
        public void Draw()
        {
            this.BeginDraw();
            this.OnDraw();
            this.EndDraw();
        }

        public void BeginDraw()
        {
        }

        public void OnDraw()
        {
            GUILayout.FlexibleSpace();
        }

        public void EndDraw()
        {
        }
    }
}
