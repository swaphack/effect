using System;
using UnityEngine;

namespace Game.Mathematics.Algebra
{
    /// <summary>
    /// 复数
    /// </summary>
    public struct Complex
    {
        /// <summary>
        /// 实部
        /// </summary>
        public float Real { get; }
        /// <summary>
        /// 虚部
        /// </summary>
        public float Imaginary { get; }
        /// <summary>
        /// 复数的模
        /// </summary>
        public float Module => Mathf.Sqrt(Mathf.Pow(Real, 2) + Mathf.Pow(Imaginary, 2));
        /// <summary>
        /// 共轭复数
        /// </summary>
        public Complex Conjugate => new Complex(Real, -Imaginary);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        public Complex(float real, float imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static Complex Add(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        public static Complex Sub(Complex c1, Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        public static Complex Mul(Complex c1, Complex c2)
        {
            return new Complex(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary, c1.Real * c2.Imaginary + c2.Real * c1.Imaginary);
        }
    }
    
}
