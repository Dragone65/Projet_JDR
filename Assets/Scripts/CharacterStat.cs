using System.Collections.Generic;
using UnityEngine;

namespace Koboct.Data
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private List<Characteristique> _characteristiques = new();
        [SerializeField] private List<Equipement> _inventaire = new();
        [SerializeField] private Arme _armeEquipee;
        [SerializeField] private Protection _armureEquipee;

        public void AjouterEquipement(Equipement equipement)
        {
            _inventaire.Add(equipement);
        }

        public void EquiperArme(Arme arme)
        {
            _armeEquipee = arme;
            Debug.Log($"Arme équipée : {arme.name}");
        }

        public void EquiperProtection(Protection protection)
        {
            _armureEquipee = protection;
            Debug.Log($"Protection équipée : {protection.name}");
        }

        public Arme GetArmeEquipee()
        {
            return _armeEquipee;
        }

        public Protection GetProtectionEquipee()
        {
            return _armureEquipee;
        }
        public int GetValeur(TypeCharacteristique type)
        {
            return _characteristiques.Find(car => car.MonType == type)?.Valeur ?? 0;
        }

        public int GetModificateur(TypeCharacteristique type)
        {
            return _characteristiques.Find(car => car.MonType == type)?.Modificateur ?? 0;
        }

        public List<Equipement> GetInventaire()
        {
            return _inventaire;
        }
    }
}