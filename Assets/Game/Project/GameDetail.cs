using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Game.Project
{
    /// <summary>
    /// 游戏清单
    /// </summary>
    public static class GameDetail
    {
        /// <summary>
        /// 主版本号
        /// </summary>
        public static int MainVersion;
        /// <summary>
        /// 子版本号
        /// </summary>
        public static int SubVersion;
        /// <summary>
        /// 登陆服务器地址
        /// </summary>
        public static string LoginServerAddress;
        /// <summary>
        /// 登陆服务器端口
        /// </summary>
        public static int LoginServerPort;

        /// <summary>
        /// 游戏服务器地址
        /// </summary>
        public static string GameServerAddress;
        /// <summary>
        /// 游戏服务器端口
        /// </summary>
        public static int GameServerPort;
    }
}
