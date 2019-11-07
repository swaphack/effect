using System;
using UnityEngine;

namespace Game.Mathematics.Geometry3D
{
    /// <summary>
    /// 射线
    /// </summary>
    public struct Ray
    {
        /// <summary>
        /// 起点
        /// </summary>
        public Vector3 Orgin { get; private set; }
        /// <summary>
        /// 方向
        /// </summary>
        public Vector3 Direction { get; private set; }

        public Ray(Vector3 orgin, Vector3 direction)
        {
            Orgin = orgin;
            Direction = direction;
        }

        /// <summary>
        /// 是否包含点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Vector3 point)
        {
            if (Orgin == point)
            {
                return true;
            }

            return Utility3D.IsSameDirection(Direction, point - Orgin);
        }

        /// <summary>
        /// 是否同向
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public bool IsSameDirection(Ray ray)
        {
            return Utility3D.IsSameDirection(Direction, ray.Direction);
        }

        /// <summary>
        /// 是否平行
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public bool IsParallel(Ray ray)
        {
            return Utility3D.IsParallel(Direction, ray.Direction);
        }

        /// <summary>
        /// 是否相交
        /// 1.如果方向向量成比例,直线平行
        /// 2.如果不平行,方向向量叉乘,然后取两直线上各一点,构成的向量和前面叉乘的结果点乘.如果点乘结果是0,则相交,否则不相交
        /// 
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public bool Intersect(Ray ray)
        {
            if (IsParallel(ray))
            { // 平行
                if (ray.Contains(Orgin)) return true;
                Ray ray1 = new Ray(ray.Orgin, -ray.Direction);
                if (ray1.Contains(Orgin)) return true;
            }

            var crossDirection = Vector3.Cross(Direction, ray.Direction);

            var newDirection = ray.Orgin - Orgin;

            var value = Vector3.Dot(newDirection, crossDirection);

            return Mathf.Approximately(value, 0);
        }
    }
}
