using System.Collections.Generic;
using UnityEngine;

namespace Assets.Foundation.Extensions
{
    /// <summary>
    /// 节点扩展
    /// </summary>
    public static class NodeExtensions
    {
        public static T CreateComponent<T>(this GameObject t) where T : Component
        {
            if (t == null)
            {
                return null;
            }
            var c = t.GetComponent<T>();
            if (c == null)
            {
                c = t.AddComponent<T>();
            }

            return c;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="go"></param>
        /// <param name="child"></param>
        public static void AddChild(this GameObject go, GameObject child)
        {
            if (go == null || child == null)
            {
                return;
            }
            child.transform.SetParent(go.transform);
        }

        /// <summary>
        /// 移除子节点
        /// </summary>
        /// <param name="go"></param>
        /// <param name="child"></param>
        public static void RemoveChild(this GameObject go, GameObject child)
        {
            if (go == null || child == null)
            {
                return;
            }
            child.transform.SetParent(null);
        }

        /// <summary>
        /// 移除子节点并删除
        /// </summary>
        /// <param name="go"></param>
        /// <param name="child"></param>
        public static void RemoveChildAndCleanUp(this GameObject go, GameObject child)
        {
            if (go == null || child == null)
            {
                return;
            }

            child.transform.SetParent(null);
            GameObject.DestroyImmediate(child);
        }

        /// <summary>
        /// 从父节点移除
        /// </summary>
        /// <param name="go"></param>
        public static void RemoveFromParent(this GameObject go)
        {
            if (go == null)
            {
                return;
            }
            go.transform.SetParent(null);
        }

        /// <summary>
        /// 从父节点移除并释放
        /// </summary>
        /// <param name="go"></param>
        public static void RemoveFromParentAndCleanUp(this GameObject go)
        {
            if (go == null)
            {
                return;
            }
            go.transform.SetParent(null);
            GameObject.DestroyImmediate(go);
        }

        /// <summary>
        /// 移除所有子节点
        /// </summary>
        /// <param name="go"></param>
        public static void RemoveAllChildren(this GameObject go)
        {
            if (go == null)
            {
                return;
            }
            var count = go.transform.childCount;
            for (var i = count - 1; i >= 0; i--)
            {
                var child = go.transform.GetChild(i);
                child.gameObject.RemoveFromParentAndCleanUp();
            }
        }        

        /// <summary>
        /// 获取字节点的数量
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static int GetChildCount(this GameObject go)
        {
            if (go == null)
            {
                return 0;
            }
            return go.transform.childCount;
        }

        public static List<GameObject> GetChildren(this GameObject go)
        {
            if (go == null)
            {
                return null;
            }

            List<GameObject> gos = new List<GameObject>();

            var count = go.transform.childCount;
            for (var i = 0; i < count; i++)
            {
                var child = go.transform.GetChild(i);
                gos.Add(child.gameObject);
            }

            return gos;
        }

        /// <summary>
        /// 根据名字查找子节点
        /// </summary>
        /// <param name="go"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindChild(this GameObject go, string name)
        {
            if (go == null)
            {
                return null;
            }
            var child = go.transform.Find(name);
            if (child == null)
            {
                return null;
            }

            return child.gameObject;
        }

        /// <summary>
        /// 根据名字递归查找子节点
        /// </summary>
        /// <param name="go"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject FindChildWithRecurse(this GameObject go, string name)
        {
            if (go == null)
            {
                return null;
            }
            if (go.name == name)
            {
                return go;
            }
            var count = go.transform.childCount;
            for (var i = 0; i < count; i++)
            {
                var child = go.transform.GetChild(i);
                var target = child.gameObject.FindChildWithRecurse(name);
                if (target)
                {
                    return target;
                }
            }

            return null;
        }
    }
}
