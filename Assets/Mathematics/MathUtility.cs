using System.Collections.Generic;
using Game.Mathematics.Algebra;
using UnityEngine;

namespace Game.Mathematics
{
    public static class MathUtility
    {
        /// <summary>
        /// 是否在范围内
        /// </summary>
        /// <param name="a"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(float a, float min, float max)
        {
            return a >= min && a <= max;
        }

        /// <summary>
        /// 是否在范围内
        /// </summary>
        /// <param name="a"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(Vector2 a, Vector2 min, Vector2 max)
        {
            return IsInRange(a.x, min.x, max.x)
                && IsInRange(a.y, min.y, max.y);
        }

        /// <summary>
        /// 是否在范围内
        /// </summary>
        /// <param name="a"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(Vector3 a, Vector3 min, Vector3 max)
        {
            return IsInRange(a.x, min.x, max.x)
                && IsInRange(a.y, min.y, max.y)
                && IsInRange(a.z, min.z, max.z);
        }
    }
}
