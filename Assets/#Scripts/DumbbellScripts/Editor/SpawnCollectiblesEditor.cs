using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(SpawnCollectibles))]
public class SpawnCollectiblesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpawnCollectibles sc = (SpawnCollectibles) target;
        if(GUILayout.Button("Generate Pickup Areas(Ordered)")) sc.SpawnLinear();
        if(GUILayout.Button("Generate Pickup Areas(Randomized)")) sc.SpawnWeighted();
        if(GUILayout.Button("Generate Pickup Objects")) sc.InstantiatePickups();
        GUIStyle myStyle = GUI.skin.GetStyle("HelpBox");
        myStyle.richText = true;
        string x = "<b>You need 11 pickup areas in order to use Spawn Weighted Randoms</b>\nOnly use randomize if you didn't fill pickup areas in an order\n\nStart-End Positions are floats in Z-axis \nThis script always generates pickups in a linear line\nSpawn interval is distance betweeen two pickup areas \nFeed in all the pickup areas you want to visualize and press Generate Pickup Areas";
        EditorGUILayout.TextArea(x, myStyle);
    }
}
#endif
