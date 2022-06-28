using FIMSpace.FEditor;
using UnityEditor;
using UnityEngine;

namespace FIMSpace.FProceduralAnimation
{
    [UnityEditor.CustomEditor(typeof(RagdollAnimator))]
    public partial class RagdollAnimatorEditor : UnityEditor.Editor
    {
        public RagdollAnimator Get { get { if (_get == null) _get = (RagdollAnimator)target; return _get; } }
        private RagdollAnimator _get;

        private SerializedProperty sp_RagProcessor;

        private RagdollGenerator generator;

        private void OnEnable()
        {
            sp_RagProcessor = serializedObject.FindProperty("Processor");

            if (Application.isPlaying)
            {
                Get._EditorDrawSetup = false;
            }
        }

        public override bool UseDefaultMargins()
        {
            return false;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(FGUI_Resources.BGInBoxBlankStyle);

            //if (Application.isPlaying)
            //{
            //    Animator a = Get.transform.GetComponentInChildren<Animator>();
            //    if (a)
            //    {
            //        if (a.enabled == false)
            //        {
            //            GUILayout.Space(4);
            //            EditorGUILayout.HelpBox("Unity Animator disabled - Ragdoll Animator will apply it's algorithms", MessageType.None);
            //            if (GUILayout.Button("Test Enable Animator")) { Get.User_SwitchAnimator(null, true); Get.User_SetAllKinematic(); }
            //            GUILayout.Space(4);
            //        }
            //        else
            //        {
            //            GUILayout.Space(4);
            //            EditorGUILayout.HelpBox("! Unity Animator enabled - Ragdoll Animator overrided - not doing anything", MessageType.None);
            //            if (GUILayout.Button("Test Disable Animator")) { Get.User_SwitchAnimator(); Get.User_SetAllKinematic(false); }
            //            GUILayout.Space(4);
            //        }
            //    }
            //}

            serializedObject.Update();

            Editor_DrawTweakFullGUI(sp_RagProcessor, Get.Parameters, ref Get._EditorDrawSetup);

            GUILayout.Space(2);
            //GUILayout.Space(6);

            if (Get._EditorDrawSetup)
            {
                FGUI_Inspector.DrawUILine(0.4f, 0.3f, 1, 8);

                EditorGUILayout.PropertyField(serializedObject.FindProperty("ObjectWithAnimator"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("CustomRagdollAnimator"));

                EditorGUILayout.PropertyField(serializedObject.FindProperty("RootBone"));
            }

            GUILayout.Space(4);

            Undo.RecordObject(target, "RagdollAnimator");

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndVertical();
        }


        public void Editor_DrawTweakFullGUI(SerializedProperty sp_ragProcessor, RagdollProcessor proc, ref bool drawSetup)
        {
            Color bg = GUI.backgroundColor;

            GUILayout.Space(4);
            EditorGUILayout.BeginHorizontal();

            if (drawSetup) GUI.backgroundColor = new Color(1f, 1f, 0f);
            if (GUILayout.Button(new GUIContent(FGUI_Resources.Tex_GearSetup), FGUI_Resources.ButtonStyle, new GUILayoutOption[] { GUILayout.Width(28), GUILayout.Height(24) })) drawSetup = !drawSetup;
            GUI.backgroundColor = bg;

            EditorGUILayout.BeginVertical(FGUI_Resources.ViewBoxStyle);
            GUILayout.Space(2);
            EditorGUILayout.LabelField(drawSetup ? "Setup Ragdoll Bones" : "Ragdoll Factors", FGUI_Resources.HeaderStyle);
            GUILayout.Space(1);

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            if (drawSetup)
            {
                //GUILayout.Space(8);
                // Generating buttons etc.

                FGUI_Inspector.FoldHeaderStart(ref proc._EditorDrawBones, "Bones Setup", FGUI_Resources.BGInBoxStyle);

                if (proc._EditorDrawBones)
                {
                    GUILayout.Space(4);
                    SerializedProperty sp_BaseTransform = sp_ragProcessor.FindPropertyRelative("BaseTransform");

                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    GUILayout.Space(8);
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    GUILayout.Space(8);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    if (EditorGUI.EndChangeCheck()) { serializedObject.ApplyModifiedProperties(); if (Get.Parameters.LeftUpperArm != null) Get.Parameters.LeftForeArm = Get.Parameters.LeftUpperArm.GetChild(0); }
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    GUILayout.Space(5);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    if (EditorGUI.EndChangeCheck()) { serializedObject.ApplyModifiedProperties(); if (Get.Parameters.RightUpperArm != null) Get.Parameters.RightForeArm = Get.Parameters.RightUpperArm.GetChild(0); }
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    GUILayout.Space(8);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    if (EditorGUI.EndChangeCheck()) { serializedObject.ApplyModifiedProperties(); if (Get.Parameters.LeftUpperLeg != null) Get.Parameters.LeftLowerLeg = Get.Parameters.LeftUpperLeg.GetChild(0); }
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    GUILayout.Space(5);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                    if (EditorGUI.EndChangeCheck()) { serializedObject.ApplyModifiedProperties(); if (Get.Parameters.RightUpperLeg != null) Get.Parameters.RightLowerLeg = Get.Parameters.RightUpperLeg.GetChild(0); }
                    EditorGUILayout.PropertyField(sp_BaseTransform); sp_BaseTransform.Next(false);
                }

                GUILayout.EndVertical();

                GUILayout.Space(5);

                FGUI_Inspector.FoldHeaderStart(ref proc._EditorDrawGenerator, "Ragdoll Generator", FGUI_Resources.BGInBoxStyle);

                if (proc._EditorDrawGenerator)
                {
                    if (generator == null)
                    {
                        generator = new RagdollGenerator();
                        generator.BaseTransform = Get.ObjectWithAnimator != null ? Get.ObjectWithAnimator : Get.transform;
                        generator.SetAllBoneReferences(Get.Parameters.Pelvis, Get.Parameters.SpineStart, Get.Parameters.Chest, Get.Parameters.Head, Get.Parameters.LeftUpperArm, Get.Parameters.LeftForeArm, Get.Parameters.RightUpperArm, Get.Parameters.RightForeArm, Get.Parameters.LeftUpperLeg, Get.Parameters.LeftLowerLeg, Get.Parameters.RightUpperLeg, Get.Parameters.RightLowerLeg);
                    }

                    generator.Tab_RagdollGenerator(Get.Parameters, true);

                }
                else
                {
                    if (generator != null)
                        generator.ragdollTweak = RagdollGenerator.tweakRagd.None;
                }

                GUILayout.EndVertical();

                GUILayout.Space(7);

                EditorGUILayout.BeginVertical(FGUI_Resources.BGInBoxStyle);
                SerializedProperty sp_ext = sp_ragProcessor.FindPropertyRelative("ExtendedAnimatorSync");
                EditorGUILayout.PropertyField(sp_ext);
                sp_ext.NextVisible(false);
                EditorGUILayout.PropertyField(sp_ext);
                GUILayout.Space(3);
                sp_ext.NextVisible(false); EditorGUILayout.PropertyField(sp_ext);
                sp_ext.NextVisible(false); EditorGUILayout.PropertyField(sp_ext);
                GUILayout.Space(3);

                GUILayout.EndVertical();

            }
            else
            {
                if (generator != null)
                    generator.ragdollTweak = RagdollGenerator.tweakRagd.None;

                GUILayout.Space(2);

                EditorGUILayout.BeginVertical(FGUI_Resources.ViewBoxStyle); // ----------
                GUILayout.Space(2);
                SerializedProperty sp_param = sp_ragProcessor.FindPropertyRelative("FreeFallRagdoll");

                RagdollProcessor.Editor_DrawTweakGUI(sp_param, Get.Parameters);

                EditorGUILayout.EndVertical();
            }


            if (Get.Parameters.Pelvis != null)
            {
                bool layerWarn = false;
                if (Get.gameObject.layer == Get.Parameters.Pelvis.gameObject.layer) layerWarn = true;
                if (Get.Parameters.SpineStart) if (Get.gameObject.layer == Get.Parameters.SpineStart.gameObject.layer) layerWarn = true;
                if (Get.Parameters.LeftUpperArm) if (Get.gameObject.layer == Get.Parameters.LeftUpperArm.gameObject.layer) layerWarn = true;

                if (layerWarn)
                {
                    GUILayout.Space(7);
                    EditorGUILayout.HelpBox("WARNING! It seams your main object have the same layer as bone transforms! You should create layer with ignored collision between character model and skeleton bones!", MessageType.Warning);
                    GUILayout.Space(7);
                }
            }
        }

        private void OnSceneGUI()
        {
            if (generator == null) return;
            generator.OnSceneGUI();
        }

    }
}
