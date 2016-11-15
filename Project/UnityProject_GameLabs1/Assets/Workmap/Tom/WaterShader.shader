Shader "Custom/WaterShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_NormalMapOne ("Normal Map One", 2D) = "bump" {}
		_NormalMapTwo("Normal Map Two", 2D) = "bump" {}

		_ScrollSpeedX("Scroll Speed X", float) = 0.0
		_ScrollSpeedY("Scroll Speed Y", float) = 0.0

		_Slider("Slider", Range(0,1)) = 1.0

	}
	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalMapOne;
		sampler2D _NormalMapTwo;

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalMapOne;
			float2 uv_NormalMapTwo;
		};

		half _Glossiness;
		half _Metallic;
		half _Slider;
		fixed4 _Color;

		float _ScrollSpeedX;
		float _ScrollSpeedY;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed2 scrolledUV = IN.uv_NormalMapOne;

			fixed xScrolllValue = _ScrollSpeedX * _Time;
			fixed yScrolllValue = _ScrollSpeedY * _Time;

			scrolledUV += fixed2(xScrolllValue, yScrolllValue);

			// Albedo comes from a texture tinted by color
			//////fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = _Color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Slider;
			fixed3 w = UnpackNormal(tex2D(_NormalMapOne, scrolledUV));
			fixed3 wa = UnpackNormal(tex2D(_NormalMapTwo, IN.uv_NormalMapTwo));
			fixed3 n = normalize(fixed3(w.r + wa.r, w.g + wa.g, w.b));
			o.Normal = n;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
