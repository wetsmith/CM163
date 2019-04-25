// vertex displacement shader
// contorts while doing rotation
Shader "Custom/shader2"
{
	Properties
	{
		_Speed("Speed", Float) = 1.0
		_Twistiness("Twistiness", Float) = 1.0
	}
	SubShader
	{

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform float _Speed;
			uniform float _Twistiness;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 radNormal : NORMAL;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 radNormal : NORMAL;
				float timeVal : Float;
			};

			float3x3 getRotationMatrixX(float theta)
			{
				float s = -sin(theta);
				float c = cos(theta);
				return float3x3(1, 0, 0, 0, c, -s, 0, s, c);
			}

			float3x3 getRotationMatrixY(float theta)
			{
				float s = -sin(theta);
				float c = cos(theta);
				return float3x3(c, 0, s, 0, 1, 0, -s, 0, c);
			}
			
			float3x3 getRotationMatrixZ(float theta)
			{
				float s = -sin(theta);
				float c = cos(theta);
				return float3x3(c, -s, 0, s, c, 0, 0, 0, 1);
			}

			v2f vert(appdata v)
			{
				v2f o;
				const float PI = 3.14159;

				float radNormal = fmod(_Time.y * _Speed, PI*2.0);
				float radTwist = sin(_Time.y * _Speed);

				float useTwist = 10.0;

				if (_Twistiness >= 10.0)
				{
					useTwist = 0.01;
				}
				else if (_Twistiness <= 0)
				{
					useTwist = 10;
				}
				else
				{
					useTwist = 10 - _Twistiness;
				}

				float ct = cos(v.vertex.y / useTwist);
				float st = sin(v.vertex.y / useTwist);

				//float newX = v.vertex.x;
				float newX = v.vertex.x + (v.vertex.x * ct * radTwist - v.vertex.z * st * radTwist);
				float newY = v.vertex.y;
				//float newY = v.vertex.y + -(v.vertex.x * ct * radTwist - v.vertex.z * st * radTwist);
				
				//float newZ = v.vertex.z;
				float newZ = v.vertex.z + (v.vertex.x * st * radTwist + v.vertex.z * ct * radTwist);

				float3x3 rotationMatrix = getRotationMatrixX(radNormal);
				float3 rotatedVertex = mul(rotationMatrix, v.vertex.xyz);
				float4 xyz = float4(newX + rotatedVertex[1], newY + rotatedVertex[2], newZ + rotatedVertex[0], 1.0);

				o.vertex = UnityObjectToClipPos(xyz);
				o.radNormal = v.radNormal;

				return o;
			}

			float4 normalToColor(float3 n)
			{
				return float4((normalize(n) + 1) / 2.0, 1.0);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return normalToColor(i.radNormal);
			}
			ENDCG

		}
	}

}
