using System.Collections.Generic;
using UnityEngine;

namespace Assets.Foundation.Geometry
{
    /// <summary>
    /// 顶点 顺时针
    /// </summary>
    public class ShapeVertex
    {
        /// <summary>
        /// 顶点坐标
        /// </summary>
        private List<Vector3> _vertexes = new List<Vector3>();

        /// <summary>
        /// 添加顶点坐标
        /// </summary>
        /// <param name="point"></param>
        public void AddVertex(Vector3 point)
        {
            _vertexes.Add(point);
        }

        /// <summary>
        /// 移除顶点坐标
        /// </summary>
        /// <param name="point"></param>
        public void RemoveVertex(Vector3 point)
        {
            _vertexes.Remove(point);
        }

        /// <summary>
        /// 坐标转为数组
        /// </summary>
        /// <returns></returns>
        public Vector3[] ToArray()
        {
            return _vertexes.ToArray();
        }
    }
}
