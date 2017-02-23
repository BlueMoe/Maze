Shader "Custom/NailShader" {
	Properties
	{
		_UpColor("UpColor",color) = (1,0,0,1)
		_DownColor("DownColor",color) = (0,1,0,1)
		_Center("Center",range(-0.7,0.7)) = 0
		_R("R",range(0,0.5)) = 0.2
	}

		SubShader{

		pass {
		Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "Lighting.cginc"

			struct v2f {
				float4 pos : POSITION;
				float y : TEXCOORD0;
			};

			float4 _UpColor;
			float4 _DownColor;
			float _Center;
			float _R;

			v2f vert(appdata_base v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP,v.vertex);
				o.y = v.vertex.y;

				return o;
			}

			fixed4 frag(v2f v) :COLOR
			{

				float d = v.y - _Center;//融合带
				float s = abs(d);
				d = d / s;//正负值分别描述上半部分和下半部分，取值1和-1

				float f = s / _R; //范围>1：表示上下部分;范围<1:表示融合带
				f = saturate(f);
				d *= f;//表示全部[-1,1];范围>1：表示上部分；范围<1:表示融合带;范围<-1:表示下部分

				d = d / 2 + 0.5;//将范围控制到[0,1],因为颜色值返回就是[0,1]
				return lerp(_UpColor,_DownColor,d);
			}

		ENDCG
		}
	}
}