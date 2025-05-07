using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "PNJ", menuName = "PNJ", order = 0)]
    public class PNJ : ScriptableObject
    {
        [SerializeField] public string _nom;
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

        [SerializeField] public bool _complete;

        public List<Arme> armes = new();
        public List<Protection> protections = new();

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

        internal int GetCaracteristiqueModificateur(TypeCharacteristique testCaracteristique)
        {
            throw new NotImplementedException();
        }

}

}