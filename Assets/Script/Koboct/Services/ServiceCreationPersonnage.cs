using System;
using Koboct.Data;
using UnityEngine;

namespace Koboct.Services
{
    [CreateAssetMenu(fileName = "persoCreator", menuName = "Créateur de perso", order = 0)]
    public class ServiceCreationPersonnage : ScriptableObject
    {
        [SerializeField] private Personnage _monPersonnage;

        [SerializeField] private ServiceLancerDeDe _monServiceDeLanceDeDe;
        [SerializeField] private int[] _monResultatJetCharacteristique;

        private void OnEnable()
        {
            Reset();
        }

        public void SetNomJoueur(string nomJoueur)
        {
            _monPersonnage.NomJoueur = nomJoueur;
        }

        private void Reset()
        {
            _monResultatJetCharacteristique = null;
        }

        [ContextMenu("Lancer Dé Charactèristique")]
        public void LancerDeCharacteristique()
        {
            _monPersonnage.Reset();
            _monServiceDeLanceDeDe.LancerDesCharacteristiques(RetourResultatLancerCharacterisque);
        }

        public void RetourResultatLancerCharacterisque(int[] resultat)
        {
            _monResultatJetCharacteristique = resultat;
        }

        [ContextMenu("Valider Dé Charactèristique")]
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

        private bool ValiderResultatDes()
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                sum += Characteristique.CalculModificateur(_monResultatJetCharacteristique[i]);
            }

            return sum > 3;
        }
    }
}