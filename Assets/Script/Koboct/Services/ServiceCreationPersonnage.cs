using System;
using System.Collections.Generic;
using System.Linq;
using Koboct.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Koboct.Services
{
    [CreateAssetMenu(fileName = "persoCreator", menuName = "Créateur de perso", order = 0)]
    public class ServiceCreationPersonnage : ScriptableObject
    {
        [SerializeField] private Personnage _monPersonnage;

        [SerializeField] private ServiceLancerDeDe _monServiceDeLanceDeDe;

        [SerializeField] private int[] _monResultatJetCharacteristique;

        [FormerlySerializedAs("_listeRacesDisponible")]
        public List<Race> ListeRacesDisponible = new();

        [FormerlySerializedAs("_listeProfilsDisponible")]
        public List<Profil> ListeProfilsDisponible = new();

        private void OnEnable()
        {
            Reset();
        }

        public Characteristique GetCharacteristique(TypeCharacteristique type)
        {
            return _monPersonnage.GetCharacteristique(type);
        }

        public int[] MonResultatJetCharacteristique => _monResultatJetCharacteristique;

        public void SetNomJoueur(string nomJoueur)
        {
            _monPersonnage.NomJoueur = nomJoueur;
        }

        private void Reset()
        {
            _monResultatJetCharacteristique = null;
        }

        [ContextMenu("Lancer Dé Caractèristique")]
        public void LancerDeCharacteristique()
        {
            _monPersonnage.Reset();
            _monServiceDeLanceDeDe.LancerDesCharacteristiques(RetourResultatLancerCaracterisque);
        }

        public void LancerDeCharacteristiqueAvecValidation()
        {
            _monPersonnage.Reset();
            _monServiceDeLanceDeDe.LancerDesCharacteristiques(RetourResultatLancerCaracterisqueValid);
        }

        public void RetourResultatLancerCaracterisque(int[] resultat)
        {
            _monResultatJetCharacteristique = resultat.OrderByDescending(v => v).ToArray();
        }

        public void RetourResultatLancerCaracterisqueValid(int[] resultat)
        {
            _monResultatJetCharacteristique = resultat.OrderByDescending(v => v).ToArray();
            if (!ValiderResultatDes())
                LancerDeCharacteristiqueAvecValidation();
        }

        [ContextMenu("Valider Dé Caractèristique")]
        public void ValiderDeCharacteristique()
        {
            Debug.Log(ValiderResultatDes());
        }

        [ContextMenu("Auto Assignation valeurs dés au personnage")]
        public void AutoAssign()
        {
            _monPersonnage.SetCharacterisicValue(TypeCharacteristique.Force, _monResultatJetCharacteristique[0]);
            _monPersonnage.SetCharacterisicValue(TypeCharacteristique.Dexterite, _monResultatJetCharacteristique[1]);
            _monPersonnage.SetCharacterisicValue(TypeCharacteristique.Constitution, _monResultatJetCharacteristique[2]);
            _monPersonnage.SetCharacterisicValue(TypeCharacteristique.Intelligence, _monResultatJetCharacteristique[3]);
            _monPersonnage.SetCharacterisicValue(TypeCharacteristique.Intelligence, _monResultatJetCharacteristique[3]);
            _monPersonnage.SetCharacterisicValue(TypeCharacteristique.Sagesse, _monResultatJetCharacteristique[4]);
            _monPersonnage.SetCharacterisicValue(TypeCharacteristique.Charisme, _monResultatJetCharacteristique[5]);
        }

        public bool ValiderResultatDes()
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                sum += Characteristique.CalculModificateur(_monResultatJetCharacteristique[i]);
            }
#if UNITY_EDITOR
            Debug.Log(sum);
#endif
            return sum >= 3;
        }

        public void SetCharacteristique(TypeCharacteristique myCarac, int selectedValue)
        {
            _monPersonnage.SetCharacterisicValue(myCarac, selectedValue);
        }

        public void ChangeRace(Race race)
        {
            var actualRace = _monPersonnage.Race;
            if (actualRace != null)
                actualRace.RemoveCharacteristiqueModificateur(_monPersonnage);


            if (race == null) return;

            _monPersonnage.Race = race;
            _monPersonnage.Race.ApplyCharacteristiqueModificateur(_monPersonnage);
        }
    }
}