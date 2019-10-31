using System;

namespace Assets.Editor.ShaderLab
{
    /// <summary>
    ///  变量类型
    /// </summary>
    public enum MemberType
    {
        Float,
        Float2,
        Float3,
        Float4,
        Half,
        Half2,
        Half3,
        Half4,
        Fixed,
        Fixed2,
        Fixed3,
        Fixed4,
        Sampler2D,
        Sampler3D,
        SamplerCube,
    };

    /// <summary>
    /// 代码成员变量
    /// </summary>
    public class ProgramMember
    {
        public MemberType Type { get; set; }
        public string InternalName { get; set; }
    }

    public class ShaderProgram
    {
        public ShaderProgram()
        {
        }
    }
}
