
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.DataAccess
{
    public static class EditorAssets
    {
        /// <summary>
        /// 当前工程所在目录
        /// </summary>
        public static string Root
        {
            get
            {
                return string.Format("{0}/Assets/", System.Environment.CurrentDirectory);
            }
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="root"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] GetFilePaths(string root, string pattern)
        {
            if (string.IsNullOrEmpty(root))
            {
                return null;
            }

            if (!Directory.Exists(root))
            {
                return null;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                pattern = "*";
            }

            string[] allpath = Directory.GetFiles(root, pattern, SearchOption.AllDirectories);

            string dir = System.Environment.CurrentDirectory;
            dir = dir.Replace('\\', '/');

            List<string> filepaths = new List<string>();

            for (int i = 0; i < allpath.Length; i++)
            {
                string fullpath = allpath[i].Replace('\\', '/');
                fullpath = fullpath.Substring(dir.Length + 1);

                filepaths.Add(fullpath);
            }

            return filepaths.ToArray();
        }

        /// <summary>
        /// 获取资源完整路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetAssetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            string appRoot = System.Environment.CurrentDirectory;

            if (path.StartsWith(appRoot, StringComparison.Ordinal))
            {
                path = path.Substring(appRoot.Length+1);
            }

            if (!path.StartsWith("Assets/", StringComparison.Ordinal))
            {
                path = string.Format("Assets/{0}", path);
            }


            //string fullpath = string.Format("Assets/EditorResources/{0}", path);

            return path;
        }

        /// <summary>
        /// 获取资源路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetResourcePath(string path)
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
        public static T LoadAssetAtPath<T>(string path) where T : UnityEngine.Object
        {
            string fullpath = GetAssetPath(path);
            if (string.IsNullOrEmpty(fullpath))
            {
                return default(T);
            }
            return AssetDatabase.LoadAssetAtPath<T>(fullpath);
        }
    }
}

