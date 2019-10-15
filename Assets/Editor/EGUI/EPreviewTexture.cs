using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    /// <summary>
    /// 图片预览
    /// </summary>
    public class EPreviewTexture : Widget
    {
        private Rect _position;
        private Texture _image;
        private Material _mat;
        
        private ScaleMode _scaleMode;
        private float _imageAspect;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public UnityEngine.Material Mat
        {
            get { return _mat; }
            set { _mat = value; }
        }

        public UnityEngine.ScaleMode ScaleMode
        {
            get { return _scaleMode; }
            set { _scaleMode = value; }
        }

        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }

        public EPreviewTexture()
        {
            ScaleMode = UnityEngine.ScaleMode.ScaleToFit;
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawPreviewTexture(Position, Image, Mat, ScaleMode, ImageAspect);
        }
    }

    public class ETextureAlpha : Widget
    {
        private Rect _position;
        private Texture _image;
        private ScaleMode _scaleMode;
        private float _imageAspect;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public UnityEngine.ScaleMode ScaleMode
        {
            get { return _scaleMode; }
            set { _scaleMode = value; }
        }

        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawTextureAlpha(Position, Image, ScaleMode, ImageAspect);
        }
    }

    public class ETextureTransparent : Widget
    {
        private Rect _position;
        private Texture _image;
        private ScaleMode _scaleMode;
        private float _imageAspect;

        public UnityEngine.Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public UnityEngine.Texture Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public UnityEngine.ScaleMode ScaleMode
        {
            get { return _scaleMode; }
            set { _scaleMode = value; }
        }

        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }

        protected override void OnDraw()
        {
            EditorGUI.DrawTextureTransparent(Position, Image, ScaleMode, ImageAspect);
        }
    }
}
