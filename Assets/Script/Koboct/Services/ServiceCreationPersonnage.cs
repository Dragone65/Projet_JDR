using System;
using System.Collections.Generic;
using System.Linq;
using Koboct.Data;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Koboct.Services
{
    [CreateAssetMenu(fileName = "persoCreator", menuName = "Créateur de perso", order = 0)]
    public class ServiceCreationPersonnage : ScriptableObject
    {
        [SerializeField] private Personnage _monPersonnage;

        [SerializeField] private ServiceLancerDeDe _monServiceDeLanceDeDe;

        [SerializeField] private int[] _monResultatJetCaracteristique;

        [FormerlySerializedAs("_listeRacesDisponible")]
        public List<Race> ListeRacesDisponible = new();

        [FormerlySerializedAs("_listeProfilsDisponible")]
        public List<Profil> ListeProfilsDisponible = new();


        private void OnEnable()
        {
#if UNITY_EDITOR
            ListeRacesDisponible = AssetDatabase.FindAssets("t:Race")
                .Select(guid => AssetDatabase.LoadAssetAtPath<Race>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToList();

            ListeProfilsDisponible = AssetDatabase.FindAssets("t:Profil")
                .Select(guid => AssetDatabase.LoadAssetAtPath<Profil>(AssetDatabase.GUIDToAssetPath(guid)))
                .ToList();
#endif

            Reset();
        }

        public Caracteristique GetCaracteristique(TypeCaracteristique type)
        {
            return _monPersonnage.GetCaracteristique(type);
        }

        public int[] MonResultatJetCaracteristique => _monResultatJetCaracteristique;

        public void SetNomJoueur(string nomJoueur)
        {
            _monPersonnage.NomJoueur = nomJoueur;
        }
        public void SetNomPersonnage(string nom)
        {
            _monPersonnage._nom = nom;
        }

        public void SetSexe(Genre sexe)
        {
            _monPersonnage._sexe = sexe;
        }

        public void SetAge(int age)
        {
            _monPersonnage._age = age;
        }

        public void SetPoids(int poids)
        {
            _monPersonnage._poids = poids;
        }

        public void SetTaille(int taille)
        {
            _monPersonnage._taille = taille;
        }
        public void SetDescription(string description)
        {
            _monPersonnage._description = description;
        }


        private void Reset()
        {
            _monResultatJetCaracteristique = null;
        }

        [ContextMenu("Lancer Dé Caractèristique")]
        public void LancerDeCaracteristique()
        {
            _monServiceDeLanceDeDe.LancerDesCaracteristiques(RetourResultatLancerCaracterisque);
        }

        public void LancerDeCaracteristiqueAvecValidation()
        {
            _monServiceDeLanceDeDe.LancerDesCaracteristiques(RetourResultatLancerCaracterisqueValid);

        }

        public void RetourResultatLancerCaracterisque(int[] resultat)
        {
            _monResultatJetCaracteristique = resultat.OrderByDescending(v => v).ToArray();

        }

        public void RetourResultatLancerCaracterisqueValid(int[] resultat)
        {
            _monResultatJetCaracteristique = resultat.OrderByDescending(v => v).ToArray();
            if (!ValiderResultatDes())
                LancerDeCaracteristiqueAvecValidation();
        }

        [ContextMenu("Valider Dé Caractèristique")]
        public void ValiderDeCaracteristique()

        {
            Debug.Log(ValiderResultatDes());
        }

        [ContextMenu("Auto Assignation valeurs dés au personnage")]
        public void AutoAssign()
        {
            _monPersonnage.SetCaracterisicValue(TypeCaracteristique.Force, _monResultatJetCaracteristique[0]);
            _monPersonnage.SetCaracterisicValue(TypeCaracteristique.Dexterite, _monResultatJetCaracteristique[1]);
            _monPersonnage.SetCaracterisicValue(TypeCaracteristique.Constitution, _monResultatJetCaracteristique[2]);
            _monPersonnage.SetCaracterisicValue(TypeCaracteristique.Intelligence, _monResultatJetCaracteristique[3]);
            _monPersonnage.SetCaracterisicValue(TypeCaracteristique.Intelligence, _monResultatJetCaracteristique[3]);
            _monPersonnage.SetCaracterisicValue(TypeCaracteristique.Sagesse, _monResultatJetCaracteristique[4]);
            _monPersonnage.SetCaracterisicValue(TypeCaracteristique.Charisme, _monResultatJetCaracteristique[5]);
        }

        public bool ValiderResultatDes()
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                sum += Caracteristique.CalculModificateur(_monResultatJetCaracteristique[i]);
            }
#if UNITY_EDITOR
            Debug.Log(sum);
#endif
            return sum >= 3;
        }

        public void SetCaracteristique(TypeCaracteristique myCarac, int selectedValue)
        {
            _monPersonnage.SetCaracterisicValue(myCarac, selectedValue);
        }

        public void ChangeRace(Race race)
        {
            var actualRace = _monPersonnage.Race;
            if (actualRace != null)
                actualRace.RemoveCaracteristiqueModificateur(_monPersonnage);


            if (race == null) return;

            _monPersonnage.Race = race;
            _monPersonnage.Race.ApplyCaracteristiqueModificateur(_monPersonnage);
        }

        private void RemoveAndAddCapacitiesToVoie(Voie sourceVoie, Voie targetVoie)
        {
            if (sourceVoie == null || targetVoie == null) return;

            foreach (var capacite in targetVoie.Capacites)
            {
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.RemoveObjectFromAsset(capacite);
#endif
            }

            targetVoie.Capacites.Clear();
            foreach (var capacite in sourceVoie.Capacites)
            {
                var capacitelone = Instantiate(capacite);
                capacitelone.name = capacitelone.name.Replace("(Clone)", string.Empty);

#if UNITY_EDITOR
                UnityEditor.AssetDatabase.AddObjectToAsset(capacitelone, _monPersonnage);
                UnityEditor.EditorUtility.SetDirty(capacitelone);
#endif
                targetVoie.Capacites.Add(capacitelone);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(_monPersonnage);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        public void ChangeProfil(Profil profil)
        {
            _monPersonnage.EquipementsClear();

            if (profil == null) return;

            _monPersonnage.Profil = profil;
            _monPersonnage._deDePointDeVie = profil.DeDePointDeVie;
            _monPersonnage._bourse = profil._argentDeDepart;

            // Affecter les voies du profil au personnage
            _monPersonnage.Voie1 = profil.Voies.ElementAtOrDefault(0);
            _monPersonnage.Voie2 = profil.Voies.ElementAtOrDefault(1);
            _monPersonnage.Voie3 = profil.Voies.ElementAtOrDefault(2);

            // Copier les capacités de chaque voie
            RemoveAndAddCapacitiesToVoie(_monPersonnage.Voie1, _monPersonnage.Voie1);
            RemoveAndAddCapacitiesToVoie(_monPersonnage.Voie2, _monPersonnage.Voie2);
            RemoveAndAddCapacitiesToVoie(_monPersonnage.Voie3, _monPersonnage.Voie3);

            foreach (var equipement in profil.EquimentsDeBase)
            {
                var equipementClon = Instantiate(equipement);

                equipementClon.name = equipementClon.name.Replace("(Clone)", string.Empty);
#if UNITY_EDITOR


                UnityEditor.AssetDatabase.AddObjectToAsset(equipementClon, _monPersonnage);
                UnityEditor.EditorUtility.SetDirty(equipementClon);
#endif
                _monPersonnage.Equipements.Add(equipementClon);
            }


#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(_monPersonnage);
            // Save all changes to disk
            UnityEditor.AssetDatabase.SaveAssets();

#endif
        // public void SetCharacteristique(TypeCharacteristique myCarac, int selectedValue)
        // {
        //     _monPersonnage.SetCharacterisicValue(myCarac, selectedValue);
        // }

        // public void ChangeRace(Race race)
        // {
        //     var actualRace = _monPersonnage.Race;
        //     if (actualRace != null)
        //         actualRace.RemoveCharacteristiqueModificateur(_monPersonnage);


        //     if (race == null) return;

        //     _monPersonnage.Race = race;
        //     _monPersonnage.Race.ApplyCharacteristiqueModificateur(_monPersonnage);

        // }
        }
    }
}    
