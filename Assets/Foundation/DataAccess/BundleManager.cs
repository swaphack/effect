using UnityEngine;
using System.Collections.Generic;
using Assets.Foundation.Managers;
using System.IO;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 资源包管理
    /// </summary>
    public sealed class BundleManager : Singleton<BundleManager>
    {
        private Dictionary<string, AssetBundle> _assetBundles;

        private BundleManager()
        {
            _assetBundles = new Dictionary<string,AssetBundle>();
        }

        /// <summary>
        /// 添加资源包
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bundle"></param>
        public void Add(string path, AssetBundle bundle)
        {
            _assetBundles.Add(path, bundle);
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
            foreach (var item in _assetBundles)
            {
                if (item.Value.Contains(assetName))
                {
                    return item.Value.LoadAsset<T>(assetName);
                }
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
            foreach (var item in _assetBundles)
            {
                if (item.Value.Contains(assetName))
                {
                    return item.Value.LoadAssetAsync<T>(assetName);
                }
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

        public void Init()
        {
            
        }
    }
}
