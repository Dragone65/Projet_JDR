using UnityEngine;
using UnityEngine.Events;

namespace Koboct.Game
{
    public class KeyListener : MonoBehaviour
    {
        [Header("Key Settings")] public KeyCode Key;

        [Header("Events")] public UnityEvent OnKeyPress;

        private void Update()
        {
            if (Input.GetKeyDown(Key))
            {
                OnKeyPress?.Invoke();
            }
        }
    }
}

