using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 文件路径
    /// </summary>
    public sealed class FilePath
    {
        /// <summary>
        /// 包路径
        /// </summary>
        public static string BundlesPath
        {
            get
            {
                return "Bundles";
            }
        }
        /// <summary>
        /// 流资源路径，不压缩，不加密，只读
        /// </summary>
        public static string StreamingAssetsPath
        {
            get
            {
#if UNITY_ANDROID
                return Application.streamingAssetsPath;
#else
                return "file://" + Application.streamingAssetsPath;
#endif
            }
        }

        /// <summary>
        /// 持久数据路径，可读写
        /// </summary>
        public static string PersistentDataPath
        {
            get
            {
                return Application.persistentDataPath;
            }
        }

        /// <summary>
        /// 临时缓存路径，可读写
        /// </summary>
        public static string TemporaryCachePath
        {
            get
            {
                return Application.temporaryCachePath;
            }
        }

        /// <summary>
        /// 应用所在的路径
        /// </summary>
        public static string AppRoot
        {
            get
            {
#if UNITY_ANDROID
                return Application.dataPath;
#else
                return "file://" + Application.dataPath;
#endif
            }
        }

        /// <summary>
        /// 获取资源包路径
        /// </summary>
        /// <returns></returns>
        public static string GetBundleManifestPath()
        {
            string configPath = "";

            if (Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                configPath = string.Format("{0}/Bundles/{1}.manifest", PersistentDataPath, Application.platform.ToString());
            }
            else 
            {
                configPath = string.Format("{0}/Output/{1}/{1}.manifest", AppRoot, Application.platform.ToString());
            }

            return configPath;
        }

        /// <summary>
        /// 获取资源包路径
        /// </summary>
        /// <returns></returns>
        public static string GetBundlePath()
        {
            string configPath = "";

            if (Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                configPath = Path.Combine(PersistentDataPath, BundlesPath);
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                configPath = string.Format("{0}/Output/{1}/", Application.dataPath, Application.platform.ToString());
            }

            return configPath;
        }

        /// <summary>
        /// 单例
        /// </summary>
        private static FilePath _instance;

        /// <summary>
        /// 单例
        /// </summary>
        public static FilePath Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FilePath();
                    _instance.Init();
                }

                return _instance;
            }
        }

        /// <summary>
        /// 搜索路径
        /// </summary>
        private HashSet<string> _searchPaths;
        /// <summary>
        /// 资源文件名对应完整路径
        /// </summary>
        private Dictionary<string, string> _fullpaths;

        private FilePath()
        {
            _fullpaths = new Dictionary<string, string>();
            _searchPaths = new HashSet<string>();
        }

        private void Init()
        {
#if UNITY_EDITOR
            AddSearchPath("");
            AddSearchPath("Assets/Editor/Resource");
#else
#endif
 
        }

        /// <summary>
        /// 增加搜索目录
        /// </summary>
        /// <param name="path"></param>
        public void AddSearchPath(string path)
        {
            if (path == null)
            {
                return;
            }

            _searchPaths.Add(path);
        }

        /// <summary>
        /// 增加搜索目录
        /// </summary>
        /// <param name="paths"></param>
        public void AddSearchPaths(string[] paths)
        {
            if (paths == null || paths.Length == 0)
            {
                return;
            }
            foreach (var item in paths)
            {
                _searchPaths.Add(item);
            }
        }

        /// <summary>
        /// 移除搜索目录
        /// </summary>
        /// <param name="path"></param>
        public void RemoveSearchPath(string path)
        {
            if (path == null)
            {
                return;
            }

            _searchPaths.Remove(path);
        }

        /// <summary>
        /// 清空搜素目录
        /// </summary>
        public void RemoveAllSearchPaths()
        {
            _searchPaths.Clear();
        }

        /// <summary>
        /// 获取文件的完整路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetFullPath(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }
            if (_fullpaths.ContainsKey(filename))
            {
                return _fullpaths[filename];
            }

            foreach (var item in _searchPaths)
            {
                // 外部文件
                string path = Path.Combine(item, filename);
                if (File.Exists(path))
                {
                    _fullpaths.Add(filename, path);

                    return path;
                }
#if UNITY_EDITOR
                // editor
                if (AssetDatabase.LoadAssetAtPath<Object>(path))
                {
                    _fullpaths.Add(filename, path);
                    return path;
                }
#endif

                // Resources文件
                if (Resources.Load<Object>(path))
                {
                    _fullpaths.Add(filename, path);

                    return path;
                }
            }

            return null;
        }


        public T Get<T>(string filename) where T : Object
        {
            string fullpath = GetFullPath(filename);
            if (fullpath == null)
            {
                return null;
            }
#if UNITY_EDITOR
            return AssetDatabase.LoadAssetAtPath<T>(fullpath);
#else
            return Resources.Load<T>(fullpath);
#endif
        }
    }
}
