// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Common/Color" 
{
	Properties
	{
        // Shader properties
		_Color ("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
        // Shader code
		Pass
        {
            CGPROGRAM

            fixed4 _Color;

            #pragma vertex vert
            float4 vert(float4 v : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(v);
            }

            #pragma fragment frag

            fixed4 frag() :SV_Target
            {
                return _Color;
            }

            ENDCG
		}
	} 
}
