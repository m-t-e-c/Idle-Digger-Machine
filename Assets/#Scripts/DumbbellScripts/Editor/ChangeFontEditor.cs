using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(FontSelector))]
public class ChangeFontEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        FontSelector fs = (FontSelector) target;
        if(GUILayout.Button("Change Font")) fs.ChangeFont();
        if(GUILayout.Button("Find Text Objects")) fs.GetTexts();
        if(GUILayout.Button("Get Current Font")) fs.CurrentFont();
    }
}
#endif
