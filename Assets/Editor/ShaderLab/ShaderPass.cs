using System;

namespace Game.Editor.ShaderLab
{
    public class ShaderPass
    {
        /// <summary>
        /// 文档名称
        /// </summary>
        public string Name { get; set; }

        public ShaderTags Tags { get; } = new ShaderTags();
        public ShaderProgram Program { get; } = new ShaderProgram();
    }
}
