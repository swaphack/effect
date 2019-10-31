using Assets.Editor.Widgets;
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
            _color = Handles.color;
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

        public Rect Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Camera Camera { get; set; }

        public DrawCameraMode DrawMode { get; set; }

        public SceneCamera()
        {
            Camera = Camera.main;
            DrawMode = DrawCameraMode.Normal;
        }

        public void Set(Rect position, Camera camera)
        {
            _position = position;
            Camera = camera;

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
