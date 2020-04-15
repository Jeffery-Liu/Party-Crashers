#if _CURVE_ON
sampler2D _FallOffTex;
#endif

#if _DITHER_ON
float _JitterAmount;
sampler2D _DitherTex;
#endif

inline fixed4 computeFragPoint (v2f i)
{
	#if _DITHER_ON
	float2 sp = 0.5 * _ScreenParams.xy * (i.screenPos.xy / i.screenPos.w);
	float jitter = tex2D(_DitherTex, float2(sp.x, sp.y)).r - 0.5;
	jitter *= _JitterAmount;
  	i.positionV.z += jitter;
  	#endif

	float _LightNearRange = _LightParams.x;
	float _LightFarRange = _LightParams.y;
	float _Range = _LightParams.z;
	float _Fov = _LightParams.w;

	float3 direction = i.positionV.xyz;
	float3 rotatedDirection = mul(direction, (float3x3)_Rotation).xyz;
	half noise = texCUBE(_NoiseTex, rotatedDirection).r;

	float dist = length(i.positionV.xyz) / _LightFarRange;
	float spotEffect = 1 - min(1, dist);
	float attenuation = 0.0f;
	spotEffect = pow(spotEffect, _SpotExp);

	#if _CURVE_ON
	float4 fallOff = tex2D(_FallOffTex, float2(1 - dist, 0.5));
	attenuation = fallOff.a * 0.5;
	#else
	attenuation = spotEffect / (_ConstantAttn + _LinearAttn * dist + _QuadAttn * (dist * dist));
	#endif

	#if _SHADOW_ON
	float shadowMapDepth = texCUBE(_ShadowTexture, direction.xyz).r;
	clip(shadowMapDepth - dist);
	#endif

	half4 color = texCUBE(_LightColorEmission, mul(normalize(direction), (float3x3)_LocalRotation));

	#if _CURVE_ON
	half3 Albedo = fallOff.rgb;
	half Alpha = (noise * attenuation) * _Strength;
	#else
	half3 Albedo = color.rgb * _Color.rgb;
	half Alpha = (noise * attenuation) * _Strength * _Color.a;
	#endif

	return fixed4(Albedo * Alpha, Alpha);
}
