using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.FProceduralAnimation
{
    public partial class RagdollProcessor
    {
        public class RagdollCollisionHelper : MonoBehaviour
        {
            public bool Colliding = false;
            public bool DebugLogs = false;
            private RagdollProcessor parent = null;

            public RagdollCollisionHelper Initialize(RagdollProcessor owner)
            {
                parent = owner;
                return this;
            }

            [NonSerialized] public List<Transform> EnteredCollisions = new List<Transform>();
            [NonSerialized] public List<Transform> ignores = new List<Transform>();
            private void OnCollisionEnter(Collision collision)
            {
                if (ignores.Contains(collision.transform)) return;
                if (DebugLogs) UnityEngine.Debug.Log(name + " collides with " + collision.transform.name);
                EnteredCollisions.Add(collision.transform);
                Colliding = true;
            }

            private void OnCollisionExit(Collision collision)
            {
                EnteredCollisions.Remove(collision.transform);
                if (EnteredCollisions.Count == 0) Colliding = false;
            }
        }
    }
}