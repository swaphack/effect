using UnityEngine;

namespace Game.Mathematics.Geometry2D
{
    /// <summary>
    /// 平面多边形
    /// </summary>
    public struct Polygon2D
    {
        public Vector2[] Points { get; }

        public Polygon2D(Vector2[] points)
        {
            Points = points;
        }

        public bool IsValid()
        {
            if (Points == null || Points.Length <= 3)
            {
                return false;
            }

            return true;
        }

        public bool Contains(Vector2 point)
        {
            return false;
        }        
    }
}
