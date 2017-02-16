Shader "Custom/ShowShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Bump ("Bump",2D) = "bump"{}
		_SnowColor ("Snow Color",Color) = (1,1,1,1)
		_SnowDirection("SnowDirection",Vector) = (0,1,0)
		_SnowDepth("Snow Depth",Range(0,0.1)) = 0.1
		_SnowLevel("Snow Level",Range(0,1)) = 0
		_SnowWetness("Snow Wetness",Range(0,1)) = 0.25
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma vertex vert
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Bump;
		float _SnowDepth;
		float4 _SnowColor;
		float4 _SnowDirection;
		float _SnowLevel;
		float _SnowWetness;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Bump;
			float3 worldNormal;
			INTERNAL_DATA
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));

			float difference = dot(WorldNormalVector(IN, o.Normal), _SnowDirection.xyz) - lerp(1, -1, _SnowLevel);
			difference = saturate(difference / _SnowWetness);
			o.Albedo = difference*_SnowColor.rgb + (1 - difference)*c;
			//if (difference > 0)
			//{
			//	o.Albedo = _SnowColor.rgb;
			//}
			//else
			//{
			//	o.Albedo = c.rgb;
			//}

		}


		void vert(inout appdata_full  v)
		{
			float4 sn = mul(UNITY_MATRIX_IT_MV, _SnowDirection);

			if (dot(v.normal, sn.xyz) >= lerp(1, -1, ((1- _SnowWetness)*_SnowLevel*2)/3))
			{
				v.vertex.xyz += (sn.xyz + v.normal)*_SnowDepth*_SnowLevel;
			}
		}

		ENDCG
	}
	FallBack "Diffuse"
}
