using System;
using UnityEngine;

namespace Koboct.Data
{
    [Serializable]
    public class CharacteristiqueModificateur
    {
        [SerializeField] private TypeCharacteristique _monType;
        [SerializeField] private int _modificateur;    
    }
}