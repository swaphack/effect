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

        /// <summary>
        /// 是否是标准多边形
        /// 无交叉线
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (Points == null || Points.Length <= 3)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 是否包含点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Vector2 point)
        {
            int ret = Utility2D.GetPointPosition(point, Points);
            return ret <= 0;
        }        
    }
}
