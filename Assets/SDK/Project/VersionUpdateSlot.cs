using Assets.Foundation.DataAccess;
using Assets.Foundation.Common;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 版本更新
    /// </summary>
    class VersionUpdateSlot : WorkSlot
    {
        /// <summary>
        /// 存放在Resources目录下的路径
        /// </summary>
        private string _configPath;

        private UpdateDetail _config;

        private string _tempUrl;
        private string _bundleUrl;

        public VersionUpdateSlot()
        {
            _configPath = string.Format("{0}/App/{1}.xml", FilePath.PersistentDataPath, _config.GetType().Name);
        }

        public override void Init()
        {
            _config = ConfigHelper.LoadFromXmlText<UpdateDetail>((string)Data);

            GameDetail.LoginServerAddress = _config.ServerAddress;
            GameDetail.LoginServerPort = _config.ServerPort;

            var localConfig = ConfigHelper.LoadFromXmlFile<UpdateDetail>(_configPath);
            if (localConfig.MainVersion == _config.MainVersion && localConfig.SubVersion == _config.SubVersion)
            {
                this.MoveTo(WorkState.End);
                return;
            }

            _tempUrl = Path.Combine(FilePath.TemporaryCachePath, string.Format("{0}-{1}.zip", _config.MainVersion, _config.SubVersion));
            _bundleUrl = Path.Combine(FilePath.PersistentDataPath, FilePath.BundlesPath);

            Debug.Log(_tempUrl);
            Debug.Log(_bundleUrl);

            this.MoveNext();
        }

        public override void DoEvent()
        {
            string url = _config.AssetBundleUrl;
            DownloadManager.Instance.AddTask(url, _tempUrl, this.OnDownloading);
        }

        private void OnDownloading(string error, string url, float progress)
        {
            if (!string.IsNullOrEmpty(error))
            {
                Debug.LogError(error);
                this.MoveNext();
                return;
            }
            Debug.LogFormat("Downloading percent {0}", progress * 100);

            if (progress == 1)
            {
                SingletonBehaviour.GetInstance<ZipResource>().AddTask(new ZipResource.LoadTask(_tempUrl, _bundleUrl, (IFileItem item) =>
                {
                    this.MoveNext();
                }));
            }           
        }

        public override void Finish()
        {
            this.MoveNext();
        }
    }
}
