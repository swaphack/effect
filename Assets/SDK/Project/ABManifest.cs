using Assets.Foundation.Data;
using Assets.Foundation.TextFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 主文件解析
    /// </summary>
    public class ABManifest
    {
        public class AssetBundleInfo
        {
            public string Name;
            public List<string> Dependencies;

            public AssetBundleInfo(string name, List<string> dependencies)
            {
                Name = name;
                if (dependencies == null)
                    Dependencies = new List<string>();
                else
                    Dependencies = dependencies;
            }

            public AssetBundleInfo(string name)
                :this(name, null)
            {
            }
        }

        public string ManifestFileVersion;
        public string CRC;
        public Dictionary<string, AssetBundleInfo> AssetBundleInfos;

        public ABManifest()
        {
            ManifestFileVersion = "";
            CRC = "";
            AssetBundleInfos = new Dictionary<string, AssetBundleInfo>(); ;
        }

        public List<string> GetAllAssetBundles()
        {
            return AssetBundleInfos.Keys.ToList<string>();
        }

        public bool Read(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            DManifestReader config = new DManifestReader();
            config.Read(text);
            if (config.Root == null)
            {
                return false;
            }

            var mapValue = config.Root.Map;
            if (mapValue == null)
            {
                return false;
            }

            ManifestFileVersion = mapValue["ManifestFileVersion"].Value;
            CRC = mapValue["CRC"].Value;

            AssetBundleInfos.Clear();

            var assetBundleInfos = mapValue["AssetBundleManifest"].Map["AssetBundleInfos"].Map;
            foreach (var item in assetBundleInfos)
            {
                var name = item.Value.Map["Name"].Value;
                var strDependencies = item.Value.Map["Dependencies"].Value;
                if (string.IsNullOrEmpty(strDependencies))
                {
                    var mapDependencies = item.Value.Map["Dependencies"].Map;
                    List<string> values = new List<string>();
                    foreach (var res in mapDependencies)
                    {
                        values.Add(res.Value.Value);
                    }
                    AssetBundleInfos.Add(name, new AssetBundleInfo(name, values));
                }
                else
                {
                    AssetBundleInfos.Add(name, new AssetBundleInfo(name));
                }
            }

            return true;
        }
    }
}
