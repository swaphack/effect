using System;
using System.Collections.Generic;
using System.Linq;
using Game.Algorithm.Structure;
using Game.Mathematics.Geometry2D;
using UnityEngine;

namespace Game.ComputationalGeometry.LineSegments
{
    /// <summary>
    /// 相交
    /// </summary>
    public class Intersection2D
    {        
        /// <summary>
        /// 比较器
        /// </summary>
        private class SortedVector : IComparer<Vector2>
        {
            public int Compare(Vector2 x, Vector2 y)
            {
                if (x.y > y.y) return -1;
                if (x.y < y.y) return 1;
                if (x.x < y.x) return -1;
                if (x.x > y.x) return 1;
                return 0;
            }
        }

<<<<<<< HEAD
=======
        /// <summary>
        /// 比较器
        /// </summary>
        private class SortedAddLinSeg : IComparer<Vector2>
        {
            public int Compare(Vector2 x, Vector2 y)
            {
                return x.x.CompareTo(y.x);
            }
        }
>>>>>>> 953175f322bcac1057ea6a522db447f36fa4f397
        private DoubleKeyDictionary<Vector2, VertexSegment2D> _upLineSegment = new DoubleKeyDictionary<Vector2, VertexSegment2D>();
        private DoubleKeyDictionary<Vector2, VertexSegment2D> _lowLineSegment = new DoubleKeyDictionary<Vector2, VertexSegment2D>();
        private SortedSet<Vector2> _sortedVertexes = new SortedSet<Vector2>(new SortedVector());
        private IntersectionStatusTree _statusTree = new IntersectionStatusTree();

        public void Clear()
        {
            _upLineSegment.Clear();
            _lowLineSegment.Clear();
            _sortedVertexes.Clear();
        }

        public Vector2[] FindIntersections(LineSegment2D[] lineSegs)
        {
            this.Clear();

            if (lineSegs == null || lineSegs.Length == 0)
            {
                return null;
            }

            VertexSegment2D[] segs = new VertexSegment2D[lineSegs.Length];

            for (var i = 0; i < lineSegs.Length; i++)
            {
                var seg = new VertexSegment2D(lineSegs[i]);

                segs[i] = seg;

                _upLineSegment.Add(seg.Up, seg);
                _lowLineSegment.Add(seg.Low, seg);

                _sortedVertexes.Add(seg.Up);
                _sortedVertexes.Add(seg.Low);
            }

            while (_sortedVertexes.Count > 0)
            {
                var point = _sortedVertexes.Min;
                _sortedVertexes.Remove(point);
                HandleEventPoint(point);
            }

<<<<<<< HEAD
            return _sortedVertexes.ToArray();
=======
            return null;
>>>>>>> 953175f322bcac1057ea6a522db447f36fa4f397
        }

        private List<Vector2> HandleEventPoint(Vector2 point)
        {
            var upList = new List<VertexSegment2D>();
            foreach (var item in _upLineSegment.Values)
            {
                if (item.Up == point)
                {
                    upList.Add(item);
                }
            }

            var lowList = new List<VertexSegment2D>();
            var containsList = new List<VertexSegment2D>();

            _statusTree.InorderTraversal((a) =>
            {
                if (a.Data.Low == point)
                {
                    lowList.Add(a.Data);
                }
                if (a.Data.Line.Contains(point))
                {
                    containsList.Add(a.Data);
                }
            });

            var allSeg = new SortedSet<VertexSegment2D>();
            allSeg.UnionWith(upList);
            allSeg.UnionWith(lowList);
            allSeg.UnionWith(containsList);

            List<Vector2> intersectionPoints = new List<Vector2>();
            if (allSeg.Count != 0)
            {
<<<<<<< HEAD
                return allSeg;
=======
                //var ary = new 
                //intersectionPoints.AddRange();
>>>>>>> 953175f322bcac1057ea6a522db447f36fa4f397
            }

            var deleteSeg = new SortedSet<VertexSegment2D>();
            deleteSeg.UnionWith(lowList);
            deleteSeg.UnionWith(containsList);
            if (deleteSeg.Count > 0)
            {
                var ary = new VertexSegment2D[deleteSeg.Count];
                deleteSeg.CopyTo(ary);
                for (var i = 0; i < ary.Length; i++)
                {
                    _statusTree.Remove(ary[i]);
                }
            }
<<<<<<< HEAD
            
            var addSeg = new SortedSet<VertexSegment2D>();
            containsList.Reverse();
=======

            
            var addSeg = new SortedSet<VertexSegment2D>();
            containsList.Reverse();

>>>>>>> 953175f322bcac1057ea6a522db447f36fa4f397
            addSeg.UnionWith(upList);
            addSeg.UnionWith(containsList);
            if(addSeg.Count > 0)
            {
                var ary = new VertexSegment2D[addSeg.Count];
                addSeg.CopyTo(ary);
                for (var i = 0; i < ary.Length; i++)
                {
                    _statusTree.Add(ary[i]);
                }
            }

<<<<<<< HEAD
            return allSeg;
        }
=======
            return null;
        }        
>>>>>>> 953175f322bcac1057ea6a522db447f36fa4f397
    }
}
