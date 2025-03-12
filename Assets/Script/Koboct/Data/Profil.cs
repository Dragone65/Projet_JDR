using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Profil", menuName = "Profil", order = 0)]
    public class Profil:NamedScriptableObject
    {
        [SerializeField] private List<Voie> _voies = new List<Voie>();
        
        [SerializeField] private TypeDeDe _deDePointDeVie;

        public TypeDeDe DeDePointDeVie
        {
            get => _deDePointDeVie;
        }
    }
}