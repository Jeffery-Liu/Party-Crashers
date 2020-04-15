#if UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6
#define UNITY_4
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * VLight
 * Copyright Brian Su 2011-2015
*/

public static class VLightGeometryUtil
{
	private static Vector3[] _outputList = new Vector3[20];
	private static Vector3[] _inputList = new Vector3[20];

	public static void RecalculateFrustrumPoints(Camera camera, float aspectRatio, out Vector3[] _frustrumPoints)
	{
		
#if UNITY_4 || UNITY_5
		float far = camera.farClipPlane;
		float near = camera.nearClipPlane;
#else
		float far = camera.far;
		float near = camera.near;
#endif

		if(!camera.orthographic)
		{
			
#if UNITY_4 || UNITY_5
			float Hnear = 2 * Mathf.Tan((camera.fieldOfView * 0.5f) * Mathf.Deg2Rad) * near;
			float Wnear = Hnear * aspectRatio;

			float Hfar = 2 * Mathf.Tan((camera.fieldOfView * 0.5f) * Mathf.Deg2Rad) * far;
			float Wfar = Hfar * aspectRatio;
#else
			float Hnear = 2 * Mathf.Tan((camera.fov * 0.5f) * Mathf.Deg2Rad) * near;
			float Wnear = Hnear * aspectRatio;

			float Hfar = 2 * Mathf.Tan((camera.fov * 0.5f) * Mathf.Deg2Rad) * far;
			float Wfar = Hfar * aspectRatio;

#endif

			Vector3 fc = Vector3.forward * far;
			Vector3 ftl = fc + (Vector3.up * Hfar / 2) - (Vector3.right * Wfar / 2);
			Vector3 ftr = fc + (Vector3.up * Hfar / 2) + (Vector3.right * Wfar / 2);
			Vector3 fbl = fc - (Vector3.up * Hfar / 2) - (Vector3.right * Wfar / 2);
			Vector3 fbr = fc - (Vector3.up * Hfar / 2) + (Vector3.right * Wfar / 2);
			Vector3 nc = Vector3.forward * near;
			Vector3 ntl = nc + (Vector3.up * Hnear / 2) - (Vector3.right * Wnear / 2);
			Vector3 ntr = nc + (Vector3.up * Hnear / 2) + (Vector3.right * Wnear / 2);
			Vector3 nbl = nc - (Vector3.up * Hnear / 2) - (Vector3.right * Wnear / 2);
			Vector3 nbr = nc - (Vector3.up * Hnear / 2) + (Vector3.right * Wnear / 2);

			_frustrumPoints = new Vector3[8];
			_frustrumPoints[0] = ntl;
			_frustrumPoints[1] = ftl;
			_frustrumPoints[2] = ntr;
			_frustrumPoints[3] = ftr;
			_frustrumPoints[4] = nbl;
			_frustrumPoints[5] = fbl;
			_frustrumPoints[6] = nbr;
			_frustrumPoints[7] = fbr;
		}
		else
		{
			float halfOrthoSize = camera.orthographicSize * 0.5f;
			_frustrumPoints = new Vector3[8];
			_frustrumPoints[0] = new Vector3(-halfOrthoSize, halfOrthoSize, near);
			_frustrumPoints[1] = new Vector3(-halfOrthoSize, halfOrthoSize, far);
			_frustrumPoints[2] = new Vector3(halfOrthoSize, halfOrthoSize, near);
			_frustrumPoints[3] = new Vector3(halfOrthoSize, halfOrthoSize, far);
			_frustrumPoints[4] = new Vector3(-halfOrthoSize, -halfOrthoSize, near);
			_frustrumPoints[5] = new Vector3(-halfOrthoSize, -halfOrthoSize, far);
			_frustrumPoints[6] = new Vector3(halfOrthoSize, -halfOrthoSize, near);
			_frustrumPoints[7] = new Vector3(halfOrthoSize, -halfOrthoSize, far);
		}
	}

	public static Vector3[] ClipPolygonAgainstPlane(Vector3[] subjectPolygon, Plane[] planes)
	{
		int outCount = 0;
		int inCount = 0;

		Array.Copy(subjectPolygon, _outputList, subjectPolygon.Length);
		outCount = subjectPolygon.Length;

		foreach (Plane plane in planes)
		{
			Array.Copy(_outputList, _inputList, outCount);
			inCount = outCount;
			outCount = 0;

			if(inCount == 0)
			{
				continue;
			}

			Vector3 S = _inputList[inCount - 1];
			for(int i = 0; i < inCount; i++)
			{
				//Grab the edge
				Vector3 E = _inputList[i];
				bool dE = plane.GetSide(E);
				bool dS = plane.GetSide(S);
				if(dE)
				{
					if(!dS)
					{
						Vector3 output;
						if(ComputeIntersection(S, E, plane, 0, out output))
						{
							_outputList[outCount++] = output;
						}
						else
						{
						}
					}
					_outputList[outCount++] = E;
				}
				else if(dS)
				{
					Vector3 output;
					if(ComputeIntersection(S, E, plane, 0, out output))
					{
						_outputList[outCount++] = output;
					}
					else
					{
						_outputList[outCount++] = E;
					}
				}
				S = E;
			}

			if(outCount == 0)
			{
				continue;
			}
		}

		Vector3[] outArray = new Vector3[outCount];
		Array.Copy(_outputList, outArray, outCount);
		return outArray;
	}

	public static bool ComputeIntersection(Vector3 start, Vector3 end, Plane plane, float e, out Vector3 result)
	{
		Vector3 dir = start - end;
		float r0 = Vector3.Dot(plane.normal, start) + plane.distance;
		float r1 = Vector3.Dot(plane.normal, dir);
		float u = r0 / r1;

		if(Mathf.Abs(u) < e)
		{
			result = Vector3.zero; // Parrallel
		}
		else if((u > 0 && u < 1))
		{
			result = ((end - start) * u) + start;
			return true;
		}
		else
		{
			result = Vector3.zero; // Parrallel
		}
		return false;
	}

	public static Vector4 Vector4Multiply(Vector4 right, Vector4 left)
	{
		return new Vector4(
            right.x * left.x, 
            right.y * left.y, 
            right.z * left.z, 
            right.w * left.w);
	}

	public static Vector4 Vector4Frac(Vector4 vector)
	{
		return new Vector4(Frac(vector.x), Frac(vector.y), Frac(vector.z), Frac(vector.w));
	}

	public static float Frac(float value)
	{
		return value - Mathf.FloorToInt(value);
	}

	public static Color FloatToRGBA(float value)
	{
		Vector4 enc = new Vector4(1.0f, 255.0f, 65025.0f, 160581375.0f) * value;
		enc = Vector4Frac(enc);
		enc -= Vector4Multiply(new Vector4(enc.y, enc.z, enc.w, enc.w), new Vector4(1.0f / 255.0f, 1.0f / 255.0f, 1.0f / 255.0f, 0.0f));
		return enc;
	}

}

