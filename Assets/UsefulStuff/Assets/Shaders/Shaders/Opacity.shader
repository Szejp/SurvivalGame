// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/Opacity"
{
	Properties
	{
		_Color0("Color 0", Color) = (0,0,0,0)
		_Opacity("Opacity", 2D) = "white" {}
		_NoiseStrenth("Noise Strenth", Float) = 5
		_ColorFactor("Color Factor", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
		[Header(Forward Rendering Options)]
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color0;
		uniform float _ColorFactor;
		uniform sampler2D _Opacity;
		uniform float _NoiseStrenth;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = ( _Color0 * _ColorFactor ).rgb;
			float mulTime76 = _Time.y * 0.3;
			float2 uv_TexCoord114 = i.uv_texcoord * float2( 1,1 ) + float2( 0,0 );
			float2 panner72 = ( uv_TexCoord114 + mulTime76 * float2( -2,0 ));
			float4 tex2DNode99 = tex2D( _Opacity, (panner72*1 + 0) );
			float4 appendResult123 = (float4(tex2DNode99.r , tex2DNode99.g , 0 , 0));
			float2 panner94 = ( uv_TexCoord114 + mulTime76 * float2( 0,-2 ));
			float4 tex2DNode117 = tex2D( _Opacity, (panner94*1 + 0) );
			float4 appendResult124 = (float4(tex2DNode117.r , tex2DNode117.g , 0 , 0));
			o.Alpha = tex2D( _Opacity, ( ( ( (appendResult123).xyzw + (appendResult124).xyzw ) * _NoiseStrenth ) + float4( 0,0,0,0 ) ).xy ).b;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=14501
0;92;1320;431;-338.5602;736.9233;1.432391;True;True
Node;AmplifyShaderEditor.RangedFloatNode;106;-1332.724,-609.6447;Float;False;Constant;_Float3;Float 3;2;0;Create;True;0;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;95;-1068.534,-366.1311;Float;False;Constant;_Vector1;Vector 1;3;0;Create;True;0;0,-2;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;76;-1119.19,-599.0363;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;77;-1084.476,-501.8345;Float;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;-2,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;114;-939.5693,-756.4403;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;72;-533.1786,-681.4039;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;94;-617.5068,-429.1405;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;103;-285.5114,-707.2183;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;105;-213.054,-343.4052;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;127;350.8475,-195.4276;Float;True;Property;_Opacity;Opacity;2;0;Create;True;0;None;c20408e0403090048b38bb64b04c2f47;False;white;Auto;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;99;27.73659,-693.8411;Float;True;Property;_OpacityTex;Opacity Tex;2;0;Create;True;0;None;c20408e0403090048b38bb64b04c2f47;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;117;20.30246,-420.9907;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;None;c20408e0403090048b38bb64b04c2f47;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;124;348.8891,-404.08;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;123;369.0076,-617.4105;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;119;514.6711,-426.7153;Float;True;True;True;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;118;538.6633,-631.8738;Float;False;True;True;True;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;120;821.355,-547.22;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;135;861.0355,-285.8694;Float;False;Property;_NoiseStrenth;Noise Strenth;4;0;Create;True;0;5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;134;1105.286,-409.9086;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;136;1067.944,-707.8939;Float;False;Property;_ColorFactor;Color Factor;5;0;Create;True;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;107;795.8683,-948.3817;Float;False;Property;_Color0;Color 0;0;0;Create;True;0;0,0,0,0;0.372549,1,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;140;1278.783,-482.3491;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;125;1418.084,-622.3257;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;None;c20408e0403090048b38bb64b04c2f47;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;112;610.4757,-753.7291;Float;False;Property;_Colorfactor;Color factor;1;0;Create;True;0;0;3.84;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;131;762.0576,-1178.171;Float;True;Property;_Laser;Laser;3;0;Create;True;0;None;b1ad92f735ad1ec47b0862832e83b931;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;1345.667,-812.8676;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;139;961.9438,-134.8422;Float;False;Property;_Panning; Panning;6;0;Create;True;0;0;-0.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;141;979.8276,-30.94417;Float;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;142;1183.364,-186.1964;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;129;1676.187,-803.8995;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Amplify/Opacity;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;True;False;Back;0;0;False;0;0;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;2;SrcAlpha;OneMinusSrcAlpha;2;SrcAlpha;OneMinusSrcAlpha;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;76;0;106;0
WireConnection;72;0;114;0
WireConnection;72;2;77;0
WireConnection;72;1;76;0
WireConnection;94;0;114;0
WireConnection;94;2;95;0
WireConnection;94;1;76;0
WireConnection;103;0;72;0
WireConnection;105;0;94;0
WireConnection;99;0;127;0
WireConnection;99;1;103;0
WireConnection;117;0;127;0
WireConnection;117;1;105;0
WireConnection;124;0;117;1
WireConnection;124;1;117;2
WireConnection;123;0;99;1
WireConnection;123;1;99;2
WireConnection;119;0;124;0
WireConnection;118;0;123;0
WireConnection;120;0;118;0
WireConnection;120;1;119;0
WireConnection;134;0;120;0
WireConnection;134;1;135;0
WireConnection;140;0;134;0
WireConnection;125;0;127;0
WireConnection;125;1;140;0
WireConnection;108;0;107;0
WireConnection;108;1;136;0
WireConnection;142;0;139;0
WireConnection;142;1;141;0
WireConnection;129;2;108;0
WireConnection;129;9;125;3
ASEEND*/
//CHKSM=1438646ECECA4AAFFB163F5A0890651663B6E041