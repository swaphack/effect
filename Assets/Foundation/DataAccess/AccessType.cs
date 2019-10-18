using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 获取资源方式
    /// </summary>
    public enum AccessType
    {
        /// <summary>
        /// Resources目录下
        /// </summary>
        Resources,
        /// <summary>
        /// StreamingAssets目录下
        /// </summary>
        StreamingAssets,
        /// <summary>
        /// 资源包
        /// </summary>
        AssetBundles,
        /// <summary>
        /// editor资源
        /// </summary>
        AssetDatabase,
        /// <summary>
        /// 外部目录
        /// </summary>
        Extern
    }
}
