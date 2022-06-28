using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(LayoutOrganizer))]
public class LayoutOrganizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LayoutOrganizer lo = (LayoutOrganizer) target;
        if(GUILayout.Button("Clear 3D Array")) lo.Clear3DArray();
        if(GUILayout.Button("Populate 3D Array")) lo.Populate3DArray();
        if(GUILayout.Button("Space Rows")) lo.RowSpacing();
        if(GUILayout.Button("Space Columns")) lo.ColumnSpacing();
        GUIStyle myStyle = GUI.skin.GetStyle("HelpBox");
        myStyle.richText = true;
        EditorGUILayout.TextArea("Always populate before visualizing", myStyle);
    }
}
#endif