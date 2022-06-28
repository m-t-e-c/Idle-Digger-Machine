using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(SettingsControl))]
public class SettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SettingsControl fs = (SettingsControl) target;
        if(GUILayout.Button("Clear Player Preferences")) PlayerPrefs.DeleteAll();
    }
}
#endif