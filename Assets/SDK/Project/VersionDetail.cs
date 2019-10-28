using System;
namespace Assets.SDK.Project
{
    /// <summary>
    /// 游戏清单
    /// </summary>
    public struct VersionDetail
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
}
