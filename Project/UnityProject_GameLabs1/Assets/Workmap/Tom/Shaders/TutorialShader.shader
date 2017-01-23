Shader "Tutorial Shader" {
	Properties{
		_EmColorO("Emission Color One", Color) = (0,0,0,0)
		_EmColorT("Emission Color Two", Color) = (0,0,0,0)
		_EmLayerO("Emission Layer One", 2D) = "white" {}
	_EmLayerT("Emission Layer Two", 2D) = "white" {}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _EmLayerO;
	sampler2D _EmLayerT;

	struct Input {
		float2 uv_EmLayerO;
	};

	fixed4 _EmColorO;
	fixed4 _EmColorT;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed3 e = tex2D(_EmLayerO, IN.uv_EmLayerO) * _EmColorO;
		fixed3 em = tex2D(_EmLayerT, IN.uv_EmLayerO) * _EmColorT;
		fixed3 emi = normalize(fixed3(e.rgb * em.rgb));
		o.Emission = emi.rgb;
		o.Albedo = emi.rgb;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
