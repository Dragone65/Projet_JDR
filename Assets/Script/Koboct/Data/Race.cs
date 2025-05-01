using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Race", menuName = "Race", order = 0)]
    public class Race : NamedScriptableObject
    {
        [SerializeField] private List<CharacteristiqueModificateur> _modificateurs;

        public void RemoveCharacteristiqueModificateur(Personnage monPersonnage)
        {
            foreach (var modificateur in _modificateurs)
            {
                monPersonnage.SetCharacterisicValue(modificateur.MonType,
                    monPersonnage.GetCharacteristiqueValeur(modificateur.MonType) + modificateur.Modificateur * -1);
            }
        }

        public void ApplyCharacteristiqueModificateur(Personnage monPersonnage)
        {
            foreach (var modificateur in _modificateurs)
            {
                monPersonnage.SetCharacterisicValue(modificateur.MonType,
                    monPersonnage.GetCharacteristiqueValeur(modificateur.MonType) + modificateur.Modificateur);
            }
        }
    }
}