using System;
using System.Collections.Generic;

namespace Assets.Editor.ShaderLab
{
    public class ShaderTags
    {
        public Dictionary<string, string> Tags { get; } = new Dictionary<string, string>();

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        public void AddTag(string tag, string value)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }
            Tags[tag] = value;
        }

        /// <summary>
        /// 移除标签
        /// </summary>
        /// <param name="tag"></param>
        public void RemoveTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return;
            }
            Tags.Remove(tag);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            Tags.Clear();
        }
    }
}
