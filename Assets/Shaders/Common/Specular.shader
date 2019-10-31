Shader "Custom/Common/Specular" 
{
	Properties
	{
        // Shader properties
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader
	{
        // Shader code
		Pass
        {
            #pragma vertex vert

            float4 vert(float4 v : POSITION) : SV_POSITION
            {
                return mul(UNITY_MATRIX_MVP, v);
            }

            fixed 4
		}
	} 
}
