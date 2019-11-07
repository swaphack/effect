using System;
using System.Collections.Generic;
using Game.Algorithm.Structure;
using Game.Mathematics.Geometry2D;
using UnityEngine;

/*
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

        private DoubleKeyDictionary<Vector2, VertexSegment2D> _upLineSegment = new DoubleKeyDictionary<Vector2, VertexSegment2D>();
        private DoubleKeyDictionary<Vector2, VertexSegment2D> _lowLineSegment = new DoubleKeyDictionary<Vector2, VertexSegment2D>();
        private SortedSet<Vector2> _sortedVertexes;
        private IntersectionStatusTree _statusTree = new IntersectionStatusTree();

        public Intersection2D()
        {
            _sortedVertexes = new SortedSet<Vector2>(new SortedVector());
        }

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
        }

        private SortedSet<VertexSegment2D> HandleEventPoint(Vector2 point)
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

            if (allSeg.Count != 0)
            {
                yield return allSeg;
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
            
            var addSeg = new SortedSet<VertexSegment2D>();
            containsList.Reverse();
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
        }        
    }
}
*/
