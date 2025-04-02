using UnityEngine;

namespace Koboct.Data
{
    public abstract class NamedScriptableObject : ScriptableObject
    {
        [SerializeField] private string _nom;
#if UNITY_EDITOR
        private void OnEnable()
        {
            _nom= name ;
        }
#endif
    }
}