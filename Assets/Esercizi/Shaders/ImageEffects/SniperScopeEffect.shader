// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/SniperScopeEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_VisorTex("Visor" , 2D) = "white" {}
		_ScanLineTex("Scan Lines" , 2D) = "white" {}
		_NoiseTex("Noise" , 2D) = "white" {}

		_VisorColor("Visor Color", Color) = (1,1,1,1)
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
		sampler2D _VisorTex;
		sampler2D _ScanLineTex;
		sampler2D _NoiseTex;
		float4 _VisorColor;

		fixed4 frag (v2f i) : SV_Target
		{
			// Get the rendered color
			fixed4 noiseColor = tex2D(_NoiseTex, frac(i.uv+_Time.yz));
			fixed4 maskColor = tex2D(_VisorTex, i.uv);
			fixed4 scanLineColor = tex2D(_ScanLineTex, frac(i.uv + _Time.xy));

			// Distortion
			fixed4 col;
			float2 uv = i.uv - 0.5;
			float z = sqrt(1.0 - uv.x * uv.x - uv.y * uv.y);
			float a = 1.0 / (z * tan(1.18 * 0.5));
			float4 c;
			float2 uv2 = (uv * a) + 0.5;
			if (uv2.x >= 1 || uv2.y >= 1 || uv2.x <= 0 || uv2.y <= 0)
				col = float4(0, 0, 0, 1);
			else
				col = tex2D(_MainTex, uv2);

			// Compute the BLACK/WHITE corresponding value
			float lum = col.r*.3 + col.g*.59 + col.b*.11;

			float4 visorColor = lerp(float4(0, 0, 0, 0), _VisorColor, lum);

			if (maskColor.a || scanLineColor.a > 0) {
				return maskColor;
			}
			else {
				return  visorColor * noiseColor;
			}
		}
		ENDCG
		}
	}
}
