using System;

namespace Game.Algorithm.Structure
{
    /// <summary>
    /// 二叉排序树节点
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class BinarySortNode<Type> : BinaryNode<BinarySortNode<Type>, Type> { }

    /// <summary>
    /// 二叉排序树
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class BinarySortTree<Type> : BinaryTree<BinarySortNode<Type>, Type>
    {
        public BinarySortTree(Comparison<Type> compareFunc)
            :base(compareFunc)
        {
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        public void Add(BinarySortNode<Type> parent, BinarySortNode<Type> node)
        {
            if (parent == null || node == null)
            {
                return;
            }

            var temp = parent;
            while (true)
            {
                int? ret = Compare(temp, node);

                if (ret < 0)
                {
                    if (temp.Left == null)
                    {
                        temp.Left = node;
                        break;
                    }
                    temp = temp.Left;
                }
                else if (ret > 0)
                {
                    if (temp.Right == null)
                    {
                        temp.Right = node;
                        break;
                    }
                    temp = temp.Right;
                }
                else
                {
                    throw new Exception("Value of input node exists in binary tree");
                }
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node"></param>
        public void Add(BinarySortNode<Type> node)
        {
            if (node == null)
            {
                return;
            }

            if (Root == null)
            {
                Root = node;
                return;
            }

            this.Add(Root, node);
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        public void Remove(BinarySortNode<Type> parent, BinarySortNode<Type> node)
        {
            if (parent == null || node == null)
            {
                return;
            }

            var temp = parent;
            while (temp != null)
            {
                int? ret = Compare(temp, node);
                if (ret < 0)
                {
                    temp = temp.Left;
                }
                else if (ret > 0)
                {
                    temp = temp.Right;
                }
                else
                {
                    if (temp.Right != null)
                    {
                        var min = PopMinValueNode(temp.Right);
                        temp.Data = min.Data;
                    }
                    else if (temp.Left != null)
                    {
                        var max = PopMaxValueNode(temp.Left);
                        temp.Data = max.Data;
                    }
                }
            }
        }
        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="node"></param>
        public void Remove(BinarySortNode<Type> node)
        {
            if (node == null)
            {
                return;
            }

            if (Root == null)
            {
                return;
            }

            this.Remove(Root, node);
        }
    }
}
