using UnityEngine;

namespace Koboct
{
    [RequireComponent(typeof(Rigidbody))]
    public class RandomRigid : MonoBehaviour
    {
        [Header("Torque Settings")]
        [SerializeField] private float minTorque = -10f; // Minimum torque value
        [SerializeField] private float maxTorque = 10f;  // Maximum torque value

        [Header("Force Settings")]
        [SerializeField] private float minForce = -2f;  // Minimum displacement force value
        [SerializeField] private float maxForce = 2f;   // Maximum displacement force value

        private Rigidbody rb;

        // Called when the object becomes enabled and active
        private void OnEnable()
        {
            // Get the Rigidbody component attached to this GameObject
            rb = GetComponent<Rigidbody>();

            // Generate a random torque (rotation force)
            Vector3 randomTorque = new Vector3(
                Random.Range(minTorque, maxTorque),
                Random.Range(minTorque, maxTorque),
                Random.Range(minTorque, maxTorque)
            );

            // Generate a random displacement force
            Vector3 randomForce = new Vector3(
                Random.Range(minForce, maxForce),
                -40,
                Random.Range(minForce, maxForce)
            );

            // Apply the random torque to rotate the Rigidbody
            rb.AddTorque(randomTorque, ForceMode.Impulse);

            // Apply the random force to displace (move) the Rigidbody
            rb.AddForce(randomForce, ForceMode.Impulse);
        }
    }

}