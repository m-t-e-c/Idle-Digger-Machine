﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.FProceduralAnimation
{

    public partial class RagdollProcessor
    {
        public enum ESyncMode { None, RagdollToAnimator, AnimatorToRagdoll }

        private class PosingBone
        {
            /// <summary> Bone transform of ragdoll - hidden influence </summary>
            public Transform transform;
            /// <summary> Bone transform of animator - visible influence </summary>
            public Transform visibleBone;
            /// <summary> Bone transform of custom pose ragdoll - hidden influence </summary>
            public Transform customRefBone;
            public Transform riggedParent;

            public ToAnimateBone parentFixer;

            /// <summary> Tries to correct arms rotation with shoulder rotation etc. </summary>
            public ESyncMode FullAnimatorSync = ESyncMode.None;

            public Quaternion animatorLocalRotation;

            public Collider collider;
            public Rigidbody rigidbody;

            public RagdollCollisionHelper collisions;
            public RagdollProcessor owner;
            public PosingBone child;

            public float user_internalMusclePower = 1f;
            public float user_internalRagdollBlend = 1f;
            public float user_internalMuscleMultiplier = 1f;
            public float internalMusclePower = 1f;
            public float internalRagdollBlend = 0f;
            public float targetMass = 3f;

            public ConfigurableJoint ConfigurableJoint { get; private set; }
            public CharacterJoint CharacterJoint { get; private set; }
            public bool Colliding { get { return collisions.EnteredCollisions.Count > 0; } }

            internal Quaternion initialParentLocalRotation = Quaternion.identity;
            internal Quaternion initialLocalRotation;
            Quaternion localConvert;
            Quaternion jointAxisConversion;
            Quaternion initialAxisCorrection;

            public PosingBone(Transform tr, RagdollProcessor owner)
            {

                if (tr == null)
                {
                    UnityEngine.Debug.Log("<b>[Ragdoll Generator]</b> Not found some of the bones! You probably need to assign '<b>Root Bone</b>' (on the bottom of the inspector window of RagdollAnimator) to fix this issue!");
                    UnityEngine.Debug.LogError("<b>[Ragdoll Generator]</b> Not found some of the bones! You probably need to assign '<b>Root Bone</b>' (on the bottom of the inspector window of RagdollAnimator) to fix this issue!");
                }

                // Main ----------------------------------
                transform = tr;
                initialLocalRotation = tr.localRotation;
                visibleBone = null;
                this.owner = owner;
                animatorLocalRotation = tr.localRotation;

                // Physical Components ----------------------------------
                collider = transform.GetComponent<Collider>();
                rigidbody = transform.GetComponent<Rigidbody>();
                rigidbody.maxAngularVelocity = 15f;
                if (rigidbody == null) Debug.Log("[Ragdoll Animator] Bone " + transform.name + " is not having Rigidbody attached to it!");
                targetMass = rigidbody.mass;

                // Joints ----------------------------------
                ConfigurableJoint = rigidbody.gameObject.GetComponent<ConfigurableJoint>();
                if (ConfigurableJoint == null)
                {
                    CharacterJoint = rigidbody.gameObject.GetComponent<CharacterJoint>();
                    if (CharacterJoint) riggedParent = CharacterJoint.connectedBody.transform;
                }
                else
                {
                    riggedParent = ConfigurableJoint.connectedBody == null ? tr.parent : ConfigurableJoint.connectedBody.transform;
                }

                if (riggedParent == null) riggedParent = transform.parent;

                // Joint space preparations ----------------------------------
                if (ConfigurableJoint)
                {
                    localConvert = Quaternion.identity;

                    Vector3 forward = Vector3.Cross(ConfigurableJoint.axis, ConfigurableJoint.secondaryAxis).normalized;
                    Vector3 up = Vector3.Cross(forward, ConfigurableJoint.axis).normalized;

                    Quaternion toJointSpace = Quaternion.LookRotation(forward, up);
                    jointAxisConversion = Quaternion.Inverse(toJointSpace);
                    initialAxisCorrection = initialLocalRotation * toJointSpace;
                }
            }

            internal void SetVisibleBone(Transform visBone)
            {
                if (visBone) if (visBone.parent) initialParentLocalRotation = visBone.parent.localRotation;
                visibleBone = visBone;
            }

            internal void CaptureAnimator()
            {
                if (customRefBone)
                    animatorLocalRotation = customRefBone.localRotation;
                else if (visibleBone != null)
                    animatorLocalRotation = visibleBone.localRotation;
            }

            public float internalForceMultiplier = 1f;
            public void FixedUpdate()
            {

                float blend = owner.RotateToPoseForce * internalForceMultiplier * internalMusclePower * user_internalMusclePower;

                // Using configurable joint for ragdoll ---------------------------------------
                if (ConfigurableJoint)
                {
                    rigidbody.solverIterations = owner.UnitySolverIterations;

                    var dr = ConfigurableJoint.slerpDrive;
                    dr.positionSpring = owner.ConfigurableSpring * blend;
                    dr.positionDamper = owner.ConfigurableDamp * blend;
                    dr.maximumForce = owner.ConfigurableSpring;
                    ConfigurableJoint.slerpDrive = dr;

                    //float angleDiff = Quaternion.Angle(ToConfigurableSpaceRotation(rigidbody.transform.localRotation), ConfigurableJoint.targetRotation);

                    ConfigurableJoint.targetRotation = ToConfigurableSpaceRotation(animatorLocalRotation);
                    //float angleDiff = Quaternion.Angle(ToConfigurableSpaceRotation(rigidbody.transform.localRotation), ConfigurableJoint.targetRotation);
                    //blend *= Mathf.Lerp(0.5f, 2f, angleDiff * 0.1f);

                    return;
                }

                if (blend <= 0f) return;

                // Using character joints ------------------------------------------------------
                Vector3 targetAngular = FEngineering.QToAngularVelocity(rigidbody.rotation, transform.parent.rotation * animatorLocalRotation, true);

                if (user_internalMuscleMultiplier != 0f)
                    rigidbody.angularVelocity = Vector3.LerpUnclamped(rigidbody.angularVelocity, targetAngular * user_internalMuscleMultiplier, blend);
                else
                    rigidbody.angularVelocity = Vector3.LerpUnclamped(rigidbody.angularVelocity, targetAngular, blend);

            }

            Quaternion ToConfigurableSpaceRotation(Quaternion local)
            {
                return jointAxisConversion * Quaternion.Inverse(local * localConvert) * initialAxisCorrection;
            }

            internal void SyncAnimatorToRagdoll(float blend)
            {
                if (visibleBone == null) return;
                visibleBone.localRotation = Quaternion.LerpUnclamped(visibleBone.localRotation, transform.localRotation, blend);
            }
        }

        public Transform GetRagdollDummyBoneByAnimatorBone(Transform tr)
        {
            PosingBone p = posingPelvis;
            while (p != null)
            {
                if (p.visibleBone == tr) return p.transform;
                p = p.child;
            }

            return null;
        }

        private List<ToAnimateBone> toReanimateBones = new List<ToAnimateBone>();
        private class ToAnimateBone
        {
            public Transform animatorVisibleBone;
            public Transform ragdollBone;
            public PosingBone childRagdollBone;

            /// <summary> When zero then not used </summary>
            public float InternalRagdollToAnimatorOverride = 0f;

            public ToAnimateBone(Transform animatorVisibleBone, Transform ragdollBone, PosingBone child)
            {
                this.animatorVisibleBone = animatorVisibleBone;
                this.ragdollBone = ragdollBone;
                childRagdollBone = child;
                childRagdollBone.parentFixer = this;
            }

            internal bool wasSyncing = false;
            internal void SyncRagdollBone(float ragdolledBlend, bool animatorEnabled)
            {
                wasSyncing = false;

                if (childRagdollBone.FullAnimatorSync == ESyncMode.AnimatorToRagdoll)
                {
                    if (animatorEnabled)
                    {
                        if (ragdolledBlend <= 0f)
                        {
                            ragdollBone.localRotation = animatorVisibleBone.localRotation;
                        }
                        else
                        {
                            ragdollBone.localRotation =
                                Quaternion.LerpUnclamped(
                                animatorVisibleBone.localRotation,
                                ragdollBone.localRotation,
                                ragdolledBlend);
                        }


                        Quaternion rotDiff = animatorVisibleBone.localRotation * Quaternion.Inverse(childRagdollBone.initialParentLocalRotation);
                        childRagdollBone.animatorLocalRotation = childRagdollBone.animatorLocalRotation * (rotDiff);
                        wasSyncing = true;
                    }
                }
                else if (childRagdollBone.FullAnimatorSync == ESyncMode.RagdollToAnimator)
                {
                    if (InternalRagdollToAnimatorOverride > 0f)
                        SyncAnimatorBone(Mathf.Max(InternalRagdollToAnimatorOverride, ragdolledBlend));
                    else
                        SyncAnimatorBone(ragdolledBlend);
                }

                // Useful for getup animations
                if (childRagdollBone.FullAnimatorSync != ESyncMode.RagdollToAnimator)
                    if (InternalRagdollToAnimatorOverride > 0f)
                    {
                        SyncAnimatorBone(InternalRagdollToAnimatorOverride);
                    }

            }

            internal void SyncAnimatorBone(float ragdollBlend)
            {
                wasSyncing = true;

                if (ragdollBlend >= 1f)
                {
                    animatorVisibleBone.localRotation = ragdollBone.localRotation;
                }
                else
                {
                    animatorVisibleBone.localRotation = Quaternion.LerpUnclamped(
                        animatorVisibleBone.localRotation, ragdollBone.localRotation,
                        ragdollBlend);
                }
            }
        }

    }
}