using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Capacite", menuName = "Capacite", order = 0)]
    public class Capacite:NamedScriptableObject
    {
        [SerializeField] private bool _choisi;
    }
}