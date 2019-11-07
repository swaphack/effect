using System;
using UnityEngine;

namespace Game.Mathematics.Geometry3D
{
    public static class Utility3D
    {
        /// <summary>
        /// 是否同向
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsSameDirection(Vector3 a, Vector3 b)
        {
            float value = Vector3.Dot(a.normalized, b.normalized);
            return Mathf.Approximately(value, 1);
        }

        /// <summary>
        /// 是否平行
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsParallel(Vector3 a, Vector3 b)
        {
            float value = Vector3.Dot(a.normalized, b.normalized);
            return Mathf.Approximately(value, 1) || Mathf.Approximately(value, -1);
        }
    }
}
