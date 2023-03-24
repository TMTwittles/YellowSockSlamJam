// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ShuttlePath_SHA"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_PathTexure("Path Texure", 2D) = "white" {}
		_Colour("Colour", Color) = (0.5188679,0.07538268,0.07538268,0)
		_Shadows("Shadows", Color) = (0.2264151,0.05211819,0.05211819,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _PathTexure;
		uniform float4 _Shadows;
		uniform float4 _Colour;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime39 = _Time.y * -0.7;
			float2 appendResult36 = (float2(( mulTime39 + i.uv_texcoord.x ) , i.uv_texcoord.y));
			float2 scrollingUvs29 = appendResult36;
			float2 temp_output_2_0_g3 = scrollingUvs29;
			float2 break6_g3 = temp_output_2_0_g3;
			float temp_output_25_0_g3 = ( pow( 0.5 , 3.0 ) * 0.1 );
			float2 appendResult8_g3 = (float2(( break6_g3.x + temp_output_25_0_g3 ) , break6_g3.y));
			float4 tex2DNode14_g3 = tex2D( _PathTexure, temp_output_2_0_g3 );
			float temp_output_4_0_g3 = 1.0;
			float3 appendResult13_g3 = (float3(1.0 , 0.0 , ( ( tex2D( _PathTexure, appendResult8_g3 ).g - tex2DNode14_g3.g ) * temp_output_4_0_g3 )));
			float2 appendResult9_g3 = (float2(break6_g3.x , ( break6_g3.y + temp_output_25_0_g3 )));
			float3 appendResult16_g3 = (float3(0.0 , 1.0 , ( ( tex2D( _PathTexure, appendResult9_g3 ).g - tex2DNode14_g3.g ) * temp_output_4_0_g3 )));
			float3 normalizeResult22_g3 = normalize( cross( appendResult13_g3 , appendResult16_g3 ) );
			float3 break21 = normalizeResult22_g3;
			float3 appendResult22 = (float3(break21.x , break21.y , break21.z));
			o.Normal = appendResult22;
			float4 tex2DNode1 = tex2D( _PathTexure, scrollingUvs29 );
			float4 lerpResult7 = lerp( _Shadows , _Colour , tex2DNode1);
			o.Albedo = lerpResult7.rgb;
			o.Emission = lerpResult7.rgb;
			o.Smoothness = 0.2;
			o.Alpha = 1;
			clip( ( tex2DNode1.a * 1.0 ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-319.0125,368.4131;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1446.587,-34.96461;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;2;-1703.587,-33.96461;Inherit;True;Property;_PathTexure;Path Texure;3;0;Create;True;0;0;0;False;0;False;a30d370179b3d9640ab2f0142eb8b67b;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.LerpOp;7;-705.8487,-279.2305;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;6;-1281.193,-421.6082;Inherit;False;Property;_Colour;Colour;4;0;Create;True;0;0;0;False;0;False;0.5188679,0.07538268,0.07538268,0;0.5188679,0.07538268,0.07538268,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-1285.952,-242.7155;Inherit;False;Property;_Shadows;Shadows;5;0;Create;True;0;0;0;False;0;False;0.2264151,0.05211819,0.05211819,0;0.2264151,0.05211819,0.05211819,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;10;-1026.798,26.90939;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;13;-1154.798,381.9094;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;11;-869.7979,210.9094;Inherit;True;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,1;False;3;COLOR;0,0,0,0;False;4;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1455.798,291.9094;Inherit;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;0;False;0;False;0.3176471;0;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;18;-691.2332,-33.66039;Inherit;True;NormalCreate;1;;3;e12f7ae19d416b942820e3932b56220f;0;4;1;SAMPLER2D;;False;2;FLOAT2;0,0;False;3;FLOAT;0.5;False;4;FLOAT;2;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;23;-275.1752,130.916;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;21;-425.1752,9.915955;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;22;-87.17517,14.91595;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;236,-6;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;ShuttlePath_SHA;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;True;0;False;Transparent;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-239.2696,225.8873;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;0;False;0;False;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-2207.188,269.5711;Inherit;False;scrollingUvs;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;31;-1647.269,162.0536;Inherit;False;29;scrollingUvs;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;32;-882.949,22.99968;Inherit;False;29;scrollingUvs;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-842.1752,106.916;Inherit;False;Constant;_Float2;Float 2;5;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;36;-2454.949,291.9997;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;34;-3060.949,278.9997;Inherit;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-2668.949,239.9997;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;39;-2940.949,110.9997;Inherit;False;1;0;FLOAT;-0.7;False;1;FLOAT;0
WireConnection;3;0;1;4
WireConnection;1;0;2;0
WireConnection;1;1;31;0
WireConnection;7;0;8;0
WireConnection;7;1;6;0
WireConnection;7;2;1;0
WireConnection;10;0;1;0
WireConnection;13;0;12;0
WireConnection;11;0;10;0
WireConnection;11;3;12;0
WireConnection;11;4;13;0
WireConnection;18;1;2;0
WireConnection;18;2;32;0
WireConnection;18;4;25;0
WireConnection;23;0;21;1
WireConnection;21;0;18;0
WireConnection;22;0;21;0
WireConnection;22;1;21;1
WireConnection;22;2;21;2
WireConnection;0;0;7;0
WireConnection;0;1;22;0
WireConnection;0;2;7;0
WireConnection;0;4;24;0
WireConnection;0;10;3;0
WireConnection;29;0;36;0
WireConnection;36;0;40;0
WireConnection;36;1;34;2
WireConnection;40;0;39;0
WireConnection;40;1;34;1
ASEEND*/
//CHKSM=D3AD7C40CA305CE5214A595C0BEFF902E93EC0A5