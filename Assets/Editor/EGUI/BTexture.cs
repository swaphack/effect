using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class BTexture : Widget
    {
        private Rect _position;
        private Texture _image;
        private ScaleMode _scaleMode;
        private bool _alphaBlend;
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
        
        public bool AlphaBlend
        {
            get { return _alphaBlend; }
            set { _alphaBlend = value; }
        }
        
        public float ImageAspect
        {
            get { return _imageAspect; }
            set { _imageAspect = value; }
        }
        protected override void OnDraw()
        {
            GUI.DrawTexture(Position, Image, ScaleMode, AlphaBlend, ImageAspect);
        }
    }

    class BTextureWithTexCoords : Widget
    {
        private Rect _position;
        private Texture _image;
        private Rect _texCoords;
        
        private bool _alphaBlend;
        
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

        public UnityEngine.Rect TexCoords
        {
            get { return _texCoords; }
            set { _texCoords = value; }
        }

        public bool AlphaBlend
        {
            get { return _alphaBlend; }
            set { _alphaBlend = value; }
        }

        protected override void OnDraw()
        {
            GUI.DrawTextureWithTexCoords(Position, Image, TexCoords, AlphaBlend);
        }
    }
}
