using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Personnage", menuName = "Personnage", order = 0)]
    public class Personnage : ScriptableObject
    {
        [SerializeField] private List<Characteristique> _characteristiques = new();
        [SerializeField] private Race _race;
        [SerializeField] private Profil _profil;
        [SerializeField] private TypeDeDe _deDePointDeVie;
        [SerializeField] private int _pointDeVie;

        private void OnEnable()
        {
            Reset();
        }

        private void Reset()
        {
            _characteristiques.Clear();
            _characteristiques.Add(new Characteristique { MonType = TypeCharacteristique.Force });
            _characteristiques.Add(new Characteristique { MonType = TypeCharacteristique.Dexterite });
            _characteristiques.Add(new Characteristique { MonType = TypeCharacteristique.Constitution });
            _characteristiques.Add(new Characteristique { MonType = TypeCharacteristique.Intelligence });
            _characteristiques.Add(new Characteristique { MonType = TypeCharacteristique.Sagesse });
            _characteristiques.Add(new Characteristique { MonType = TypeCharacteristique.Charisme });
            _race = null;
            _profil = null;
            _deDePointDeVie = 0;
            _pointDeVie = 0;
        }

        private int GetCharacteristiqueValeur(TypeCharacteristique type)
        {
            var chara = _characteristiques.First(car => car.MonType == type);
            return chara.Valeur;
        }

        private int GetCharacteristiqueModificateur(TypeCharacteristique type)
        {
            var chara = _characteristiques.First(car => car.MonType == type);
            return chara.Modificateur;
        }
        
        [ContextMenu("Calculer les points de vies")]
        private void CalculPointDeVie()
        {
            _deDePointDeVie = _profil.DeDePointDeVie;
            _pointDeVie = (int)_deDePointDeVie + GetCharacteristiqueModificateur(TypeCharacteristique.Constitution);
        }
    }
}