using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(CustomizeButtons))]
public class CustomizeButtonsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CustomizeButtons cb = (CustomizeButtons) target;

        GUI.backgroundColor = Color.blue;
        if(GUILayout.Button("Change Static to Blue")) cb.ChangeColor(0);
        GUI.backgroundColor = Color.yellow;
        if(GUILayout.Button("Change Static to Yellow")) cb.ChangeColor(1);
        GUI.backgroundColor = Color.green;
        if(GUILayout.Button("Change Static to Green")) cb.ChangeColor(2);
        GUI.backgroundColor = Color.red;
        if(GUILayout.Button("Change Static to Purple")) cb.ChangeColor(3);

        GUI.backgroundColor = Color.white;
        if(GUILayout.Button("Apply Hue")) cb.ApplyHue();
        if(GUILayout.Button("Get All Backgrounds")) cb.GetAllBG();
    }
}
#endif