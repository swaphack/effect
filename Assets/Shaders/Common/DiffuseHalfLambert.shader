
Shader "Custom/Common/DiffuseHalfLambert" 
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
                "LightModel" = "ForwardBase"
            }

            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members worldNormalDir)
#pragma exclude_renderers d3d11

            #include "lighting.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct a2v
            {
                float4 vertext : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float3 worldNormalDir : COLOR;
            };

            fixed4 _Diffuse;

            v2f vert(a2v v)
            {
                v2f f;
                f.position = UnityObjectToClipPos(v.vertext);
                f.worldNormalDir = (mul(v.normal, (float3x3)unity_WorldToObject));
                return f;
            }

            fixed4 frag(v2f f) : SV_Target
            {
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;
                fixed normalDir = normalize(f.worldNormalDir);
                fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfLambert = dot(normalDir, lightDir) * 0.5 + 0.5;

                fixed3 diffuse = _LightColor0 * halfLambert * _Diffuse;
                return fixed4(diffuse + ambient , 1);
            }

            ENDCG
        }
    } 
}
