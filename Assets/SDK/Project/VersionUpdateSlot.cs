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
        public const string ConfigPath = "Config/GameUpdateConfig";

        private UpdateDetail _config;

        private string _tempUrl;
        private string _bundleUrl;
        public override IEnumerator Init(object data)
        {
            State = WorkState.Start;

            _config = ConfigHelper.LoadFromXmlText<UpdateDetail>((string)data);

            GameDetail.LoginServerAddress = _config.ServerAddress;
            GameDetail.LoginServerPort = _config.ServerPort;

            var localConfig = ConfigHelper.LoadFromXmlFile<UpdateDetail>(ConfigPath);
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
