using UnityEngine;
using UnityEngine.Events;

namespace Koboct.Game
{
    public class TriggerOnTag : MonoBehaviour
    {
        public string targetTag;
        public UnityEvent OnTriggerEnterEvent=new ();
        public UnityEvent OnTriggerExitEvent=new ();

        private void Start()
        {
            OnTriggerExitEvent.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag))
                OnTriggerEnterEvent.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(tag))
                OnTriggerExitEvent.Invoke();
        }
    }
}