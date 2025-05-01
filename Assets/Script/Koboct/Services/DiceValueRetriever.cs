using System;
using UnityEngine;
using System.Collections.Generic;

namespace Koboct.Services
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiceValueCalculator : MonoBehaviour
    {
        private Rigidbody rb;

        // Attach these in the inspector or programmatically
        [SerializeField] private List<Transform> faceTransforms; // A list of child transforms for each face

        // Determines if the dice has already been checked
        private bool hasStopped = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody is required for DiceValueCalculator to work!");
            }

            if (faceTransforms == null || faceTransforms.Count == 0)
            {
                Debug.LogError("Face transforms must be assigned in the inspector or generated at runtime!");
            }
        }

        private void Update()
        {
            // Check if the dice has stopped moving
            if (rb != null && rb.IsSleeping() && !hasStopped)
            {
                hasStopped = true; // Prevent further calculations once the dice has stopped
                int diceValue = CalculateDiceValue();
                Debug.Log("Dice Value: " + diceValue); // Output the dice roll result
            }
        }

        // Calculate and return the top face value of the dice
        private int CalculateDiceValue()
        {
            if (faceTransforms == null )
            {
                Debug.LogError("Face transforms are not properly configured!");
                return -1; // Return an invalid value if setup is incomplete
            }

            // Find the transform with the lowest Y position
            int lowestYIndex = -1;
            float lowestY = float.MaxValue;

            for (int i = 0; i < faceTransforms.Count; i++)
            {
                float yPosition = faceTransforms[i].position.y; // Get the world Y position of the face
                if (yPosition < lowestY)
                {
                    lowestY = yPosition;
                    lowestYIndex = i;
                }
            }

            // Return the value of the face with the lowest Y position
            if (lowestYIndex >= 0)
            {
                return Convert.ToInt32( faceTransforms[lowestYIndex].name);
            }

            Debug.LogWarning("Failed to determine the dice value!");
            return -1;
        }
    }
}