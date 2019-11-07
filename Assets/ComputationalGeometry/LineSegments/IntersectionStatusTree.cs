using System;
using Game.Algorithm.Structure;

namespace Game.ComputationalGeometry.LineSegments
{
    public class StatusNode : AVLNode<VertexSegment2D>
    {

    }

    /// <summary>
    /// 相交状态树
    /// </summary>
    public class IntersectionStatusTree : AVLTree<VertexSegment2D>
    {
        public IntersectionStatusTree()
            : base((a, b)=> { return a.Up.x.CompareTo(b.Up.x);})
        {
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="seg"></param>
        public void Remove(VertexSegment2D seg)
        {
            var node = new StatusNode();
            node.Data = seg;
            if (this.Contains(node))
            {
                this.Remove(node);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="seg"></param>
        public void Add(VertexSegment2D seg)
        {
            var node = new StatusNode();
            node.Data = seg;
            if (!this.Contains(node))
            {
                this.Add(node);
            }
        }
    }
}
