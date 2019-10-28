using Assets.Foundation.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public const string ConfigPath = "Config/GameMainConfig";

        /// <summary>
        /// 清单
        /// </summary>
        private VersionDetail _config;

        public override IEnumerator Init(object data)
        {
            _config = ConfigHelper.LoadFromXmlResource<VersionDetail>(ConfigPath);

            GameDetail.GameID = _config.GameID;
            GameDetail.MainVersion = _config.MainVersion;
            GameDetail.SubVersion = _config.SubVersion;

            State = WorkState.Start;
            yield return null;
        }

        /// <summary>
        /// 检查版本
        /// </summary>
        /// <returns></returns>
        public override IEnumerator DoEvent()
        {
            State = WorkState.Update;
            Dictionary<string, string> fields = new Dictionary<string, string>();
            var fieldInfos = _config.GetType().GetFields();
            foreach (var item in fieldInfos)
            {
                fields.Add(item.Name, Convert.ToString(item.GetValue(_config)));
            }
            fields.Remove("HostUrl");
            string url = _config.HostUrl;
            UnityWebRequest request = HttpUtility.DoGet(url, fields);
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else if (request.isDone)
            {
                Data = request.downloadHandler.text;
                State = WorkState.End;
            }
        }

        public override IEnumerator Finish()
        {
            yield return null;
        }
    }
}
