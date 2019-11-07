using System;

namespace Game.Mathematics.Algebra
{
    /// <summary>
    /// 矩阵
    /// </summary>
    public struct Matrix
    {
        /// <summary>
        /// 数据
        /// </summary>
        public float[] Value { get; }
        /// <summary>
        /// 行
        /// </summary>
        public int Row { get; }
        /// <summary>
        /// 列
        /// </summary>
        public int Column { get; }


        public Matrix(float[] value, int row, int column)
        {
            Value = value;
            Row = row;
            Column = column;
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public float this[int row, int column]
        {
            get
            {
                if (row >= Row || column >= Column)
                {
                    throw new Exception("row or column is out of range");
                }

                return Value[row * Column + column];
            }
        }

        /// <summary>
        /// 获取列向量
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Vector GetColumn(int column)
        {
            if (column <= 0 || column >= Column)
            {
                throw new Exception("column is out of range");
            }

            float[] value = new float[Row];
            for (int i = 0; i < Row; i++)
            {
                value[i] = this[i, column];
            }

            return new Vector(value);
        }

        /// <summary>
        /// 获取行向量
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public Vector GetRow(int row)
        {
            if (row <= 0 || row >= Row)
            {
                throw new System.Exception("column is out of range");
            }

            float[] value = new float[Column];
            for (var i = 0; i < Column; i++)
            {
                value[i] = this[row, i];
            }

            return new Vector(value);
        }

        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="value"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static bool IsValid(float[] value, int row, int column)
        {
            if (value == null || value.Length == 0)
            {
                return false;
            }
            if (row <= 0 || column <= 0)
            {
                return false;
            }

            if (value.Length != row * column)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 两矩阵相乘
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix Product(Matrix m1, Matrix m2)
        {
            if (m1.Column != m2.Row)
            {
                throw new Exception("Column and Target Row is not Match");
            }

            var row = m1.Row;
            var column = m2.Column;

            float[] value = new float[row * column];

            for (var i = 0; i < row; i++)
            {
                for (var j = 0; i < column; j++)
                {
                    value[i * column + j] = Vector.Dot(m1.GetRow(i), m2.GetColumn(j));
                }
            }

            return new Matrix(value, row, column);
        }

        /// <summary>
        /// 矩阵乘常数
        /// </summary>
        /// <param name="m"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Matrix Mul(Matrix m, float k)
        {
            var row = m.Row;
            var column = m.Column;

            float[] value = new float[row * column];

            for (var i = 0; i < row; i++)
            {
                for (var j = 0; i < column; j++)
                {
                    value[i * column + j] = m[i, j] * k;
                }
            }

            return new Matrix(value, row, column);
        }

        /// <summary>
        /// 两矩阵相加
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix Add(Matrix m1, Matrix m2)
        {
            if (m1.Column != m2.Column || m1.Row != m2.Row)
            {
                throw new Exception("Column and Target Row is not Match");
            }

            int row = m1.Row;
            int column = m1.Column;
            float[] value = new float[row * column];

            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < column; j++)
                {
                    value[i * column + j] = m1[i, j] + m2[i, j];
                }
            }

            return new Matrix(value, row, column);
        }

        /// <summary>
        /// 两矩阵相减
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Matrix Sub(Matrix m1, Matrix m2)
        {
            if (m1.Column != m2.Column || m1.Row != m2.Row)
            {
                throw new Exception("Column and Target Row is not Match");
            }

            int row = m1.Row;
            int column = m1.Column;
            float[] value = new float[row * column];

            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < column; j++)
                {
                    value[i * column + j] = m1[i, j] - m2[i, j];
                }
            }

            return new Matrix(value, row, column);
        }

        /// <summary>
        /// 转置矩阵
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix Transposition(Matrix m)
        {
            int row = m.Column;
            int column = m.Row;
            float[] value = new float[row * column];

            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < column; j++)
                {
                    value[i * column + j] = m[j, i];
                }
            }

            return new Matrix(value, row, column);
        }
    }
}
