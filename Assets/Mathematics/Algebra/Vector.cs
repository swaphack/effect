using System;

namespace Game.Mathematics.Algebra
{
    /// <summary>
    /// 向量
    /// </summary>
    public struct Vector
    {
        /// <summary>
        /// 值
        /// </summary>
        public float[] Value { get; }

        public Vector(float[] value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public float this[int i]
        {
            get
            {
                if (i < 0 || i >= Value.Length)
                {
                    throw new System.Exception("index is out of range");
                }

                return Value[i];
            }
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValid(float[] value)
        {
            if (value == null || value.Length == 0)
            {
                return false;
            }

            return true;
        }
        

        /// <summary>
        /// 点积
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Dot(Vector v1, Vector v2)
        {
            if (v1.Value.Length != v2.Value.Length)
            {
                throw new Exception("Length of two vector is not equal");
            }
            float value = 0;
            for (var i = 0; i < v1.Value.Length; i++)
            {
                value += v1[i] * v2[i];
            }

            return value;
        }
    }
}
