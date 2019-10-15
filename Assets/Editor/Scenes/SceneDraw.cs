using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Scenes
{
    public class SceneDraw : SceneWidget
    {

    }
    public class AAConvexPolygon : SceneDraw
    {
        private Vector3[] _points;
        public Vector3[] Points
        {
            get { return _points; }
            set { _points = value; }
        }
        public override void Draw()
        {
            base.Draw();

            Handles.DrawAAConvexPolygon(Points);
        }
    }

    public class AAPolyLine : SceneDraw
    {
        private float _width;
        
        private Vector3[] _points;

        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public Vector3[] Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public AAPolyLine()
        {
            _width = 1;
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawAAPolyLine(Width, Points);
        }
    }

    public class DottedLine : SceneDraw
    {
        private Vector3 _point1;
        private Vector3 _point2;
        private float _screenSpaceSize;

        public Vector3 Point1
        {
            get { return _point1; }
            set { _point1 = value; }
        }
        
        public Vector3 Point2
        {
            get { return _point2; }
            set { _point2 = value; }
        }
        
        public float ScreenSpaceSize
        {
            get { return _screenSpaceSize; }
            set { _screenSpaceSize = value; }
        }

        public DottedLine()
        {
            _screenSpaceSize = 1;
            _point1 = Vector3.zero;
            _point2 = Vector3.zero;
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawDottedLine(Point1, Point2, ScreenSpaceSize);
        }
    }

    public class DottedLines : SceneDraw
    {
        private Vector3[] _lineSegments;
        private float _screenSpaceSize;

        public Vector3[] LineSegments
        {
            get { return _lineSegments; }
            set { _lineSegments = value; }
        }

        public float ScreenSpaceSize
        {
            get { return _screenSpaceSize; }
            set { _screenSpaceSize = value; }
        }

        public DottedLines()
        {
            _screenSpaceSize = 1;
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawDottedLines(LineSegments, ScreenSpaceSize);
        }
    }

    public class Line : SceneDraw
    {
        private Vector3 _point1;
        private Vector3 _point2;
        private float _screenSpaceSize;

        public Vector3 Point1
        {
            get { return _point1; }
            set { _point1 = value; }
        }

        public Vector3 Point2
        {
            get { return _point2; }
            set { _point2 = value; }
        }

        public float ScreenSpaceSize
        {
            get { return _screenSpaceSize; }
            set { _screenSpaceSize = value; }
        }

        public Line()
        {
            _screenSpaceSize = 1;
            _point1 = Vector3.zero;
            _point2 = Vector3.zero;
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawLine(Point1, Point2);
        }
    }

    public class Lines : SceneDraw
    {
        private Vector3[] _lineSegments;
        private float _screenSpaceSize;

        public Vector3[] LineSegments
        {
            get { return _lineSegments; }
            set { _lineSegments = value; }
        }

        public float ScreenSpaceSize
        {
            get { return _screenSpaceSize; }
            set { _screenSpaceSize = value; }
        }

        public Lines()
        {
            _screenSpaceSize = 1;
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawLines(LineSegments);
        }
    }

    public class PolyLine : SceneDraw
    {
        private Vector3[] _points;

        public Vector3[] Points
        {
            get { return _points; }
            set { _points = value; }
        }

        public PolyLine()
        {
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawPolyLine(Points);
        }
    }

    /// <summary>
    /// 中心
    /// </summary>
    public abstract class CenterDraw : SceneDraw
    {
        private Vector3 _center;

        public Vector3 Center
        {
            get { return _center; }
            set { _center = value; }
        }
    }

    /// <summary>
    ///  描边
    /// </summary>
    public abstract class EdgeDraw : CenterDraw
    {
        private Vector3 _normal;
        private float _radius;
        
        public Vector3 Normal
        {
            get { return _normal; }
            set { _normal = value; }
        }
        
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }
        public EdgeDraw()
        {
        }
    }

    public class SolidArc : EdgeDraw
    {
        private Vector3 _from;
        private float _angle;

        public Vector3 From
        {
            get { return _from; }
            set { _from = value; }
        }
        
        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawSolidArc(Center, Normal, From, Angle, Radius);
        }
    }

    public class SolidDisc : EdgeDraw
    {
        public override void Draw()
        {
            base.Draw();

            Handles.DrawSolidDisc(Center, Normal, Radius);
        }
    }

    public class WireArc : EdgeDraw
    {
        private Vector3 _from;
        private float _angle;

        public Vector3 From
        {
            get { return _from; }
            set { _from = value; }
        }

        public float Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public override void Draw()
        {
            base.Draw();

            Handles.DrawWireArc(Center, Normal, From, Angle, Radius);
        }
    }


    public class WireDisc : EdgeDraw
    {
        public override void Draw()
        {
            base.Draw();

            Handles.DrawWireDisc(Center, Normal, Radius);
        }
    }

    public class WireCube : CenterDraw
    {
        private Vector3 _size;
        public Vector3 Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public override void Draw()
        {
            base.Draw();

            Handles.DrawWireCube(Center, Size);
        }
    }
}
