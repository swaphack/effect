// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/*
漫反射计算公式：

漫反射Diffuse颜色 = 直射光颜色 * max(0, cos(光源方向和法线方向夹角)) * 材质自身色彩

其中max(0, cos(光源方向和法线方向夹角))部分可以改用半兰伯特光照模型以增强背光面的光照效果。
*/
Shader "Custom/Common/DiffuseVertex" 
{
	Properties
	{
        // Shader properties
		_Diffuse ("Diffuse Color", Color) = (1,1,1,1)
	}
	SubShader
	{
        // Shader code
		Pass
        {
            Tags
            {
                "LightMode" = "ForwardBase"
            }

            CGPROGRAM
            #include "Lighting.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct a2v
            {
                float4 vertext : POSITION;
                float3 normal : NORMAL;
                float4 textcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 color : COLOR;
            };

            fixed4 _Diffuse;

            v2f vert(a2v v)
            {
                v2f f;
                f.position = UnityObjectToClipPos(v.vertext);

                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
                fixed3 normalDir = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 diffuse = _LightColor0 * max(0, dot(normalDir, lightDir)) * _Diffuse;

                f.color = diffuse + ambient;

                return f;
            }

            fixed4 frag(v2f f) : SV_Target
            {
                return fixed4(f.color, 1);
            }

            ENDCG
		}
	} 
}
