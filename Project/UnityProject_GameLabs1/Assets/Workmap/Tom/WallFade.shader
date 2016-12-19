Shader "Custom/WallFade" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", 2D) = "white" {}
		_GlosSlider ("Smoothness Slider", Range(0,1)) = 0.5
		_Metallic ("Metallic", 2D) = "white" {}
		_MetaSlider ("Metallic Slider", Range(0,1)) = 0.0
		_Alpha ("Alpha Slider", Range(0,1)) = 1.0
		_Height("HeightMap", 2D) = "black" {}
		_Amount("Extrusion Amount", Range(-1,1)) = 0.0
	}
	SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows alpha vertex:vert

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Metallic;
		sampler2D _Glossiness;
		sampler2D _Height;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Metallic;
			float2 uv_Glossiness;
			float2 uv_Height;
		};	

		half _Amount;
		half _MetaSlider;
		half _GlosSlider;
		half _Alpha;
		fixed4 _Color;

		void vert(inout appdata_full v)
		{
			//fixed4 h = tex2D(_Height, v.uv_Height);
			v.vertex.xyz += v.normal * _Amount;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Metallic = tex2D(_Metallic, IN.uv_Metallic) * _MetaSlider;
			o.Smoothness = tex2D(_Glossiness, IN.uv_Glossiness) * _GlosSlider;
			o.Alpha = _Alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
