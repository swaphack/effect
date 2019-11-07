using System;
using System.Collections.Generic;

namespace Game.Editor.ShaderLab
{
    /// <summary>
    /// shader逻辑部分
    /// </summary>
    public class SubShader
    {
        public ShaderTags Tags { get; } = new ShaderTags();

        public List<ShaderPass> Passes { get; } = new List<ShaderPass>();

        public void AddPass(ShaderPass value)
        {
            if (value == null)
            {
                return;
            }
            if (!Passes.Contains(value))
            {
                Passes.Add(value);
            }
        }

        public void RemovePass(ShaderPass value)
        {
            if (value == null)
            {
                return;
            }
            Passes.Remove(value);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void ClearPasses()
        {
            Passes.Clear();
        }
    }
}
