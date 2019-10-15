using UnityEngine;
using UnityEngine.UI;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public interface IFileItem
    {
        /// <summary>
        /// 资源路径
        /// </summary>
        string path { get; }
    }

    /// <summary>
    /// 文件转换
    /// </summary>
    public interface IExchangeFileItem : IFileItem
    {
        /// <summary>
        /// 目标路径
        /// </summary>
        string destPath { get; }
    }

    /// <summary>
    /// 资源类型
    /// </summary>
    public interface IResourceFileItem : IFileItem
    {
        /// <summary>
        /// 字节
        /// </summary>
        byte[] bytes { get; }
        /// <summary>
        /// 文本
        /// </summary>
        string text { get; }
        /// <summary>
        /// 图片
        /// </summary>
        Texture2D texture2D { get; }
        /// <summary>
        /// 音频
        /// </summary>
        AudioClip audioClip { get; }
    }
}
