using System;
using UnityEngine;

namespace FIMSpace.FProceduralAnimation
{
    [AddComponentMenu("FImpossible Creations/Ragdoll Animator")]
    public class RagdollAnimator : MonoBehaviour
    {
        [HideInInspector] public bool _EditorDrawSetup = true;

        [SerializeField]
        private RagdollProcessor Processor;

        [Tooltip("! REQUIRED ! Just object with Animator and skeleton as child transforms")]
        public Transform ObjectWithAnimator;

        [Tooltip("! OPTIONAL ! Leave here nothing to not use the feature! \n\nObject with bones structure to which ragdoll should try fit with it's pose.\nUseful only if you want to animate ragdoll with other animations than the model body animator.")]
        public Transform CustomRagdollAnimator;
        [Tooltip("If null then it will be found automatically - do manual if you encounter some errors after entering playmode")] public Transform RootBone;

        public RagdollProcessor Parameters { get { return Processor; } }

        private void Reset()
        {
            if (Processor == null) Processor = new RagdollProcessor();
            Processor.TryAutoFindReferences(transform);
            Animator an = GetComponentInChildren<Animator>();
            if (an) ObjectWithAnimator = an.transform;
        }

        private void Start()
        {
            Processor.Initialize(this, ObjectWithAnimator, CustomRagdollAnimator, RootBone);
        }

        private void FixedUpdate()
        {
            Processor.FixedUpdate();
        }

        private void LateUpdate()
        {
            Processor.LateUpdate();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Processor.DrawGizmos();
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                Parameters.SwitchAllExtendedAnimatorSync(Parameters.ExtendedAnimatorSync);
            }
        }
#endif


        // --------------------------------------------------------------------- UTILITIES


        /// <param name="limb"> Access 'Parameters' for ragdoll limb </param>
        public void User_SetLimbImpact(Rigidbody limb, Vector3 powerDirection, float duration)
        {
            StartCoroutine(Processor.User_SetLimbImpact(limb, powerDirection, duration));
        }

        public void User_EnableFreeRagdoll(float blend = 1f)
        {
            Parameters.FreeFallRagdoll = true;
            User_FadeRagdolledBlend(blend, 0.2f);
        }

        public void User_SetPhysicalImpactAll(Vector3 powerDirection, float duration)
        {
            StartCoroutine(Processor.User_SetPhysicalImpactAll(powerDirection, duration));
        }

        public void User_SwitchAnimator(Transform unityAnimator = null, bool enabled = false, float delay = 0f, bool captureAnimator = false)
        {
            if (unityAnimator == null) unityAnimator = ObjectWithAnimator;
            if (unityAnimator == null) return;

            Animator an = unityAnimator.GetComponent<Animator>();
            if (an)
            {
                StartCoroutine(Processor.User_SwitchAnimator(an, enabled, captureAnimator, delay));
            }
        }

        public void User_FadeMuscles(float forcePoseEnd = 0f, float duration = 0.75f, float delay = 0f)
        {
            StartCoroutine(Parameters.User_FadeMuscles(forcePoseEnd, duration, delay));
        }

        /// <summary> Forcing rotating animator bones as ragdolled bones, can solve issue with non smooth transitions </summary>
        internal void User_ForceRagdollToAnimatorFor(float duration = 1f, float forcingFullDelay = 0.2f)
        {
            StartCoroutine(Parameters.User_ForceRagdollToAnimatorFor(duration, forcingFullDelay));
        }

        public void User_FadeRagdolledBlend(float targetBlend = 0f, float duration = 0.75f, float delay = 0f)
        {
            StartCoroutine(Parameters.User_FadeRagdolledBlend(targetBlend, duration, delay));
        }

        public void User_SetAllKinematic(bool kinematic = true)
        {
            Parameters.User_SetAllKinematic(kinematic);
        }

        public void User_AnchorPelvis(bool anchor = true, float duration = 0f)
        {
            StartCoroutine(Parameters.User_AnchorPelvis(anchor, duration));
        }

        public void User_RepositionRoot(Transform root = null, Vector3? worldUp = null, RagdollProcessor.EGetUpType getupType = RagdollProcessor.EGetUpType.None, LayerMask? snapToGround = null)
        {
            Parameters.User_RepositionRoot(root, null, worldUp, getupType, snapToGround);
        }
    }
}