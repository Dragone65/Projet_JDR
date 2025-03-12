using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Personnage", menuName = "Personnage", order = 0)]
    public class Personnage : ScriptableObject
    {
        [SerializeField] private string _nom;
        [SerializeField] private string _nomJoueur;
        [TextArea(3, 10)]
        [SerializeField] private string _description;
        [SerializeField] private Genre _sexe;
        [Range(0.5f, 2.5f)]
        [SerializeField] private float _taille;
        [Range(20, 150)]
        [SerializeField] private float _poids;
        [Range(20, 350)]
        [SerializeField] private int _age;
        [SerializeField] private List<Characteristique> _characteristiques = new();
        [SerializeField] private Race _race;
        [SerializeField] private Profil _profil;
        [SerializeField] private TypeDeDe _deDePointDeVie;
        [SerializeField] private int _pointDeVie;
        [SerializeField] private List<Equipement> _equipements = new();
        [SerializeField] private int _bourse;
        [SerializeField] private int _pointDeDefense;
        [SerializeField] private int _modAttaqueContact;
        [SerializeField] private int _modAttaqueDistance;
        [SerializeField] private int _modAttaqueMagique;
        [SerializeField] private Profil _profilMagicien;
        [SerializeField] private Profil _profilPretre;
        
        private void OnEnable()
        {
            Reset();
        }

        public void Reset()
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
            _equipements.Clear();
            _bourse = 0;
            _pointDeDefense = 0;
            _nom=string.Empty;
            _nomJoueur=string.Empty;
            _description=string.Empty;
            _sexe=Genre.Neutre;
            _taille=0;
            _poids=0;
            _age=0;
            _modAttaqueDistance=0;
            _modAttaqueContact=0;
            _modAttaqueMagique=0;
        }

        public string NomJoueur
        {
            set => _nomJoueur = value;
        }
        private int GetCharacteristiqueValeur(TypeCharacteristique type)
        {
            return GetCharacteristique(type).Valeur;
        }

        private int GetCharacteristiqueModificateur(TypeCharacteristique type)
        {
            return GetCharacteristique(type).Modificateur;
        }

        private Characteristique GetCharacteristique(TypeCharacteristique type)
        {
            return _characteristiques.First(car => car.MonType == type);
        }

        [ContextMenu("Calculer les points de vies")]
        private void CalculPointDeVie()
        {
            _deDePointDeVie = _profil.DeDePointDeVie;
            _pointDeVie = (int)_deDePointDeVie + GetCharacteristiqueModificateur(TypeCharacteristique.Constitution);
        }

        [ContextMenu("Calculer les points de défense")]
        private void CalculPointDeDefense()
        {
            _pointDeDefense = 10 + GetCharacteristiqueModificateur(TypeCharacteristique.Dexterite) +
                              _equipements.OfType<Protection>().Sum(protection => protection.ModificateurDArmure);
        }

        [ContextMenu("Calculer les mod. d'attaque")]
        private void CalculModDAttaque()
        {
            _modAttaqueContact = GetCharacteristiqueModificateur(TypeCharacteristique.Force) + 1;
            _modAttaqueDistance = GetCharacteristiqueModificateur(TypeCharacteristique.Dexterite) + 1;

            
            if (_profil == _profilMagicien)
                _modAttaqueMagique = GetCharacteristiqueModificateur(TypeCharacteristique.Intelligence) + 1;
            else if (_profil == _profilPretre)
                _modAttaqueMagique = GetCharacteristiqueModificateur(TypeCharacteristique.Sagesse) + 1;
            else
                _modAttaqueMagique = 0;
        }

        public void SetCharacterisicValue(TypeCharacteristique typeCharacteristique, int i)
        {
            var characteristic = _characteristiques.FirstOrDefault(car => car.MonType == typeCharacteristique);
            if (characteristic != null)
            {
                characteristic.Valeur = i;
            }
        }
    }

    internal enum Genre
    {
        Masculin,
        Feminin,
        Neutre
    }
}