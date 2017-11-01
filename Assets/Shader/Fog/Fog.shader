Shader "Hidden/Fog"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_Fog ("Fog", 2D) = "white" {}
		_Range ("Range", Range(0,0.5)) = 0
		_Speed ("Speed", Range(0,5)) = 0
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
				float2 uv_depth : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv_depth = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _Fog;
			sampler2D_float _CameraDepthTexture;
			float _Range;
			float _Speed;
			float4 _Color;

			fixed4 frag (v2f i) : SV_Target
			{
				float depth = DecodeFloatRG(tex2D(_CameraDepthTexture, i.uv));
				float linearDepth = Linear01Depth(depth);
				linearDepth = max(0,(_Range - linearDepth) / _Range);
				fixed4 col = tex2D(_MainTex, i.uv);
				return lerp(col, _Color, 1-linearDepth*linearDepth);
			}
			ENDCG
		}
	}
}
