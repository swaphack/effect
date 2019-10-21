using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using Assets.Foundation.Managers;
#endif

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 文件路径信息
    /// </summary>
    public class FilePathInfo
    {
        /// <summary>
        /// 资源名字
        /// </summary>
        public string _assetName;
        public string AssetName
        {
            get { return _assetName; }
            set { _assetName = value; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public AccessType _type;
        public AccessType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string _fullpath;
        public string Fullpath
        {
            get { return _fullpath; }
            set { _fullpath = value; }
        }
        public FilePathInfo(string name, string path, AccessType type)
        {
            this._assetName = name;
            this._fullpath = path;
            this._type = type;
        }
    }

    /// <summary>
    /// 文件路径
    /// </summary>
    public sealed class FilePath : Singleton<FilePath>
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
        /// 搜索路径
        /// </summary>
        private HashSet<string> _searchPaths;
        /// <summary>
        /// 资源文件名对应完整路径
        /// </summary>
        private Dictionary<string, FilePathInfo> _fullpaths;

        private FilePath()
        {
            _fullpaths = new Dictionary<string, FilePathInfo>();
            _searchPaths = new HashSet<string>();

            Init();
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
            _fullpaths.Clear();
        }

        /// <summary>
        /// 添加资源路径
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="fullpath"></param>
        /// <param name="type"></param>
        private void AddAssetPath(string assetName, string fullpath, AccessType type)
        {
            if (string.IsNullOrEmpty(assetName) || string.IsNullOrEmpty(fullpath))
            {
                return;
            }
            FilePathInfo info = new FilePathInfo(assetName, fullpath, type);

            _fullpaths[assetName] = info;
        }

        private FilePathInfo GetAsset(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                return null;
            }

            if (!_fullpaths.ContainsKey(assetName))
            {
                return null;
            }

            return _fullpaths[assetName];
        }

        /// <summary>
        /// 获取文件的完整路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public FilePathInfo GetAssetPath(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }
            var info = GetAsset(filename);
            if (info != null)
            {
                return info;
            }

            foreach (var item in _searchPaths)
            {
                // 外部文件
                string path = Path.Combine(item, filename);
#if UNITY_EDITOR
                // editor
                if (AssetDatabase.LoadAssetAtPath<Object>(path))
                {
                    this.AddAssetPath(filename, path, AccessType.AssetDatabase);
                    return GetAsset(filename);
                }
#endif

                if (BundleManager.Instance.Contains(path))
                {
                    this.AddAssetPath(filename, path, AccessType.AssetBundles);
                    return GetAsset(filename);
                }

                // Resources文件
                if (Resources.Load<Object>(path))
                {
                    this.AddAssetPath(filename, path, AccessType.Resources);
                    return GetAsset(filename);
                }
            }

            return null;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T LoadAssetAtPath<T>(string assetName) where T : UnityEngine.Object
        {
            if (string.IsNullOrEmpty(assetName))
            {
                return null;
            }

            var info = GetAssetPath(assetName);
            if (info == null)
            {
                return null;
            }

            if (info.Type == AccessType.Resources)
            {
                return Resources.Load<T>(info.Fullpath);
            }
            else if (info.Type == AccessType.AssetBundles)
            {
                return BundleManager.Instance.LoadAsset<T>(info.Fullpath);
            }
#if UNITY_EDITOR
            else if (info.Type == AccessType.AssetDatabase)
            {
                return AssetDatabase.LoadAssetAtPath<T>(info.Fullpath);
            }
#endif
            return null;
        }
    }
}
