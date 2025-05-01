using System;
using UnityEngine;

namespace Koboct.Data
{
    [Serializable]
    public class CaracteristiqueModificateur
    {
        
        [SerializeField] private TypeCaracteristique _monType;
        [SerializeField] private int _modificateur;

        public TypeCaracteristique MonType
        {
            get => _monType;
        }

        public int Modificateur
        {
            get => _modificateur;
        }
    }
}