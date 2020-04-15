#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 
#define UNITY_4
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VLights;

/*
 * VLight
 * Copyright Brian Su 2011-2015
*/

[ExecuteInEditMode()]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("V-Lights/VLight Image Effects")]
public class VLightInterleavedSampling : MonoBehaviour
{	
	public static bool renderingInterleaved = false;

	[SerializeField]
	private bool
		useInterleavedSampling = true;
	[SerializeField]
	private float
		ditherOffset = 0.02f;
	[SerializeField]
	private float
		blurRadius = 1.5f;
	[SerializeField]
	private int
		blurIterations = 1;
	[SerializeField]
	private int
		downSample = 4;
	[SerializeField]
	private Shader
		postEffectShader;
	[SerializeField]
	private Shader
		volumeLightShader;
	//
	private Camera _ppCameraGO = null;
	private LayerMask _volumeLightLayer;
	private Material _postMaterial;
	private RenderTexture interleavedBuffer;
	private Material PostMaterial
	{
		get
		{
			if(_postMaterial == null)
			{
				_postMaterial = new Material(postEffectShader);
				_postMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return _postMaterial;
		}
	}

	Camera _camera;
	Camera cam
	{
		get
		{
			if(_camera == null)
			{
				_camera = GetComponent<Camera>();
			}
			return _camera;
		}
	}

	private void OnEnable()
	{
		Init();
	}

	private void OnDisable()
	{
		CleanUp();
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		int downsampleFactor = Mathf.Clamp(downSample, 1, 20);
		blurIterations = Mathf.Clamp(blurIterations, 0, 20);

#if UNITY_4
		int width = (int)cam.pixelWidth;
		int height = (int)cam.pixelHeight;
		int dsWidth = (int)cam.pixelWidth / downsampleFactor;
		int dsHeight = (int)cam.pixelHeight / downsampleFactor;
#endif
#if UNITY_5
		int width = cam.pixelWidth;
		int height = cam.pixelHeight;
		int dsWidth = cam.pixelWidth / downsampleFactor;
		int dsHeight = cam.pixelHeight / downsampleFactor;
#endif
		
		// 4 samples for the interleaved buffer
		RenderTexture bufferA = RenderTexture.GetTemporary(dsWidth, dsHeight, 0);
		RenderTexture bufferB = RenderTexture.GetTemporary(dsWidth, dsHeight, 0);
		RenderTexture bufferC = RenderTexture.GetTemporary(dsWidth, dsHeight, 0);
		RenderTexture bufferD = RenderTexture.GetTemporary(dsWidth, dsHeight, 0);
//		bufferA.filterMode = FilterMode.Point;
//		bufferB.filterMode = FilterMode.Point;
//		bufferC.filterMode = FilterMode.Point;
//		bufferD.filterMode = FilterMode.Point;
		
		if(interleavedBuffer != null && (interleavedBuffer.width != width || interleavedBuffer.height != height))
		{
			if(Application.isPlaying)
			{
				Destroy(interleavedBuffer);
			}
			else
			{
				DestroyImmediate(interleavedBuffer);
			}
			interleavedBuffer = null;
		}		
		
		if(interleavedBuffer == null)
		{
			interleavedBuffer = new RenderTexture(width, height, 0);

			interleavedBuffer.hideFlags = HideFlags.HideAndDontSave;
		}
		
		Camera ppCamera = GetPPCamera();
		ppCamera.CopyFrom(cam);
		ppCamera.enabled = false;
		ppCamera.depthTextureMode = DepthTextureMode.None;
		ppCamera.clearFlags = CameraClearFlags.SolidColor;
		ppCamera.cullingMask = _volumeLightLayer;
		ppCamera.backgroundColor = Color.clear;
		ppCamera.renderingPath = RenderingPath.VertexLit;

		renderingInterleaved = false;

		if(useInterleavedSampling)
		{

			// For odd projection matrices
			ppCamera.projectionMatrix = cam.projectionMatrix;
			ppCamera.pixelRect = new Rect(
				0,
				0,
				cam.pixelWidth / cam.rect.width + Screen.width / cam.rect.width,
				cam.pixelHeight / cam.rect.height + Screen.height / cam.rect.height);
			
			// Render the interleaved samples
			float offset = 0.0f;
			RenderSample(offset, ppCamera, bufferA);
			renderingInterleaved = true;

			offset += ditherOffset;
			RenderSample(offset, ppCamera, bufferB);
			offset += ditherOffset;
			RenderSample(offset, ppCamera, bufferC);
			offset += ditherOffset;
			RenderSample(offset, ppCamera, bufferD);
			
			//Combine the 4 samples to make an interleaved image and the edge border
			PostMaterial.SetTexture("_MainTexA", bufferA);
			PostMaterial.SetTexture("_MainTexB", bufferB);
			PostMaterial.SetTexture("_MainTexC", bufferC);
			PostMaterial.SetTexture("_MainTexD", bufferD);
			interleavedBuffer.DiscardContents();
			Graphics.Blit(null, interleavedBuffer, PostMaterial, 0);
		}
		else
		{
			ppCamera.projectionMatrix = cam.projectionMatrix;
			ppCamera.pixelRect = new Rect(
				0,
				0,
				cam.pixelWidth / cam.rect.width + Screen.width / cam.rect.width,
				cam.pixelHeight / cam.rect.height + Screen.height / cam.rect.height);
			
			RenderSample(0, ppCamera, bufferA);
			Graphics.Blit(bufferA, interleavedBuffer);
		}

		renderingInterleaved = false;
		
		//Blur the result
		RenderTexture pingPong = RenderTexture.GetTemporary(width, height, 0);
		pingPong.DiscardContents();

		PostMaterial.SetFloat("_BlurSize", blurRadius);
		for(int i = 0; i < blurIterations; i++)
		{
			Graphics.Blit(interleavedBuffer, pingPong, PostMaterial, 1);
			interleavedBuffer.DiscardContents();
			Graphics.Blit(pingPong, interleavedBuffer, PostMaterial, 2);
			pingPong.DiscardContents();
		}
		RenderTexture.ReleaseTemporary(pingPong);
		RenderTexture.ReleaseTemporary(bufferA);
		RenderTexture.ReleaseTemporary(bufferB);
		RenderTexture.ReleaseTemporary(bufferC);
		RenderTexture.ReleaseTemporary(bufferD);

		PostMaterial.SetTexture("_MainTexBlurred", interleavedBuffer);
		Graphics.Blit(source, destination, PostMaterial, 3);
	}

	private void RenderSample(float offset, Camera ppCamera, RenderTexture buffer)
	{
		Shader.SetGlobalFloat("_InterleavedOffset", offset);
		ppCamera.targetTexture = buffer;
		ppCamera.SetReplacementShader(volumeLightShader, "RenderType");
		ppCamera.Render();
	}

	private void Init()
	{
		if(LayerMask.NameToLayer(VLightManager.VOLUMETRIC_LIGHT_LAYER_NAME) == -1)
		{
			Debug.LogWarning(VLightManager.VOLUMETRIC_LIGHT_LAYER_NAME + " layer does not exist! Cannot use interleaved sampling please add this layer.");
			return;
		}

		if(!SystemInfo.supportsImageEffects)
		{
			Debug.LogWarning("Cannot use interleaved sampling. Image effects not supported");
			return;
		}

		_volumeLightLayer = 1 << LayerMask.NameToLayer(VLightManager.VOLUMETRIC_LIGHT_LAYER_NAME);

		cam.cullingMask &= ~_volumeLightLayer;
		cam.depthTextureMode |= DepthTextureMode.DepthNormals;

		if(postEffectShader == null)
		{
			postEffectShader = Shader.Find(VLightShaderUtil.POST_SHADER_NAME);
		}

		if(volumeLightShader == null)
		{
			volumeLightShader = Shader.Find(VLightShaderUtil.INTERLEAVED_SHADER_NAME);
		}
	}

	private void CleanUp()
	{
		cam.cullingMask |= _volumeLightLayer;
		if(Application.isEditor)
		{
			DestroyImmediate(_postMaterial);
			
			if(interleavedBuffer != null)
			{
				DestroyImmediate(interleavedBuffer);
			}
		}
		else
		{
			Destroy(_postMaterial);
			
			if(interleavedBuffer != null)
			{
				Destroy(interleavedBuffer);
			}
		}
	}

	private Camera GetPPCamera()
	{
		if(_ppCameraGO == null)
		{
			var go = GameObject.Find("Post Processing Camera");
			if(go != null && go.GetComponent<Camera>() != null)
			{
				_ppCameraGO = go.GetComponent<Camera>();
			}
			else
			{
				var newGO = new GameObject("Post Processing Camera");
				_ppCameraGO = newGO.AddComponent<Camera>();
				_ppCameraGO.enabled = false;
				newGO.hideFlags = HideFlags.HideAndDontSave;
			}
		}
		return _ppCameraGO;
	}
}

