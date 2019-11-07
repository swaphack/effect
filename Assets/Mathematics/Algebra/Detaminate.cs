using System;
using UnityEngine;

namespace Game.Mathematics.Algebra
{
    /// <summary>
    /// 行列式
    /// </summary>
    public struct Detaminate
    {
        /// <summary>
        /// 数值
        /// </summary>
        public float[] Value { get; }
        /// <summary>
        /// 阶数
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// 求和
        /// </summary>
<<<<<<< HEAD
        public float Sum { get; }

        public Detaminate(float[] value, int order)
        {
            Value = value;
            Order = order;
            Sum = 0;
            Sum = GetSum();
        }

        private float GetSum()
        {
            float value = 0;
            for (var i = 0; i < Order; i++)
            {
                float right = 1;
                float left = 1;

                int index = 0;
                while (index < Order)
                {
                    right *= this[index % Order, (i + index) % Order];
                    left *= this[index % Order, (i - index + Order) % Order];
                    index += 1;
                }
                value += right - left;
            }

            return value;
=======
        public float Sum
        {
            get
            {
                float value = 0;
                for (var i = 0; i < Order; i++)
                {
                    float right = 0;
                    float left = 0;

                    int index = 0;
                    while (index < Order)
                    {
                        right += this[index % Order, (i + index) % Order];
                        left += this[index % Order, (i - index + Order) % Order];
                        index += 1;
                    }
                    value += right - left;
                }

                return value;
            }
        }

        public Detaminate(float[] value, int order)
        {
            Value = value;
            Order = order;
>>>>>>> eca791581e64b360c5edaa8138c8ad2da80cf39b
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public float this[int i, int j]
        {
            get
            {
                if (i >= Order || j >= Order)
                {
                    throw new Exception("row or column is out of range");
                }

                return Value[i * Order + j];
            }
        }

        /// <summary>
        /// 是否是行列式
        /// </summary>
        /// <param name="value"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool IsValid(float[] value, int order)
        {
            if (value == null || value.Length == 0)
            {
                return false;
            }

            if (order <= 0)
            {
                return false;
            }

            return value.Length == (int)Mathf.Pow(order, 2);
        }
    }
}
