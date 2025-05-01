using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Race", menuName = "Race", order = 0)]
    public class Race : NamedScriptableObject
    {
        [SerializeField] private List<CaracteristiqueModificateur> _modificateurs;

        public void RemoveCaracteristiqueModificateur(Personnage monPersonnage)
        {
            foreach (var modificateur in _modificateurs)
            {
                monPersonnage.SetCaracterisicValue(modificateur.MonType,
                    monPersonnage.GetCaracteristiqueValeur(modificateur.MonType) + modificateur.Modificateur *-1);
            }
        }

        public void ApplyCaracteristiqueModificateur(Personnage monPersonnage)
        {
            foreach (var modificateur in _modificateurs)
            {
                monPersonnage.SetCaracterisicValue(modificateur.MonType,
                    monPersonnage.GetCaracteristiqueValeur(modificateur.MonType) + modificateur.Modificateur);

            }
        }
    }
}