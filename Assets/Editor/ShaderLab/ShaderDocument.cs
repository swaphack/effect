using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Editor.ShaderLab
{
    public class ShaderDocument : IText
    {
        /// <summary>
        /// 文档名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public List<ShaderProperty> Properties { get; } = new List<ShaderProperty>();
        /// <summary>
        /// shader
        /// </summary>
        public List<SubShader> SubShaders { get; } = new List<SubShader>();
        /// <summary>
        /// 其他shader都无效时，调用该方法
        /// </summary>
        public string Fallback { get; set; }
        /// <summary>
        /// 自定义编辑器名称
        /// </summary>
        public string CustomEditor { get; set; }

        public void AddProperty(ShaderProperty value)
        {
            if (value == null)
            {
                return;
            }
            if (!Properties.Contains(value))
            {
                Properties.Add(value);
            }
        }

        public void RemoveProperty(ShaderProperty value)
        {
            if (value == null)
            {
                return;
            }
            Properties.Remove(value);
        }

        public void AddSubShader(SubShader value)
        {
            if (value == null)
            {
                return;
            }
            if (!SubShaders.Contains(value))
            {
                SubShaders.Add(value);
            }
        }

        public void RemoveSubShader(SubShader value)
        {
            if (value == null)
            {
                return;
            }
            SubShaders.Remove(value);
        }

        string IText.ToText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Shader \"{0}\" {", Name);
            sb.Append("}");


            return sb.ToString();
        }
    }
}
