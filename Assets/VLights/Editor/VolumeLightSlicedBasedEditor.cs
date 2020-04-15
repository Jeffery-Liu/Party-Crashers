using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/*
 * VLight
 * Copyright Brian Su 2011-2014
*/
[CustomEditor(typeof(VLight))]
[CanEditMultipleObjects]
public class VolumeLightSlicedBasedEditor : Editor
{
    public VLight Light
    {
        get { return target as VLight; }
    }

    public override void OnInspectorGUI()
    {
		Light.MeshRender.hideFlags = HideFlags.None;

		var property = serializedObject.FindProperty("renderWireFrame");
		
		property.boolValue = GUILayout.Toggle(property.boolValue, "Render wireframe");

		EditorUtility.SetSelectedWireframeHidden(Light.MeshRender, !property.boolValue);

		serializedObject.ApplyModifiedProperties();		
		
        base.OnInspectorGUI();

		GUILayout.Space(20);

		var curvesProp = serializedObject.FindProperty("useCurves");
		if(curvesProp.boolValue)
		{
			GUILayout.Label("Falloff gradient"); 
			var tex = serializedObject.FindProperty("_fallOffTexture");
			if(tex.objectReferenceValue != null)
			{
				var rect = GUILayoutUtility.GetRect(100, 100);
				GUI.DrawTexture(rect, tex.objectReferenceValue as Texture2D);
			}
		}

		GUILayout.Label("Generate a baked shadow map");
		if(GUILayout.Button("Bake shadow map", GUILayout.Width(200)))
		{
			Light.RenderBakedShadowMap();
		}
    }
}