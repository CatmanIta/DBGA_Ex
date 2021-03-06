﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/FakeLightShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Pass{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		// Vertex properties
		struct vertInput {
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
		half4 _Color;
		sampler2D _MainTex;

		vertOutput vert(vertInput input) {
			vertOutput o;
			o.pos = UnityObjectToClipPos(input.pos);
			o.uv = input.uv;
			o.normal = mul(UNITY_MATRIX_M, input.normal); 
			return o;
		}

		half4 frag(vertOutput output) : COLOR {
			float NdotL = dot(output.normal, float3(0,-1,0));
			float value = (0.5*(1 - NdotL)+0.5)*0.5;
			return tex2D(_MainTex, output.uv) * _Color * value;
		}

		ENDCG
		}
	}
}
