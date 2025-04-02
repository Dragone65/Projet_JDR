using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Voie", menuName = "Voie", order = 0)]
    public class Voie:NamedScriptableObject
    {
        [SerializeField] private List<Capacite> _capacites;
    }
}