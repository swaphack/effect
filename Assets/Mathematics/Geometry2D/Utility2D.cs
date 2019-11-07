using Game.Mathematics.Algebra;
using UnityEngine;

namespace Game.Mathematics.Geometry2D
{
    /// <summary>
    /// 2d工具
    /// </summary>
    public static class Utility2D
    {
        /// <summary>
        /// 是否同向
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsSameDirection(Vector2 a, Vector2 b)
        {
            float value = Vector2.Dot(a.normalized, b.normalized);
            return Mathf.Approximately(value, 1);
        }

        /// <summary>
        /// 是否平行
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsParallel(Vector2 a, Vector2 b)
        {
            float value = Vector2.Dot(a.normalized, b.normalized);
            return Mathf.Approximately(value, 1) || Mathf.Approximately(value, -1);
        }

        /// <summary>
        /// 在平面上，获取点与线的位置关系
        /// 返回结果：-1左边，0线上，1右边
        /// </summary>
        /// <param name="point"></param>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <returns>-1左边，0线上，1右边</returns>
        public static int GetPointPosition(Vector2 point, Vector2 src, Vector2 dest)
        {
            float[] value = {
                1, src.x, src.y,
                1, dest.x, dest.y,
                1, point.x, point.y
            };

            var det = new Detaminate(value, 3);
            if (det.Sum < 0)
            {
                return 1;
            }
            if (det.Sum > 0)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 三角形面积
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static float GetTriangleArea(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float[] value = {
                1, p1.x, p1.y,
                1, p2.x, p2.y,
                1, p3.x, p2.y
            };

            var det = new Detaminate(value, 3);
            return 0.5f * Mathf.Abs(det.Sum);
        }
    }
}
