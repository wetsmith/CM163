// textured + lighting reaction shader

Shader "Custom/shader3"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white"{}
		_Color("Color", Color) = (1,1,1,1)
		_Shininess("Shininess", Float) = 10
		_SpecColor("SpecularColor", Color) = (1,1,1,1)
	}
	SubShader
	{
		Pass
		{
			Tags {"LightMode" = "ForwardAdd" }
			LOD 200
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float4 _LightColor0;
			float4 _Color;
			float4 _SpecColor;
			float4 _Shininess;
			sampler2D _MainTex;

			struct Input
			{
				float4 position: POSITION;
				float3 normal: NORMAL;
				float2 uv: TEXCOORD0;
			};

			struct Output
			{
				float4 position: SV_POSITION;
				float3 normal: NORMAL;
				float3 vertInWorldCoords: float3;
				float2 uv: TEXCOORD0;
			};

			Output vert(Input v)
			{
				Output o;
				o.vertInWorldCoords = mul(unity_ObjectToWorld, v.position);
				o.position = UnityObjectToClipPos(v.position);
				o.normal = v.normal;
				o.uv = v.uv;
				return o;
			}

			float4 frag(Output i) : SV_TARGET
			{
				float3 Ka = float3(1,1,1);
				float3 globalAmbient = float3(.5, .5, .5);
				float3 ambientComponent = Ka * globalAmbient;

				float3 P = i.vertInWorldCoords.xyz;
				float3 N = normalize(i.normal);
				float3 L = normalize(_WorldSpaceLightPos0.xyz - P);
				float3 Kd = _Color.rgb;
				float3 lightColor = _LightColor0.rgb;
				float3 diffuseComponent = Kd * lightColor*max(dot(N, L), 0);

				float3 Ks = _SpecColor.rgb;
				float3 V = normalize(_WorldSpaceCameraPos - P);
				float3 H = normalize(L + V);
				float3 specularComponent = Ks * lightColor*pow(max(dot(N, H), 0), _Shininess);

				float3 finalColor = ambientComponent + diffuseComponent + specularComponent;

				return float4 (finalColor, 1.0) * tex2D(_MainTex, i.uv);

			}
				

			ENDCG
		}

	}
}


