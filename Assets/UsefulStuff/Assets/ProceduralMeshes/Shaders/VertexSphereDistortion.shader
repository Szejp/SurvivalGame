Shader "Unlit/VertexSphereDistortion"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_SphereOrigin("SphereOrigin", Vector) = (0,0,0,0)
			_Range("Range", float) = 0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : NORMAL;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float3 _SphereOrigin;
				float _Range;

				bool isInRange(float x, float originX, float range) {
					return x > originX - range && x < originX + range;
				}

				v2f vert(appdata v)
				{
					v2f o;

					float sqrtEq = pow(_Range, 2) - pow(v.vertex.x - _SphereOrigin.x, 2) - pow(v.vertex.y - _SphereOrigin.y, 2);
					if (isInRange(v.vertex.x, _SphereOrigin.x, _Range) &&
						isInRange(v.vertex.y, _SphereOrigin.y, _Range) &&
						isInRange(v.vertex.z, _SphereOrigin.z, _Range) &&
						sqrtEq >= 0) {
						float newVertex = sqrt(sqrtEq) + _SphereOrigin.z;
						if (newVertex > v.vertex.z) {
							v.vertex.z = newVertex;
						}
					}

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					// sample the texture
					fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
		}
}
