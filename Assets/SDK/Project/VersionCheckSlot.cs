using Assets.Foundation.Common;
using Assets.Foundation.DataAccess;
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
    /// 版本检查
    /// </summary>
    internal class VersionCheckSlot : WorkSlot
    {
        /// <summary>
        /// 存放在Resources目录下的路径
        /// </summary>
        private string _configPath;

        /// <summary>
        /// 清单
        /// </summary>
        private VersionDetail _config;

        public VersionCheckSlot()
        {
            _configPath = string.Format("App/{0}", _config.GetType().Name);
        }

        public override void Init()
        {
            var externalPath = string.Format("{0}.xml", Path.Combine(FilePath.PersistentDataPath, _configPath));
            if (File.Exists(externalPath))
            {
                _config = ConfigHelper.LoadFromXmlFile<VersionDetail>(externalPath);
            }
            else
            {
                _config = ConfigHelper.LoadFromXmlResource<VersionDetail>(_configPath);
                ConfigHelper.SaveToXmlFile<VersionDetail>(_config, externalPath);
            }

            GameDetail.GameID = _config.GameID;
            GameDetail.MainVersion = _config.MainVersion;
            GameDetail.SubVersion = _config.SubVersion;
            this.MoveNext();
        }

        /// <summary>
        /// 检查版本
        /// </summary>
        /// <returns></returns>
        public override void DoEvent()
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();
            /*
            var fieldInfos = _config.GetType().GetFields();
            foreach (var item in fieldInfos)
            {
                fields.Add(item.Name, Convert.ToString(item.GetValue(_config)));
            }
            fields.Remove("HostUrl");
            */
            string url = _config.HostUrl;
            EnumeratorManager.Instance.Add(this.Download(url, null));
        }

        private IEnumerator Download(string url, Dictionary<string, string> fields)
        {
            UnityWebRequest request = HttpUtility.DoGet(url, fields);
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log(request.error);
                this.MoveNext();
            }
            else if (request.isDone)
            {
                Data = request.downloadHandler.text;
                this.MoveNext();
            }
        }

        public override void Finish()
        {
            this.MoveNext();
        }
    }
}
