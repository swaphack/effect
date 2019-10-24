using Assets.Foundation.DataAccess;
using Assets.Foundation.Common;
using Assets.Foundation.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 工程清单
    /// </summary>
    public struct GameUpdateConfig
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress;
        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort;
        /// <summary>
        /// 资源包路径
        /// </summary>
        public string AssetBundleUrl;
        /// <summary>
        /// 主版本号
        /// </summary>
        public int MainVersion;
        /// <summary>
        /// 子版本号
        /// </summary>
        public int SubVersion;
    }

    /// <summary>
    /// 版本更新
    /// </summary>
    class VersionUpdateSlot : WorkSlot
    {
        /// <summary>
        /// 存放在Resources目录下的路径
        /// </summary>
        public const string ConfigPath = "Config/GameUpdateConfig";

        private GameUpdateConfig _config;

        private string _tempUrl;
        private string _bundleUrl;
        public override IEnumerator Init(object data)
        {
            State = WorkState.Start;

            _config = ConfigHelper.LoadFromXmlText<GameUpdateConfig>((string)data);

            GameDetail.LoginServerAddress = _config.ServerAddress;
            GameDetail.LoginServerPort = _config.ServerPort;

            var localConfig = ConfigHelper.LoadFromXmlFile<GameUpdateConfig>(ConfigPath);
            if (localConfig.MainVersion == _config.MainVersion && localConfig.SubVersion == _config.SubVersion)
            {
                State = WorkState.End;
                yield return null;
            }

            _tempUrl = Path.Combine(FilePath.TemporaryCachePath, string.Format("{0}-{1}.zip", _config.MainVersion, _config.SubVersion));
            _bundleUrl = Path.Combine(FilePath.PersistentDataPath, FilePath.BundlesPath);

            
            yield return null;
        }

        public override IEnumerator DoEvent()
        {
            State = WorkState.Update;

            string url = _config.AssetBundleUrl;
            DownloadManager.Instance.AddTask(url, _tempUrl, this.OnDownloading);
            yield return null;
        }

        private void OnDownloading(string error, string url, float progress)
        {
            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError(error);
                State = WorkState.End;
                return;
            }
            if (progress == 1)
            {
                SingletonBehaviour.GetInstance<ZipResource>().AddTask(new ZipResource.LoadTask(_tempUrl, _bundleUrl, (IFileItem item) =>
                {
                    State = WorkState.End;
                }));
            }           
        }

        public override IEnumerator Finish()
        {
            yield return null;
        }
    }
}
