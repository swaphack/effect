using System;
using UnityEngine;

namespace Game.Algorithm.Structure
{
    /// <summary>
    /// 平衡二叉排序树节点
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public class AVLNode<Type> : BinaryNode<AVLNode<Type>, Type>
    {
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
    }

    /// <summary>
    /// 平衡二叉排序树
    /// </summary>
    public class AVLTree<Type> : BinaryTree<AVLNode<Type>, Type>
    {
        public AVLTree(Comparison<Type> compareFunc)
            : base(compareFunc)
        {
        }

        /// <summary>
        /// 左孩子插入左节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public AVLNode<Type> HandLeftRotation(AVLNode<Type> node)
        {
            AVLNode<Type> temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            node.Height = Mathf.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            temp.Height = Mathf.Max(GetHeight(temp.Left), GetHeight(temp.Right)) + 1;

            return temp;
        }

        /// <summary>
        /// 右孩子插入右节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public AVLNode<Type> HandRightRotation(AVLNode<Type> node)
        {
            AVLNode<Type> temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;

            node.Height = Mathf.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
            temp.Height = Mathf.Max(GetHeight(temp.Left), GetHeight(temp.Right)) + 1;

            return temp;
        }

        /// <summary>
        /// 左孩子插入右节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public AVLNode<Type> HandLeftRightRotation(AVLNode<Type> node)
        {
            node.Left = HandRightRotation(node.Left);
            return HandLeftRotation(node);
        }

        /// <summary>
        /// 右孩子插入左节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public AVLNode<Type> HandRightLeftRotation(AVLNode<Type> node)
        {
            node.Right = HandLeftRotation(node.Right);
            return HandRightRotation(node);
        }

        /// <summary>
        /// 高度
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetHeight(AVLNode<Type> node)
        {
            if (node == null)
            {
                return -1;
            }

            return node.Height;
        }

        /// <summary>
        /// 平衡因子
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetBalanceFactor(AVLNode<Type> node)
        {
            if (node == null)
            {
                return 0;
            }
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node"></param>
        public void Add(AVLNode<Type> node)
        {
            if (node == null)
            {
                return;
            }

            Root = Add(Root, node);
        }

        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="node"></param>
        private void UpdateNode(AVLNode<Type> node)
        {
            if (node == null)
            {
                return;
            }

            this.UpdateNodeHeight(node);
        }

        /// <summary>
        /// 更新节点高度
        /// </summary>
        /// <param name="node"></param>
        private void UpdateNodeHeight(AVLNode<Type> node)
        {
            if (node == null)
            {
                return;
            }
            node.Height = Mathf.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public AVLNode<Type> Add(AVLNode<Type> parent, AVLNode<Type> node)
        {
            if (parent == null)
            {
                return node;
            }

            int? ret = Compare(node, parent);

            if (ret < 0)
            {
                parent.Left = Add(parent.Left, node);
                //如果左孩子的高度比右孩子大
                if (GetBalanceFactor(parent) > 1)
                {
                    int? ret0 = Compare(node, parent.Left);
                    if (ret0 < 0)
                    {
                        parent = HandLeftRotation(parent);
                    }
                    else if (ret0 > 0)
                    {
                        parent = HandLeftRightRotation(parent);
                    }
                }
            }
            else if (ret > 0)
            {
                parent.Right = Add(parent.Right, node);
                if (GetBalanceFactor(parent) > 1)
                {
                    int? ret0 = Compare(node, parent.Right);
                    if (ret0 < 0)
                    {
                        parent = HandRightLeftRotation(parent);
                    }
                    else if (ret0 > 0)
                    {
                        parent = HandRightRotation(parent);
                    }
                }
            }

            UpdateNode(parent);

            return parent;
        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="node"></param>
        public void Remove(AVLNode<Type> node)
        {
            if (Root == null || node == null)
            {
                return;
            }

            Remove(Root, node);
        }

        public void Remove(AVLNode<Type> parent, AVLNode<Type> node)
        {
            if (parent == null || node == null)
            {
                return;
            }

            int? ret = Compare(node, parent);
            if (ret == 0)
            {
                if (parent.Left != null && parent.Right != null)
                {
                    if (GetBalanceFactor(parent) > 0)
                    {
                        var maxNode = PopMaxValueNode(parent.Left);
                        parent.Data = maxNode.Data;
                    }
                    else
                    {
                        var maxNode = PopMinValueNode(parent.Right);
                        parent.Data = maxNode.Data;
                    }
                }
                else
                {
                    var temp = parent.Left == null ? parent.Right : parent.Left;
                    parent = temp;
                }
            }
            else if (ret < 0)
            {
                Remove(parent.Left, node);
                if (GetBalanceFactor(parent) < -1)
                {
                    if (GetHeight(parent.Right.Left) > GetHeight(parent.Right.Right))
                    {
                        parent = HandRightLeftRotation(parent);
                    }
                    else
                    {
                        parent = HandRightRotation(parent);
                    }
                }
                else
                {
                    UpdateNode(parent);
                }
            }
            else
            {
                Remove(parent.Right, node);
                if (GetBalanceFactor(parent) > 1)
                {
                    if (GetHeight(parent.Left.Left) > GetHeight(parent.Left.Right))
                    {
                        parent = HandLeftRotation(parent);
                    }
                    else
                    {
                        parent = HandLeftRightRotation(parent);
                    }
                }
                else
                {
                    UpdateNode(parent);
                }
            }
        }
    }
}
