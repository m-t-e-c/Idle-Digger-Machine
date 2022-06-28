#if UNITY_EDITOR
using FIMSpace.FEditor;
using System;
using UnityEditor;
using UnityEngine;

namespace FIMSpace.FProceduralAnimation
{
    public partial class RagdollProcessor
    {
        [HideInInspector] public bool _EditorDrawBones = true;
        [HideInInspector] public bool _EditorDrawGenerator = false;
        [HideInInspector] public bool _EditorDrawMore = false;


        public static void Editor_DrawTweakGUI(SerializedProperty sp_param, RagdollProcessor proc)
        {
            EditorGUILayout.PropertyField(sp_param);
            bool freeFall = sp_param.boolValue;
            sp_param.Next(false);

            float amount = sp_param.floatValue;

            Color preC = GUI.color;
            if (freeFall && amount < 0.5f) GUI.color = Color.yellow;
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            if (freeFall && amount < 0.5f) GUI.color = preC;

            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param); sp_param.Next(false);
            EditorGUILayout.PropertyField(sp_param);

            FGUI_Inspector.FoldHeaderStart(ref proc._EditorDrawMore, "More Individual Limbs Settings", FGUI_Resources.BGInBoxStyle);
            if (proc._EditorDrawMore)
            {
                sp_param.Next(false); EditorGUILayout.PropertyField(sp_param);
                sp_param.Next(false); EditorGUILayout.PropertyField(sp_param);
                sp_param.Next(false); EditorGUILayout.PropertyField(sp_param);
                sp_param.Next(false); EditorGUILayout.PropertyField(sp_param);
            }

            GUILayout.EndVertical();

            //Editor_MainSettings(sp_ragProcessor.FindPropertyRelative("SideSwayPower"));
            //GUILayout.Space(2);
            //Editor_RootSwaySettings(sp_ragProcessor.FindPropertyRelative("FootsOrigin"));
            //GUILayout.Space(2);
            //Editor_SpineLeanSettings(sp_ragProcessor.FindPropertyRelative("SpineBlend"));
        }


        internal void DrawGizmos()
        {
            if (Pelvis == null) return;
            Gizmos.DrawLine(Pelvis.position, Pelvis.TransformPoint(PelvisToBase));

            if (Application.isPlaying)
            {
                Handles.color = new Color(0.4f, 1f, 0.4f, 0.8f);

                foreach (var item in RagdollLimbs)
                {
                    if (item == null) continue;
                    if (item.transform.parent == null) continue;
                    if (item.transform == posingPelvis.transform) continue;

                    if ( item.transform == posingLeftLowerLeg.transform || item.transform == posingRightLowerLeg.transform  )
                    {
                        if (item.transform.childCount > 0) FGUI_Handles.DrawBoneHandle(item.transform.position, item.transform.GetChild(0).position, 0.6f);
                    } 
                    else if (item.transform == posingLeftForeArm.transform  )
                    {
                            FGUI_Handles.DrawBoneHandle(item.transform.position, item.transform.TransformPoint(LForearmToHand), 0.6f);
                    }
                    else if (item.transform == posingRightForeArm.transform)
                    {
                        FGUI_Handles.DrawBoneHandle(item.transform.position, item.transform.TransformPoint(RForearmToHand), 0.6f);
                    }
                    else if (item.transform == posingHead.transform)
                    {
                        FGUI_Handles.DrawBoneHandle(item.transform.position, item.transform.TransformPoint(HeadToTip), 0.6f);
                    }

                    Joint j = item.GetComponent<Joint>();
                    if (j == null) continue;
                    if (j.connectedBody == null) continue;

                    FGUI_Handles.DrawBoneHandle(j.connectedBody.transform.position, item.transform.position, 0.6f);
                }

                Handles.color = Color.white;
            }
        }

    }
}

#endif