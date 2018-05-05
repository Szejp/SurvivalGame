// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/GeometryHexes"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_Size("_Size", int) = 0
		_Range("_Range", float) = 0
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma geometry geom
#pragma fragment frag

#include "UnityCG.cginc"

			struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 uv : TEXCOORD0;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			float3 normal : NORMAL;
			float2 uv : TEXCOORD0;
			float3 worldPosition : TEXCOORD1;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		int _Size;
		float _Range;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			o.normal = v.normal;
			o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
			return o;
		}

		[maxvertexcount(10)]
		void geom(triangle v2f input[3], inout TriangleStream<v2f> OutputStream)
		{
			v2f test = (v2f)0;
			v2f test2 = (v2f)0;
			for (int b = 0; b < _Size; b++) {
				for (int i = 0; i < 3; i++)
				{
					float4 vert = input[i].vertex;
					test.vertex = vert + float4(_Range, 0, 0, 0);
						test.uv = input[i].uv;
						OutputStream.Append(test);
						OutputStream.RestartStrip();
						test.vertex = vert + float4(0, 0, _Range, 0);
						test.uv = input[i].uv;
						OutputStream.Append(test);
			/*			test.vertex = vert + float4(-_Range, 0, 0, 0);
						test.uv = input[i].uv;
						OutputStream.Append(test);
						test.vertex = vert + float4(0, 0, -_Range, 0);
						test.uv = input[i].uv;
						OutputStream.Append(test);*/
				}

				OutputStream.RestartStrip();

			}
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
				return col;
				}
					ENDCG
				}
	}
}
