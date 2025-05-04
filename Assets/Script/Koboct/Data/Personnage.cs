using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Personnage", menuName = "Personnage", order = 0)]
    public class Personnage : ScriptableObject
    {
        [SerializeField] public string _nom;
        [SerializeField] public string _nomJoueur;
        [TextArea(3, 10)]
        [SerializeField] public string _description;
        [SerializeField] public Genre _sexe;
        [SerializeField] public int _taille;
        [SerializeField] public int _poids;
        [SerializeField] public int _age;
        [SerializeField] public List<Caracteristique> _caracteristiques = new();
        [SerializeField] public Race _race;
        [SerializeField] public Profil _profil;
        [SerializeField] public TypeDeDe _deDePointDeVie;
        [SerializeField] public int _pointDeVie;
        [SerializeField] public List<Equipement> _equipements = new();
        [SerializeField] public Voie Voie1;  
        [SerializeField] public Voie Voie2; 
        [SerializeField] public Voie Voie3;
        [SerializeField] public int _bourse;
        [SerializeField] public int _pointDeDefense;
        [SerializeField] public int _modAttaqueContact;
        [SerializeField] public int _modAttaqueDistance;
        [SerializeField] public int _modAttaqueMagique;
        [SerializeField] private Profil _Magicien;
        [SerializeField] private Profil _Pretre;

        private void OnEnable()
        {
            Reset();
        }

        public void Reset()
        {
            _caracteristiques.Clear();
            _caracteristiques.Add(new Caracteristique { MonType = TypeCaracteristique.Force });
            _caracteristiques.Add(new Caracteristique { MonType = TypeCaracteristique.Dexterite });
            _caracteristiques.Add(new Caracteristique { MonType = TypeCaracteristique.Constitution });
            _caracteristiques.Add(new Caracteristique { MonType = TypeCaracteristique.Intelligence });
            _caracteristiques.Add(new Caracteristique { MonType = TypeCaracteristique.Sagesse });
            _caracteristiques.Add(new Caracteristique { MonType = TypeCaracteristique.Charisme });
            _race = null;
            _profil = null;
            _deDePointDeVie = 0;
            _pointDeVie = 0;
            _equipements.Clear();
            Voie1 = null;
            Voie2 = null;
            Voie3 = null;
            _bourse = 0;
            _pointDeDefense = 0;
            _nom = string.Empty;
            _nomJoueur = string.Empty;
            _description = string.Empty;
            _sexe = Genre.Neutre;
            _taille = 0;
            _poids = 0;
            _age = 0;
            _modAttaqueDistance = 0;
            _modAttaqueContact = 0;
            _modAttaqueMagique = 0;
        }

        public string NomJoueur
        {
            set => _nomJoueur = value;
        }

        public Race Race

        {
            get => _race;
            set => _race = value;
        }


        public Profil Profil
        {
            get => _profil;
            set => _profil = value;
        }

        public List<Equipement> Equipements
        {
            get => _equipements;
            set => _equipements = value;
        }

        public int GetCaracteristiqueValeur(TypeCaracteristique type)
        {
            return GetCaracteristique(type).Valeur;
        }

        public int GetCaracteristiqueModificateur(TypeCaracteristique type)
        {
            return GetCaracteristique(type).Modificateur;
        }

        public Caracteristique GetCaracteristique(TypeCaracteristique type)
        {
            return _caracteristiques.First(car => car.MonType == type);
        }

        [ContextMenu("Calculer les points de vies")]
        public void CalculPointDeVie()
        {
            _pointDeVie = (int)_deDePointDeVie + GetCaracteristiqueModificateur(TypeCaracteristique.Constitution);
        }

        [ContextMenu("Calculer les points de défense")]
        public void CalculPointDeDefense()
        {
            _pointDeDefense = 10 + GetCaracteristiqueModificateur(TypeCaracteristique.Dexterite) +
                              _equipements.OfType<Protection>().Sum(protection => protection.ModificateurDArmure);
        }

        [ContextMenu("Calculer les mod. d'attaque")]
        public void CalculModDAttaque()
        {
            _modAttaqueContact = GetCaracteristiqueModificateur(TypeCaracteristique.Force) + 1;
            _modAttaqueDistance = GetCaracteristiqueModificateur(TypeCaracteristique.Dexterite) + 1;


            if (_profil == _Magicien)
                _modAttaqueMagique = GetCaracteristiqueModificateur(TypeCaracteristique.Intelligence) + 1;
            else if (_profil == _Pretre)
                _modAttaqueMagique = GetCaracteristiqueModificateur(TypeCaracteristique.Sagesse) + 1;
            else
                _modAttaqueMagique = 0;
        }

        public void SetCaracterisicValue(TypeCaracteristique typeCaracteristique, int i)
        {
            var caracteristic = _caracteristiques.FirstOrDefault(car => car.MonType == typeCaracteristique);

            if (caracteristic != null)
            {
                caracteristic.Valeur = i;
            }
        }

        public void EquipementsClear()
        {
#if UNITY_EDITOR
            foreach (var equipement in Equipements)
            {


                UnityEditor.AssetDatabase.RemoveObjectFromAsset(equipement);
                

            }

            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
            Equipements.Clear();
        }
    }

    public enum Genre
    {
        Neutre,
        Masculin,
        Feminin
        
    }
}