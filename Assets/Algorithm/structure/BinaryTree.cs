using System;
using System.Collections.Generic;

namespace Game.Algorithm.Structure
{
    /// <summary>
    /// 二叉树节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Type"></typeparam>
    public class BinaryNode<T, Type> : Value<Type> where T : BinaryNode<T, Type>
    {
        /// <summary>
        /// 做节点
        /// </summary>
        public T Left { get; set; }
        /// <summary>
        /// 右节点
        /// </summary>
        public T Right { get; set; }
    }


    /// <summary>
    /// 二叉树
    /// </summary>
    public class BinaryTree<T, Type> : IComparer<T> where T : BinaryNode<T, Type>
    {
        /// <summary>
        /// 比较函数
        /// </summary>
        public Comparison<Type> CompareFunc { get; private set; }
        /// <summary>
        /// 根结点
        /// </summary>
        public T Root { get; protected set; }

        public BinaryTree(Comparison<Type> compareFunc)
        {
            CompareFunc = compareFunc;
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Compare(T a, T b)
        {
            int? ret = CompareFunc?.Invoke(a.Data, b.Data);

            return (int)ret;
        }

        /// <summary>
        /// 查找目标
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public T FindNode(T target)
        {
            if (Root == null || target == null)
            {
                return null;
            }

            var temp = Root;
            while (temp != null)
            {
                int? ret = Compare(temp, target);
                if (ret == 0)
                {
                    return temp;
                }
                if (ret == -1)
                {
                    temp = temp.Left;
                }
                else
                {
                    temp = temp.Right;
                }
            }

            return null;
        }

        /// <summary>
        /// 是否包含节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Contains(T node)
        {
            if (node == null)
            {
                return false;
            }

            if (Root == null)
            {
                return false;
            }

            var temp = Root;
            while (temp != null)
            {
                int? ret = Compare(temp, node);
                if (ret == 0)
                {
                    return true;
                }
                if (ret == -1)
                {
                    temp = temp.Left;
                }
                else
                {
                    temp = temp.Right;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <returns></returns>
        public T GetMinValueNode()
        {
            return GetMinValueNode(Root);
        }

        /// <summary>
        /// 获取节点的最小值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public T GetMinValueNode(T node)
        {
            if (node == null)
            {
                return null;
            }
            var temp = node;
            while (temp.Left != null)
            {
                temp = temp.Left;
            }

            return temp;
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <returns></returns>
        public T GetMaxValueNode()
        {
            return GetMaxValueNode(Root);
        }

        /// <summary>
        /// 获取节点的最大值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public T GetMaxValueNode(T node)
        {
            if (node == null)
            {
                return null;
            }
            var temp = node;
            while (temp.Right != null)
            {
                temp = temp.Right;
            }

            return temp;
        }

        /// <summary>
        /// 获取节点的最小值并移除
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public T PopMinValueNode(T node)
        {
            if (node == null)
            {
                return null;
            }
            var temp = node;
            var parent = temp;
            while (temp.Left != null)
            {
                parent = temp;
                temp = temp.Left;
            }

            parent.Left = null;

            return temp;
        }

        /// <summary>
        /// 获取节点的最大值并移除
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public T PopMaxValueNode(T node)
        {
            if (node == null)
            {
                return null;
            }
            var temp = node;
            var parent = temp;
            while (temp.Right != null)
            {
                parent = temp;
                temp = temp.Right;
            }

            parent.Right = null;

            return temp;
        }

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <param name="node"></param>
        /// <param name="func"></param>
        public void PreorderTraversal(T node, Action<T> func)
        {
            if (node == null)
            {
                return;
            }

            func?.Invoke(node);
            PreorderTraversal(node.Left, func);
            PreorderTraversal(node.Right, func);
        }

        /// <summary>
        /// 前序遍历
        /// </summary>
        /// <param name="func"></param>
        public void PreorderTraversal(Action<T> func)
        {
            PreorderTraversal(Root, func);
        }

        /// <summary>
        /// 中序遍历
        /// </summary>
        /// <param name="node"></param>
        /// <param name="func"></param>
        public void InorderTraversal(T node, Action<T> func)
        {
            if (node == null)
            {
                return;
            }

            PreorderTraversal(node.Left, func);
            func?.Invoke(node);
            PreorderTraversal(node.Right, func);
        }

        /// <summary>
        /// 中序遍历
        /// </summary>
        /// <param name="func"></param>
        public void InorderTraversal(Action<T> func)
        {
            InorderTraversal(Root, func);
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <param name="node"></param>
        /// <param name="func"></param>
        public void PostorderTraversal(T node, Action<T> func)
        {
            if (node == null)
            {
                return;
            }

            PreorderTraversal(node.Left, func);
            PreorderTraversal(node.Right, func);
            func?.Invoke(node);
        }

        /// <summary>
        /// 后序遍历
        /// </summary>
        /// <param name="func"></param>
        public void PostorderTraversal(Action<T> func)
        {
            PostorderTraversal(Root, func);
        }

        public void Clear()
        {
            Root = null;
        }
    }
}
