using System;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Arme", menuName = "Arme", order = 0)]
    [Serializable]
    public class Arme : Equipement
    {
        [SerializeField] private bool _contact;
        [SerializeField] private bool _distance;
        [SerializeField] private bool _uneMain;
        [SerializeField] private bool _deuxMains;

        
        [SerializeField] public TypeDeDe _typeDeDeDegats;
        [Range(1,5)]
        [SerializeField] public int _nbDeDeDegats;
        [SerializeField] public TypeCaracteristique _modificateurDeDegats;
    }
}