using System.Collections.Generic;
using UnityEngine;

namespace Koboct
{
    public class CameraLookAtAverage : MonoBehaviour
    {
        [Header("Target Settings")]
        [SerializeField] private List<GameObject> targets; // List of target objects the camera will look at.
        [Header("Interpolation Settings")]
        [SerializeField, Range(0f, 1f)] private float lerpFactor = 0.1f; // Smoothing factor for the LookAt function.

        private void Update()
        {
            if (targets == null || targets.Count == 0)
                return; // Exit if no targets are assigned.

            // Calculate the average position of all active targets
            Vector3 averagePosition = Vector3.zero;
            int validTargetCount = 0;

            foreach (var target in targets)
            {
                if (target != null)
                {
                    averagePosition += target.transform.position;
                    validTargetCount++;
                }
            }

            // Avoid division by zero
            if (validTargetCount == 0) return;

            averagePosition /= validTargetCount; // Get the average position.

            // Get the direction towards the target
            Vector3 directionToTarget = averagePosition - transform.position;

            // Smoothly interpolate the camera's forward direction
            Vector3 newDirection = Vector3.Slerp(transform.forward, directionToTarget.normalized, lerpFactor);

            // Apply the interpolated rotation
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

}