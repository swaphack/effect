using System;
using System.Collections.Generic;
using Game.Mathematics.Geometry2D;
using UnityEngine;

namespace Game.ComputationalGeometry.Convex
{
    /// <summary>
    /// 凸包
    /// </summary>
    public class Convex2D
    {
        /// <summary>
        /// 获取凸多边形的顶点
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Vector2[] GetConvexHull(Vector2[] points)
        {
            if (points == null || points.Length < 3)
            {
                return null;
            }

            Array.Sort(points, (a, b) =>
            {
                int ret = a.x.CompareTo(b.x);
                if (ret == 0)
                {
                    return b.y.CompareTo(a.y);
                }

                return ret;
            });

            var upList = new List<Vector2>();
            upList.Add(points[0]);
            upList.Add(points[1]);

            for (var i = 2; i < points.Length; i++)
            {
                upList.Add(points[i]);
                while (upList.Count >= 3)
                {
                    var a = upList[upList.Count - 3];
                    var b = upList[upList.Count - 2];
                    var c = upList[upList.Count - 1];

                    if (Utility2D.GetPointPosition(c, a, b) != 1)
                    {
                        upList.Remove(b);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            var lowerList = new List<Vector2>();
            lowerList.Add(points[points.Length - 1]);
            lowerList.Add(points[points.Length - 2]);


            for (var i = points.Length - 3; i >= 0; i--)
            {
                lowerList.Add(points[i]);
                while (lowerList.Count >= 3)
                {
                    var a = lowerList[lowerList.Count - 3];
                    var b = lowerList[lowerList.Count - 2];
                    var c = lowerList[lowerList.Count - 1];

                    if (Utility2D.GetPointPosition(c, a, b) != 1)
                    {
                        lowerList.Remove(b);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            lowerList.RemoveAt(0);
            lowerList.RemoveAt(lowerList.Count - 1);

            upList.AddRange(lowerList);

            return upList.ToArray();
        }
    }
}