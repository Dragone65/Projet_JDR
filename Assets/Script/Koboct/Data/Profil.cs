using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Profil", menuName = "Profil", order = 0)]
    public class Profil : NamedScriptableObject
    {
        [SerializeField] private List<Voie> _voies = new List<Voie>();

        [SerializeField] private TypeDeDe _deDePointDeVie;
        [SerializeField] private List<Equipement> _equimentsDeBase = new();
        [SerializeField] public int _argentDeDepart = 5;

        public TypeDeDe DeDePointDeVie
        {
            get => _deDePointDeVie;
        }

        public List<Voie> Voies
        {
            get => _voies;
        }

        public List<Equipement> EquimentsDeBase
        {
            get => _equimentsDeBase;
        }
    }
}