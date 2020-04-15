

#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 
#define UNITY_4
#endif

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public partial class VLight : MonoBehaviour
{
	#if UNITY_EDITOR
	RenderTexture CreateBakedShadowTexture(LightTypes type)
	{
		RenderTexture bakedShadowMap = GenerateShadowMap(shadowMapRes);
		bakedShadowMap.isPowerOfTwo = true;
		switch(type)
		{
		case LightTypes.Point:
			bakedShadowMap.isCubemap = true;
			break;
		}
		
		if(type == LightTypes.Point && !bakedShadowMap.isCubemap && bakedShadowMap.IsCreated())
		{
			bakedShadowMap = GenerateShadowMap(shadowMapRes);
			bakedShadowMap.isPowerOfTwo = true;
			bakedShadowMap.isCubemap = true;
		}
		else if(type == LightTypes.Spot && bakedShadowMap.isCubemap && bakedShadowMap.IsCreated())
		{
			bakedShadowMap = GenerateShadowMap(shadowMapRes);
			bakedShadowMap.isPowerOfTwo = true;
			bakedShadowMap.isCubemap = false;
		}
		
		return bakedShadowMap;
	}
	
	public void RenderBakedShadowMap()
	{
		#if UNITY_4 || UNITY_5
		float far = cam.farClipPlane;
		#else
		float far = cam.far;		
		#endif		
		
		if(SystemInfo.supportsImageEffects)
		{
			cam.backgroundColor = Color.red;
			cam.clearFlags = CameraClearFlags.SolidColor;
			cam.depthTextureMode = DepthTextureMode.None;
			cam.renderingPath = RenderingPath.VertexLit;
			
			var bakedShadowMap = CreateBakedShadowTexture(lightType);

			AssetDatabase.CreateAsset(bakedShadowMap, "Assets/" + name + "-shadowmap-" + System.DateTime.Now.ToString("HH-MM-ss") + ".asset");

			if(RenderDepthShader != null)
			{
				switch(lightType)
				{
				case LightTypes.Spot:
					cam.targetTexture = bakedShadowMap;
					cam.projectionMatrix = CalculateProjectionMatrix();
					cam.RenderWithShader(RenderDepthShader, "RenderType");
					
					//Blur the result
					var pingPong = RenderTexture.GetTemporary(shadowMapRes, shadowMapRes, 0);
					pingPong.DiscardContents();
					PostMaterial.SetFloat("_BlurSize", shadowBlurSize);
					for(int i = 0; i < shadowBlurPasses; i++)
					{
						Graphics.Blit(_depthTexture, pingPong, PostMaterial, 1);
						bakedShadowMap.DiscardContents();
						Graphics.Blit(pingPong, _depthTexture, PostMaterial, 2);
						pingPong.DiscardContents();
					}
					
					spotShadow = bakedShadowMap;
					
					RenderTexture.ReleaseTemporary(pingPong);
					
					break;
				case LightTypes.Point:
					bakedShadowMap.isCubemap = true;
					
					cam.projectionMatrix = Matrix4x4.Perspective(90, 1.0f, 0.1f, far);
					cam.SetReplacementShader(RenderDepthShader, "RenderType");
					cam.RenderToCubemap(bakedShadowMap, 63);
					cam.ResetReplacementShader();
					
					pointShadow = bakedShadowMap;
					break;
				default:
					break;
				}
				
				shadowMode = ShadowMode.Baked;
			}
			else
			{
				Debug.LogWarning("Could not find depth shader. Cannot render shadows");
			}
		}
	}
	#endif
}
