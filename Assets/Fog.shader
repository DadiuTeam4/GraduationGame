Shader "Hidden/Fog"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			fixed4 frag (v2f i) : SV_Target
			{
				float depth = DecodeFloatRG(tex2D(_CameraDepthTexture, i.uv));
				float linearDepth = Linear01Depth(depth);
				linearDepth = max(0,(_Range - linearDepth) / _Range);
				float quadrictDepth = sqrt(sqrt(sqrt(depth)));
				quadrictDepth = max(0,(_Range - quadrictDepth) / _Range);
				float2 uvs = float2(_Time.w *_Speed, sin(_Time.w) * _Speed);
				float4 fog = tex2D(_Fog, i.uv * 1 + uvs)*1.5;

				fixed4 col = tex2D(_MainTex, i.uv);
				return lerp(col, float4(1,1,1,1), 1-linearDepth);
			}
			ENDCG
		}
	}
}
