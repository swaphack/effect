using Assets.Foundation.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Game.Project
{
    /// <summary>
    /// 游戏清单
    /// </summary>
    public struct GameMainConfig
    {
        /// <summary>
        /// 游戏编号
        /// </summary>
        public long GameID;
        /// <summary>
        /// 主版本号
        /// </summary>
        public int MainVersion;
        /// <summary>
        /// 子版本号
        /// </summary>
        public int SubVersion;
        /// <summary>
        /// 官网地址
        /// </summary>
        public string HostUrl;
    }

    /// <summary>
    /// 版本检查
    /// </summary>
    class VersionCheckSlot : WorkSlot
    {
        /// <summary>
        /// 存放在Resources目录下的路径
        /// </summary>
        public const string ConfigPath = "Config/GameMainConfig";

        /// <summary>
        /// 清单
        /// </summary>
        private GameMainConfig _config;

        public override IEnumerator Init(System.Object data)
        {
            _config = ConfigHelper.LoadFromXmlResource<GameMainConfig>(ConfigPath);
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
            yield return request.Send();
            if (request.isError)
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
