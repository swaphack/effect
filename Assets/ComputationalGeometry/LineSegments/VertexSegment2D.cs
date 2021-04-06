using System;
using Game.Mathematics.Geometry2D;
using UnityEngine;

namespace Game.ComputationalGeometry.LineSegments
{
    /// <summary>
    /// 顶点线段
    /// </summary>
    public class VertexSegment2D
    {
        /// <summary>
        /// 上端点
        /// </summary>
        public Vector2 Up { get; }
        /// <summary>
        /// 下端点
        /// </summary>
        public Vector2 Low { get; }
        /// <summary>
        /// 线段
        /// </summary>
        public LineSegment2D Line { get; }
        /// <summary>
        /// 是否水平线
        /// </summary>
        public bool IsHorizontalLine { get; }

        /// <summary>
        /// Vector2比较
        /// </summary>
        public static Comparison<Vector2> Vector2Compare = (x, y) =>
        {
            if (x.y > y.y) return -1;
            if (x.y < y.y) return 1;
            if (x.x < y.x) return -1;
            if (x.x > y.x) return 1;
            return 0;
        };

        /// <summary>
        /// LineSeg2D比较器
        /// </summary>
        public static Comparison<VertexSegment2D> LineSeg2DCompare = (x, y) =>
        {
            return Vector2Compare(x.Up, y.Up);
        };

        public VertexSegment2D(LineSegment2D line)
        {
            Line = line;

            IsHorizontalLine = Mathf.Approximately(Line.Src.x, line.Dest.x);
            int ret = Vector2Compare(line.Src, line.Dest);
            if (ret < 0)
            {
                Up = line.Src;
                Low = line.Dest;
            }
            else
            {
                Up = line.Dest;
                Low = line.Src;
            }
        }
    }
}
