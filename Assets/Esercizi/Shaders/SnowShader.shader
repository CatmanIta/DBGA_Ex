// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SnowShader" {
	Properties{
		_SnowMultiplier("Snow Multiplier", Range(0.1,10)) = 1
		_MainTex("Main Texture", 2D) = "white" {}
		_SnowTex("Snow Texture", 2D) = "white" {}
	}
	SubShader{
		Tags{
			"RenderType" = "Opaque"
			"LightMode" = "ForwardBase"
		}

		Pass{

		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag
		#include "UnityLightingCommon.cginc"

		struct vertInput{
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		struct vertOutput {
			float4 pos: SV_POSITION;
			float2 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};

		// Material properties
		sampler2D _MainTex;
		sampler2D _SnowTex;
		float _SnowMultiplier;

		vertOutput vert(vertInput input) {
			vertOutput o;

			o.normal = mul(UNITY_MATRIX_M, input.normal);

			float UdotN = dot(float3(0, 1, 0), o.normal);
			float snowAmount = saturate(UdotN) * _SnowMultiplier * 0.01f;

			// Extrude positions
			o.pos = input.pos;
			o.pos += float4(input.normal*snowAmount, 0);
			o.pos = UnityObjectToClipPos(o.pos);

			o.uv = input.uv;
			return o;
		}

		half4 frag(vertOutput output) : COLOR {
			float UdotN = dot(float3(0,1,0), output.normal);
			float snowAmount = saturate(UdotN* _SnowMultiplier);

			float NdotL = dot(output.normal, _WorldSpaceLightPos0);
			float value = (0.5*(NdotL)+0.5)*0.5;

			float4 snowColor = tex2D(_SnowTex, output.uv) * value * _LightColor0;
			float4 realColor = tex2D(_MainTex, output.uv) * value * _LightColor0;

			return lerp(realColor, snowColor, snowAmount);
		}

		ENDCG

		}
	}
	Fallback "Diffuse"
}
