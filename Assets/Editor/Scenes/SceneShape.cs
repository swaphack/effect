

using UnityEditor;
using UnityEngine;
namespace Assets.Editor.Scenes
{
    public abstract class SceneShape: SceneWidget
    {
        private int _controlID;
        
        private Vector3 _position;
        
        private Quaternion _rotation;
        
        private float _size;
        
        private EventType _eventType;

        public int ControlID
        {
            get { return _controlID; }
            set { _controlID = value; }
        }

        public UnityEngine.Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public UnityEngine.Quaternion Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public UnityEngine.EventType EventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }

        public SceneShape(int controlID, Vector3 position, Quaternion rotation, float size = 1, EventType eventType = EventType.Repaint)
        {
            _controlID = controlID;
            _position = position;
            _rotation = rotation;
            _size = size;
            _eventType = eventType;

        }
    }

    /// <summary>
    /// 箭头
    /// </summary>
    public class ArrowShape: SceneShape
    {
        public ArrowShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.ArrowHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }

    /// <summary>
    /// 圆环
    /// </summary>
    public class CircleShape : SceneShape
    {
        public CircleShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.CircleHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }

    /// <summary>
    /// 锥形
    /// </summary>
    public class ConeShape : SceneShape
    {
        public ConeShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.ConeHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }

    /// <summary>
    /// 正方体
    /// </summary>
    public class CubeShape : SceneShape
    {
        public CubeShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.CubeHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }

    /// <summary>
    /// 圆柱
    /// </summary>
    public class CylinderShape : SceneShape
    {
        public CylinderShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.CylinderHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }

    /// <summary>
    /// 点
    /// </summary>
    public class DotShape : SceneShape
    {
        public DotShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DotHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }

    public class RectangleShape : SceneShape
    {
        public RectangleShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.RectangleHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }

    public class SelectionFrameShape : SceneShape
    {
        public SelectionFrameShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawSelectionFrame(ControlID, Position, Rotation, Size, EventType);
        }
    }

    public class SphereCapShape : SceneShape
    {
        public SphereCapShape(Vector3 position, Quaternion rotation)
            :base(0, position, rotation)
        { 
        }

        public override void Draw()
        {
            base.Draw();

            Handles.SphereHandleCap(ControlID, Position, Rotation, Size, EventType);
        }
    }
}
