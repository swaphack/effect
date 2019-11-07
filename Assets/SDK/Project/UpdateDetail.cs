
namespace Game.SDK.Project
{
    /// <summary>
    /// 更新清单
    /// </summary>
    public struct UpdateDetail
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
}
