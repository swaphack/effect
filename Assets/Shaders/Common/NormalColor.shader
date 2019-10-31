// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Common/NormalColor" 
{
	Properties
	{
	}
	SubShader
	{
        // Shader code
		Pass
        {
            CGPROGRAM

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
                float3 color : COLOR0;
            };

            v2f vert(a2v v)
            {
                v2f f;
                f.position = UnityObjectToClipPos(v.vertext);
                f.color = v.normal;
                return f;
            }

            fixed4 frag(v2f f) : SV_Target
            {
                return fixed4(f.color,1);
            }
            ENDCG
		}
	} 
}
