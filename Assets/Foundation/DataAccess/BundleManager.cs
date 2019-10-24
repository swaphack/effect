using UnityEngine;
using System.Collections.Generic;
using Assets.Foundation.Common;
using System.IO;
using Assets.SDK.Project;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 资源包管理
    /// </summary>
    public sealed class BundleManager : SingletonBehaviour<BundleManager>
    {
        /// <summary>
        /// 资源名字对应资源包
        /// </summary>
        private Dictionary<string, AssetBundle> _assetBundles;
        /// <summary>
        /// 资源名字对应资源包名字
        /// </summary>
        private Dictionary<string, string> _assetPaths;

        private BundleManager()
        {
            _assetBundles = new Dictionary<string,AssetBundle>();
            _assetPaths = new Dictionary<string, string>();
        }

        /// <summary>
        /// 添加资源包
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bundle"></param>
        public void Add(string path, AssetBundle bundle)
        {
            if (string.IsNullOrEmpty(path) || bundle == null)
            {
                return;
            }
            _assetBundles[path] = bundle;

            string[] assetNames = bundle.GetAllAssetNames();
            if (assetNames != null && assetNames.Length > 0)
            {
                for (var i = 0; i < assetNames.Length; i++)
                {
                    _assetPaths[assetNames[i]] = path;
                }
            }
        }

        /// <summary>
        /// 移除资源包
        /// </summary>
        /// <param name="path"></param>
        public void Remove(string path)
        {
            if (!_assetBundles.ContainsKey(path))
            {
                return;
            }

            var bundle = _assetBundles[path];
            string[] assetNames = bundle.GetAllAssetNames();
            for (var i = 0; i < assetNames.Length; i++)
            {
                _assetPaths.Remove(assetNames[i]);
            }
            bundle.Unload(true);
        }
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public T LoadAsset<T>(string assetName) where T : Object
        {
            if (string.IsNullOrEmpty(assetName))
            {
                return null;
            }

            if (!_assetPaths.ContainsKey(assetName))
            {
                return null;
            }

            string bundleName = _assetPaths[assetName];
            if (_assetBundles.ContainsKey(bundleName))
            {
                return _assetBundles[bundleName].LoadAsset<T>(assetName);
            }

            return null;
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public AssetBundleRequest LoadAssetAsync<T>(string assetName) where T : Object
        {
            if (string.IsNullOrEmpty(assetName))
            {
                return null;
            }

            if (!_assetPaths.ContainsKey(assetName))
            {
                return null;
            }

            string bundleName = _assetPaths[assetName];
            if (_assetBundles.ContainsKey(bundleName))
            {
                return _assetBundles[bundleName].LoadAssetAsync<T>(assetName);
            }

            return null;
        }

        /// <summary>
        /// 加载资源包文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadFromFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            if (bytes == null)
            {
                return false;
            }
            var bundle = AssetBundle.LoadFromMemory(bytes);
            if (bundle == null)
            {
                return false;
            }

            this.Add(path, bundle);

            return true;
        }

        /// <summary>
        /// 是否包含文件
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public bool Contains(string assetName)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                return false;
            }

            return _assetPaths.ContainsKey(assetName);
        }

        public override void Initialize()
        {
            LoadAssetBundle();
        }

        /*
        void LoadAssetBundle()
        {
            string configPath = FilePath.GetBundlePath();
            
            string[] files = Directory.GetFiles(configPath, "*.unity3d");
            for (int i = 0; i < files.Length; i++)
            {
                LoadFromFile(files[i]);
            }
        }
         * */

        void LoadAssetBundle()
        {
            string configPath = FilePath.GetBundleManifestPath();

            WWW www = new WWW(configPath);
            while (!www.isDone) { };
            if (!string.IsNullOrEmpty(www.error))
            {
                return;
            }
            ABManifest manifest = new ABManifest();
            manifest.Read(www.text);
            var names = manifest.GetAllAssetBundles();
            if (names == null || names.Count == 0)
            {
                return;
            }

            for (int i = 0; i < names.Count; i++)
            {
                string fullpath = Path.Combine(FilePath.GetBundlePath(), names[i]);
                this.LoadFromFile(fullpath);
            }
        }
    }
}
