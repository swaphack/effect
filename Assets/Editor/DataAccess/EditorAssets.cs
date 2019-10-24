
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.DataAccess
{
    public static class EditorAssets
    {
        /// <summary>
        /// 获取资源完整路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            string fullpath = string.Format("Assets/EditorResources/{0}", path);

            return fullpath;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T LoadAssetAtPath<T>(string path) where T : Object
        {
            string fullpath = GetFullPath(path);
            if (string.IsNullOrEmpty(fullpath))
            {
                return null;
            }
            return AssetDatabase.LoadAssetAtPath<T>(fullpath);
        }
    }
}

