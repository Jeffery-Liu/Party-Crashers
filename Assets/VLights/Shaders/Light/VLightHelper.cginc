#if _CURVE_ON
sampler2D _FallOffTex;
#endif

#if _DITHER_ON
float4 _JitterAmount;
sampler2D _DitherTex;
#endif

inline fixed4 computeFragSpot (v2f i) 
{	
	#if _DITHER_ON
	float2 sp = 0.5 * _ScreenParams.xy * (i.screenPos.xy / i.screenPos.w);
  	float jitter = tex2D(_DitherTex, float2(sp.x, sp.y)).r - 0.5;
  	jitter *= _JitterAmount;
  	i.positionV.z += jitter;
  	i.tcProj.w += jitter;
  	i.tcProjScroll.w += jitter;
  	#endif

	float _LightNearRange = _LightParams.x;
	float _LightFarRange = _LightParams.y;
	float _Range = _LightParams.z;
	float _Fov = _LightParams.w;

	half noise = tex2Dproj(_NoiseTex, i.tcProjScroll).r;
	const float3 lightDir = float3(0.0f, 0.0f, -1.0);
	float spotEffect = dot(lightDir, normalize(i.positionV.xyz));	
	float attenuation = 0.0f;
	float dist = (-i.positionV.z - _LightNearRange) / _Range;

	//clip if greater than FOV	
	clip(_Fov < acos(spotEffect) ? -1 : 1);
	
	spotEffect = pow(spotEffect, _SpotExp);
	
	#if _CURVE_ON
	float4 fallOff = tex2D(_FallOffTex, float2(dist, 0.5));	
	attenuation = fallOff.a;
	#else
	attenuation = spotEffect / (_ConstantAttn + _LinearAttn * dist + _QuadAttn * (dist * dist));
	#endif

	#if _SHADOW_ON
	float distShadow = -i.positionV.z / _LightFarRange;
	float shadowMapDepth = tex2Dproj(_ShadowTexture, i.tcProj).r; 
	clip(shadowMapDepth - distShadow);
	#endif

	half4 color = tex2Dproj(_LightColorEmission, i.tcProj);	
	
	#if _CURVE_ON
	half3 Albedo = fallOff.rgb;
	half Alpha = (noise * attenuation) * _Strength;
	#else
	half3 Albedo = color.rgb * _Color.rgb;
	half Alpha = (noise * attenuation) * _Strength * _Color.a;
	#endif

	return half4(Albedo * Alpha, 1);
}
