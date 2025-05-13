using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Autohand;

namespace Autohand.Demo
{
    public class WheelRotator : PhysicsGadgetHingeAngleReader
    {
        public Transform move;
        public Vector3 angle;
        public bool useLocal = false;

        [Header("Trigger Settings")]
        public float targetAngle = 90f;
        public float tolerance = 5f;
        public bool triggerOnce = true;
        public UnityEvent onTargetReached;

        private bool triggered = false;

        void Update()
        {
            // Existing rotation logic
            if (useLocal)
                move.localRotation *= Quaternion.Euler(angle * Time.deltaTime * GetValue());
            else
                move.rotation *= Quaternion.Euler(angle * Time.deltaTime * GetValue());

            // Trigger logic
            float current = GetValue(); // value from -1 to 1
            float mappedAngle = Mathf.Lerp(-90f, 90f, (current + 1f) / 2f); // or use actual GetAngle() if available

            if (!triggered || !triggerOnce)
            {
                if (Mathf.Abs(mappedAngle - targetAngle) <= tolerance)
                {
                    onTargetReached.Invoke();
                    if (triggerOnce)
                        triggered = true;
                }
            }
        }
    }
}
