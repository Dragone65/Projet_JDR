using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Race", menuName = "Race", order = 0)]
    public class Race : NamedScriptableObject
    {
        [SerializeField] private List<CharacteristiqueModificateur> _modificateurs;
        
    }
}