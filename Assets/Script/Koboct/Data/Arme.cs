using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Arme", menuName = "Arme", order = 0)]
    public class Arme : Equipement
    {
        [SerializeField] private bool _contact;
        [SerializeField] private bool _distance;
        [SerializeField] private bool _uneMain;
        [SerializeField] private bool _deuxMains;

        
        [SerializeField] private TypeDeDe _typeDeDeDegats;
        [Range(1,5)]
        [SerializeField] private int _nbDeDeDegats;
        [SerializeField] private TypeCharacteristique _modificateurDeDegats;
    }
}