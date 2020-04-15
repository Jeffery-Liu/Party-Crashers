Shader "V-Light/Volumetric Light Depth"
{
	Subshader
	{
		Tags { "RenderType" = "VLight" }
		Pass
		{
			Fog { Mode Off }
			ZWrite off
			Blend One One

			CGPROGRAM
			#pragma multi_compile __ _CURVE_ON
			#pragma multi_compile __ _SHADOW_ON
			#pragma multi_compile __ _DITHER_ON
			#pragma vertex vert
			#pragma fragment frag
			#if _DITHER_ON
			#pragma target 3.0
			#endif			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos :SV_POSITION;
				float4 tcProj : TEXCOORD0;
				float4 tcProjScroll : TEXCOORD1;
				float4 positionV : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
			};

			// x = near y = far z = far - near z = fov
			float4 _LightParams;
			float4 _minBounds;
			float4 _maxBounds;
			float4x4 _ViewWorldLight;
			float4x4 _Projection;
			float4x4 _Rotation;
			float4x4 _LocalRotation;

			// User
			sampler2D _NoiseTex;
			sampler2D _LightColorEmission;

			// Auto Set
			sampler2D _ShadowTexture;

			// Attenuation values
			float _SpotExp;
			float _ConstantAttn;
			float _LinearAttn;
			float _QuadAttn;

			// Light settings
			float _Strength;
			float4 _Color;

			//Global
			float _InterleavedOffset;

			v2f vert (appdata_full v) {
				v2f o;

				v.vertex -= float4(0, 0, _InterleavedOffset, _InterleavedOffset);

				const float4x4 scale = float4x4(
					0.5f, 0.0f, 0.0f, 0.5f,
					0.0f, 0.5f, 0.0f, 0.5f,
					0.0f, 0.0f, 0.5f, 0.5f,
					0.0f, 0.0f, 0.0f, 1.0f);

				float4x4 viewWorldLightProj = mul(_Projection, _ViewWorldLight);
				float4x4 lightProjection = mul(scale, viewWorldLightProj);
				float4x4 lightProjectionNoise = mul(scale, mul(_Rotation, viewWorldLightProj));

				float4 pos = _minBounds * v.vertex + _maxBounds * (1  - v.vertex);
				pos.w = 1;

				o.tcProj = mul(lightProjection, pos);
				o.tcProjScroll = mul(lightProjectionNoise, pos);

				o.pos = mul(UNITY_MATRIX_P, pos);
				o.positionV = mul(_ViewWorldLight, pos);
				o.positionV.w = -pos.z * _ProjectionParams.w;

				o.screenPos = ComputeScreenPos(o.pos);
				return o;
			}

			#include "VLightHelper.cginc"

			sampler2D _CameraDepthNormalsTexture;
			half4 frag (v2f i) : COLOR
			{
				float partZ = i.positionV.w;
				half sceneZ = UNITY_SAMPLE_DEPTH(DecodeFloatRG(tex2Dproj(_CameraDepthNormalsTexture, UNITY_PROJ_COORD(i.screenPos)).zw));
				clip(sceneZ - partZ);
				return computeFragSpot(i);
			}
			ENDCG
		}
	}

	Subshader
	{
		Tags { "RenderType" = "VLightPoint" "Queue"="Transparent" }
		Pass
		{
			Fog { Mode Off }
			ZWrite off
			Blend One One

			CGPROGRAM
			#pragma multi_compile __ _CURVE_ON
			#pragma multi_compile __ _SHADOW_ON
			#pragma multi_compile __ _DITHER_ON			
			#pragma vertex vert
			#pragma fragment frag
			#if _DITHER_ON
			#pragma target 3.0
			#endif			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos :SV_POSITION;
				float4 positionV : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
			};

			// x = near y = far z = far - near z = fov
			float4 _LightParams;
			float4 _minBounds;
			float4 _maxBounds;
			float4x4 _ViewWorldLight;
			float4x4 _Rotation;
			float4x4 _LocalRotation;

			// User
			samplerCUBE _NoiseTex;
			samplerCUBE _LightColorEmission;

			// Auto Set
			samplerCUBE _ShadowTexture;
			sampler2D _CameraDepthTexture;

			// Attenuation values
			float _SpotExp;
			float _ConstantAttn;
			float _LinearAttn;
			float _QuadAttn;

			// Light settings
			float _Strength;
			float4 _Color;

			//Global
			float _InterleavedOffset;


			v2f vert (appdata_full v) {
				v2f o;
				v.vertex -= float4(0, 0, _InterleavedOffset, 0);

				float4 pos = _minBounds * v.vertex + _maxBounds * (1  - v.vertex);
				pos.w = 1;

				o.pos = mul(UNITY_MATRIX_P, pos);
				o.positionV = mul(_ViewWorldLight, pos);
				o.positionV.w = -pos.z * _ProjectionParams.w;

				o.screenPos = ComputeScreenPos(o.pos);
				return o;
			}

			#include "VLightHelperPoint.cginc"

			sampler2D _CameraDepthNormalsTexture;
			half4 frag (v2f i) : COLOR
			{
				float partZ = i.positionV.w;
				half sceneZ = UNITY_SAMPLE_DEPTH(DecodeFloatRG(tex2Dproj(_CameraDepthNormalsTexture, UNITY_PROJ_COORD(i.screenPos)).zw));
				
				clip(sceneZ - partZ);
				return computeFragPoint(i);
			}
			ENDCG
		}
	}

	// AREA
	Subshader
	{
		Tags { "RenderType" = "VLightArea" "Queue"="Transparent" "IgnoreProjector"="true" }
		Pass
		{
			Fog { Mode Off }
			ZWrite off
			Blend One One

			CGPROGRAM
			#pragma multi_compile _SHAPE_CUBE _SHAPE_SPHERE _SHAPE_ROUNDED_CUBE _SHAPE_CYLINDER
			#pragma multi_compile __ _DITHER_ON			
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos :SV_POSITION;
				float4 positionV : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
			};

			// x = near y = far z = far - near z = fov
			float4 _LightParams;
			float4 _minBounds;
			float4 _maxBounds;
			float4x4 _ViewWorldLight;
			float4x4 _Rotation;
			float4x4 _LocalRotation;

			// User
			samplerCUBE _NoiseTex;
			samplerCUBE _LightColorEmission;

			// Attenuation values
			float _SpotExp;
			float _ConstantAttn;
			float _LinearAttn;
			float _QuadAttn;

			// Light settings
			float _Strength;
			float4 _Color;
			
			float4 _CameraDepthNormalsTexture_TexelSize;

			//Global
			float _InterleavedOffset;

			v2f vert (appdata_full v) {
				v2f o;
				v.vertex -= float4(0, 0, _InterleavedOffset, 0);

				float4 pos = _minBounds * v.vertex + _maxBounds * (1  - v.vertex);
				pos.w = 1;

				o.pos = mul(UNITY_MATRIX_P, pos);
				o.positionV = mul(_ViewWorldLight, pos);
				o.positionV.w = -pos.z * _ProjectionParams.w;

				o.screenPos = ComputeScreenPos(o.pos);
				return o;
			}

			#include "VLightHelperArea.cginc"

			sampler2D _CameraDepthNormalsTexture;
			half4 frag (v2f i) : COLOR
			{
				float partZ = i.positionV.w;
				
				half sceneZ = UNITY_SAMPLE_DEPTH(DecodeFloatRG(tex2Dproj(_CameraDepthNormalsTexture, UNITY_PROJ_COORD(i.screenPos)).zw));
				
				clip(sceneZ - partZ);
				fixed4 col = computeFrag(i);
				return col;
			}
			ENDCG
		}
	}

	Fallback Off
}

