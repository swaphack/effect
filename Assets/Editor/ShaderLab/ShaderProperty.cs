using System;

namespace Assets.Editor.ShaderLab
{
    /// <summary>
    ///  属性类型
    /// </summary>
    public enum PropertyType
    {
        Int,
        Float,
        Range,
        Color,
        Texture2D,
        Texture3D,
        Vector,
        Cube,
        Rect
    };

    public enum TexDefaultName
    {
        None,
        White,
        Black,
        Gray,
        Bump,
    }

    public enum TexGen
    {
        ObjectLinear,
        EyeLinear,
        SphereMap,
        CubeReflect,
        CubeNormal
    };

    public struct Range
    {
        public float Min;
        public float Max;
    }

    public class ShaderProperty : IText
    {
        public string InternalName { get; set; }
        public string InspectorTitle { get; set; }
        public PropertyType Type { get; set; }
        public object DefaultValue { get; set; }

        public string ToText()
        {
            throw new NotImplementedException();
        }
    }
}
