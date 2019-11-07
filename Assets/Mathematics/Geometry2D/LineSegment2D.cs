using UnityEngine;

namespace Game.Mathematics.Geometry2D
{
    /// <summary>
    /// 线段
    /// </summary>
    public struct LineSegment2D
    {
        /// <summary>
        /// 起始点坐标
        /// </summary>
        public Vector2 Src { get; }
        /// <summary>
        /// 终点坐标
        /// </summary>
        public Vector2 Dest { get; }
        /// <summary>
        /// 方向
        /// </summary>
        public Vector2 Direction => Dest - Src;
        /// <summary>
        /// 包围盒
        /// </summary>
        public Rect Box { get; }

        public LineSegment2D(Vector2 src, Vector2 dest)
        {
            Src = src;
            Dest = dest;

            float minX = Mathf.Min(src.x, dest.x);
            float minY = Mathf.Min(src.y, dest.y);
            float maxX = Mathf.Max(src.x, dest.x);
            float maxY = Mathf.Max(src.x, dest.x);

            float width = maxX - minX;
            float height = maxY - minY;

            Box = new Rect(minX, minY, width, height);
        }

        /// <summary>
        /// 点是否在线上
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Vector2 point)
        {
            if (Src == point || Dest == point)
            {
                return true;
            }

            if (!MathUtility.IsInRange(point, Src, Dest))
            {
                return false;
            }

            return Utility2D.IsSameDirection(Direction, point - Src);
        }

        /// <summary>
        /// 是否平行
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool IsParallel(LineSegment2D line)
        {
            return Utility2D.IsParallel(Direction, line.Direction);
        }

        /// <summary>
        /// 是否相交
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool Intersect(LineSegment2D line)
        {
            int statusSrc = Utility2D.GetPointPosition(line.Src, Src, Dest);
            int statusDest = Utility2D.GetPointPosition(line.Dest, Src, Dest);
            if (statusSrc == 0 || statusDest == 0)
            {
                if (statusSrc == 0)
                {
                    return this.Contains(line.Src);
                }
                if(statusDest == 0)
                {
                    return this.Contains(line.Dest);
                }

                return false;
            }
            return statusSrc != statusDest;
        }

        /// <summary>
        /// 求两线段的交点
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public Vector2 IntersectPoint(LineSegment2D line)
        {
            Vector2 seg = line.Direction;

            var h1 = Utility2D.GetTriangleArea(Src, Dest, line.Src) / seg.magnitude;
            var h2 = Utility2D.GetTriangleArea(Src, Dest, line.Dest) / seg.magnitude;

            var k = h1 / (h1 + h2);

            return this.Direction * k;
        }
    }
}
