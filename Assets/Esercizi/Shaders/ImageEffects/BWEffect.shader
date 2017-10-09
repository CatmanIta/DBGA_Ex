// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/NewImageEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_bwBlend("BW Blend", Range(0,1)) = 0.5
		_Quantizations("Quantizations", Range(0,10)) = 5
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
		// Material properties
		sampler2D _MainTex;
		float _bwBlend;
		float _Quantizations;

		fixed4 frag (v2f i) : SV_Target
		{
			// Get the rendered color
			fixed4 col = tex2D(_MainTex, i.uv);
		 
			// Compute the BLACK/WHITE corresponding value
			float lum = col.r*.3 + col.g*.59 + col.b*.11;

			float lum_quantized = round(lum * _Quantizations) / _Quantizations;

			float3 bw = float3(lum_quantized, lum_quantized, lum_quantized);

			// Lerp and output
			float4 result;

			if (col.r > 0.6 && col.g < 0.5 && col.b < 0.5) {
				result.rgb = col.rgb;
			}
			else {
				result.rgb = lerp(col.rgb, bw, _bwBlend);
			}

			return result;
		}
		ENDCG
		}
	}
}
