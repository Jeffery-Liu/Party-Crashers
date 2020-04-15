using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/*
 * VLight
 * Copyright Brian Su 2011-2015
*/
[CustomEditor(typeof(VLightManager))]
public class VolumeLightManagerEditor : Editor
{
    public VLightManager Manager
    {
        get { return target as VLightManager; }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}