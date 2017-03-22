Shader "Custom/NailShader" {
	Properties{
		_Color("Color", Color) = (0,0,1,0.5)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Bump("Bump",2D) = "bump"{}
		_TopColor("TopColor", Color) = (0,1,0,0.0)
		_BottomColor("BottomColor", Color) = (1,0,0,0)
	}
		SubShader{
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
			LOD 200

			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows alpha vertex:vert
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _Bump;
			struct Input {
				float2 uv_MainTex;
				float2 uv_Bump;
				float3 vertPos;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			fixed4 _TopColor;
			fixed4 _BottomColor;
			fixed _RimWidth;

			void vert(inout appdata_full v,out Input o) {
				UNITY_INITIALIZE_OUTPUT(Input, o);
				o.vertPos = v.vertex;
			}

			void surf(Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb*(IN.vertPos.y);
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
				if (c.r - c.b > 0.05 && c.r - c.g > 0.05)
				{
					o.Emission = _TopColor.rgb*(IN.vertPos.y)*0.5 + _TopColor.rgb*(IN.vertPos.y)*0.5;
				}
				//clamp
			}
			ENDCG
		}
			FallBack "Diffuse"
}
