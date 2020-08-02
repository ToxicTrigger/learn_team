// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Trail"
{
	Properties
	{
		_Emission("Emission", Float) = 1
		_speed("speed", Float) = -1
		_MainTex("MainTex", 2D) = "white" {}
		_Main_X("Main_X", Float) = 1
		_Main_Y("Main_Y", Float) = 1
		_MaskTex("MaskTex", 2D) = "white" {}
		_Mask_X("Mask_X", Float) = 1
		_Mask_Y("Mask_Y", Float) = 1
		_MaskRange("MaskRange", Float) = 4.17
		_Mask_Power("Mask_Power", Float) = 1.13
		_Mask_Base("Mask_Base", Float) = 0
		_Float0("Float 0", Float) = -1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _MainTex;
		uniform float _speed;
		uniform float _Main_X;
		uniform float _Main_Y;
		uniform float _Emission;
		uniform sampler2D _MaskTex;
		uniform float _Float0;
		uniform float _Mask_X;
		uniform float _Mask_Y;
		uniform float _Mask_Base;
		uniform float _MaskRange;
		uniform float _Mask_Power;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult12 = (float2(_speed , 0.0));
			float2 appendResult15 = (float2(_Main_X , _Main_Y));
			float2 uv_TexCoord8 = i.uv_texcoord * appendResult15;
			float2 panner10 = ( _Time.y * appendResult12 + uv_TexCoord8);
			float4 tex2DNode1 = tex2D( _MainTex, panner10 );
			o.Emission = ( tex2DNode1.r * i.vertexColor * _Emission ).rgb;
			float2 appendResult45 = (float2(_Float0 , 0.0));
			float2 appendResult22 = (float2(_Mask_X , _Mask_Y));
			float2 uv_TexCoord20 = i.uv_texcoord * appendResult22;
			float2 panner32 = ( _Time.y * appendResult45 + uv_TexCoord20);
			o.Alpha = ( i.vertexColor.a * saturate( ( tex2DNode1.a - ( ( tex2D( _MaskTex, panner32 ).r + _Mask_Base ) * saturate( pow( ( uv_TexCoord20.x + 0.0 ) , _MaskRange ) ) * _Mask_Power ) ) ) );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 

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
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
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
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
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
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.vertexColor = IN.color;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
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
Version=18200
1920;15;1920;1007;2242.748;231.8258;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;24;-1818.324,445.6846;Inherit;False;Property;_Mask_Y;Mask_Y;7;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-1822.024,344.9846;Inherit;False;Property;_Mask_X;Mask_X;6;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-1671.163,618.682;Inherit;False;Property;_Float0;Float 0;11;0;Create;True;0;0;False;0;False;-1;-1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;22;-1664.624,381.2846;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TimeNode;18;-1477.06,188.8057;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-1924.5,-159;Inherit;False;Property;_Main_X;Main_X;3;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-1512.402,398.6939;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-1924.5,-73.5;Inherit;False;Property;_Main_Y;Main_Y;4;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;45;-1503.163,568.682;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1640.5,17.5;Inherit;False;Property;_speed;speed;1;0;Create;True;0;0;False;0;False;-1;-4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-1386.076,1002.097;Inherit;False;Property;_MaskRange;MaskRange;8;0;Create;True;0;0;False;0;False;4.17;1.58;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-1341.477,728.4913;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;-1758.8,-138.9;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;32;-1211.264,370.3089;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-858.3623,256.3005;Inherit;False;Property;_Mask_Base;Mask_Base;10;0;Create;True;0;0;False;0;False;0;0.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;12;-1460.5,28.5;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;28;-1097.472,794.0928;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-1008.4,375.3993;Inherit;True;Property;_MaskTex;MaskTex;5;0;Create;True;0;0;False;0;False;-1;33925a1ad59292247accc773a89de8e0;c1d44e5d11f684249a37f36aace89c57;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-1594.5,-146.5;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;40;-878.3623,640.3005;Inherit;False;Property;_Mask_Power;Mask_Power;9;0;Create;True;0;0;False;0;False;1.13;2.23;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-692.3623,365.3005;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;10;-1189.5,-30.5;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;30;-880.1671,771.8922;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-832.5,-92.5;Inherit;True;Property;_MainTex;MainTex;2;0;Create;True;0;0;False;0;False;-1;0bc3a761d9d555a45a262d693fee7f46;0bc3a761d9d555a45a262d693fee7f46;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-679.759,475.5891;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;25;-466.5773,325.7211;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;26;-286.145,289.5199;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;2;-510.5,81.5;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;44;-414.4186,-41.18838;Inherit;False;Property;_Emission;Emission;0;0;Create;True;0;0;False;0;False;1;2.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-231.7,-24.5;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-114.5,225.5;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;113,-1;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Trail;False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;22;0;23;0
WireConnection;22;1;24;0
WireConnection;20;0;22;0
WireConnection;45;0;46;0
WireConnection;27;0;20;1
WireConnection;15;0;16;0
WireConnection;15;1;17;0
WireConnection;32;0;20;0
WireConnection;32;2;45;0
WireConnection;32;1;18;2
WireConnection;12;0;13;0
WireConnection;28;0;27;0
WireConnection;28;1;29;0
WireConnection;7;1;32;0
WireConnection;8;0;15;0
WireConnection;41;0;7;1
WireConnection;41;1;42;0
WireConnection;10;0;8;0
WireConnection;10;2;12;0
WireConnection;10;1;18;2
WireConnection;30;0;28;0
WireConnection;1;1;10;0
WireConnection;37;0;41;0
WireConnection;37;1;30;0
WireConnection;37;2;40;0
WireConnection;25;0;1;4
WireConnection;25;1;37;0
WireConnection;26;0;25;0
WireConnection;3;0;1;1
WireConnection;3;1;2;0
WireConnection;3;2;44;0
WireConnection;4;0;2;4
WireConnection;4;1;26;0
WireConnection;0;2;3;0
WireConnection;0;9;4;0
ASEEND*/
//CHKSM=FEA4C7872AC82031E29EDB5F339E2552051BF6C1