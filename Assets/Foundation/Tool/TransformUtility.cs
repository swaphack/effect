using System;
namespace Game.Foundation.Tool
{
    public static class TransformUtility
    {
        /// <summary>
        /// 在区间内，包含min和max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool InRange(float value, float min, float max)
        {
            float temp = max;
            if (min > max)
            {
                min = max;
                max = temp;
            }

            return value >= min && value <= max;
        }

        /// <summary>
        /// 将角度标准化
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float GetNormalAngle(float angle)
        {
            while (angle < 0)
            {
                angle += 360;
            }

            while (angle > 360)
            {
                angle -= 360;
            }

            return angle;
        }
    }
}
