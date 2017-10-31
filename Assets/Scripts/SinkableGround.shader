Shader "Custom/SinkableGround" 
{
	Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SinkingPosition ("Sinking Position", Vector) = (0,0,0,1)
		_Accuracy ("Accuracy", float) = 5.0
		_Amount ("Sinking Amoung", float) = 1.0
	}

	SubShader 
	{
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		
		#pragma surface surf Lambert vertex:vert
       
		struct Input 
		{
			float2 uv_MainTex;
		};
 
		float _Amount;
		float _Accuracy;
		float4 _SinkingPosition;
		sampler2D _MainTex;

		void vert (inout appdata_full v) {
			if (v.vertex - _SinkingPosition < _Accuracy)
			{
				v.vertex.y += _Amount;
			}
			else
			{
				v.vertex.y -= _Amount;
			}
		}
 
		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
		}
		
		ENDCG
	}
}