using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Inspectors
{
    /// <summary>
    /// 视图管理
    /// </summary>
    public static class InspectorUtility
    {
        /// <summary>
        /// 对象视图合集
        /// </summary>
        private static Dictionary<int, UIInspector> _inspectors = new Dictionary<int, UIInspector>();

        /// <summary>
        /// 添加对象视图
        /// </summary>
        /// <param name="target"></param>
        /// <param name="inspector"></param>
        public static void AddTargetInspector(UnityEngine.Object target, UIInspector inspector)
        {
            if (target == null || inspector == null)
            {
                return;
            }

            _inspectors[target.GetInstanceID()] = inspector;
        }

        /// <summary>
        /// 移除对象视图
        /// </summary>
        /// <param name="target"></param>
        public static void RemoveTargetInspector(UnityEngine.Object target)
        {
            if (target == null)
            {
                return;
            }

            _inspectors.Remove(target.GetInstanceID());
        }

        /// <summary>
        /// 对象改变
        /// </summary>
        /// <param name="target"></param>
        public static void SetDirty(UnityEngine.Object target)
        {
            if (target == null)
            {
                return;
            }            

            int instanceID = target.GetInstanceID();

            if (!_inspectors.ContainsKey(instanceID))
            {
                return;
            }

            _inspectors[instanceID].Dirty = true;
        }
    }
}
