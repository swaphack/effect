using Assets.Editor.EGUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Editor.Scenes
{
    /// <summary>
    /// 场景控件
    /// </summary>
    public class SceneWidget : IWidget
    {
        /// <summary>
        /// 颜色
        /// </summary>
        private Color _color;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public SceneWidget()
        {
            Color = Handles.color;
        }

        public virtual void Draw()
        {
            Handles.color = Color;
        }
    }

    /// <summary>
    /// 摄像机
    /// </summary>
    public class SceneCamera : SceneWidget
    {
        private Rect _position;
        private Camera _camera;
        private DrawCameraMode _drawMode;

        public Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }
        
        public Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }
        
        public DrawCameraMode DrawMode
        {
            get { return _drawMode; }
            set { _drawMode = value; }
        }

        public SceneCamera()
        {
            _camera = Camera.main;
            _drawMode = DrawCameraMode.Normal;
        }

        public void Set(Rect position, Camera camera)
        {
            _position = position;
            _camera = camera;

            Handles.SetCamera(position, camera);
        }

        public void Clear()
        {
            Handles.ClearCamera(Position, Camera);
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawCamera(Position, Camera, DrawMode);
        }
    }
}
