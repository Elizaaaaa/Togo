// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/white_shader" {
	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex: POSITION;
			};

			struct v2f
			{
				float4 vertex: SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float4 frag(v2f i): SV_Target
			{
				return float4(1, 0.3, 0.6, 1);
			}
				ENDCG

		}
	}
}
