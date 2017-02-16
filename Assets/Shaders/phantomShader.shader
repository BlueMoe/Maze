Shader "Custom/phantomShader" {
	Properties {
		_Color ("Color", Color) = (0,0,1,0.5)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Bump("Bump",2D) = "bump"{}
		_RimColor("_RimColor", Color) = (0.17,0.36,0.81,0.0)
		_RimWidth("_RimWidth", Range(0.6,9.0)) = 0.9
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Bump;
		struct Input {
			float2 uv_MainTex;
			float2 uv_Bump;
			float3 viewDir;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _RimColor;
		fixed _RimWidth;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = _Color;
			o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 0.2;
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = _RimColor.rgb * pow(rim, _RimWidth);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
