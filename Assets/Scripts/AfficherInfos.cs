using Koboct.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AfficherInfos : MonoBehaviour
{
    [SerializeField] private Personnage _personnage;

    [Header("Champs de Texte UI")]
    [SerializeField] private TextMeshProUGUI Niveau;
    [SerializeField] private TextMeshProUGUI DÈDeVie;
    [SerializeField] private TextMeshProUGUI AttaqueContact;
    [SerializeField] private TextMeshProUGUI AttaqueDistance;
    [SerializeField] private TextMeshProUGUI AttaqueMagique;
    [SerializeField] private TextMeshProUGUI Arme1;
    [SerializeField] private TextMeshProUGUI DegatsArme1;
    [SerializeField] private TextMeshProUGUI Arme2;
    [SerializeField] private TextMeshProUGUI DegatsArme2;
    [SerializeField] private TextMeshProUGUI Pv;
    [SerializeField] private TextMeshProUGUI Defense;
    [SerializeField] private TextMeshProUGUI Armure1;
    [SerializeField] private TextMeshProUGUI Armure2;
    [SerializeField] private TextMeshProUGUI ModArmure1;
    [SerializeField] private TextMeshProUGUI ModArmure2;
    [SerializeField] private TextMeshProUGUI Voie1;
    [SerializeField] private TextMeshProUGUI Voie2;
    [SerializeField] private TextMeshProUGUI Voie3;
    [SerializeField] private TextMeshProUGUI Capacite11;
    [SerializeField] private TextMeshProUGUI Capacite21;
    [SerializeField] private TextMeshProUGUI Capacite31;
    [SerializeField] private TextMeshProUGUI Capacite12;
    [SerializeField] private TextMeshProUGUI Capacite22;
    [SerializeField] private TextMeshProUGUI Capacite32;
    [SerializeField] private TextMeshProUGUI Capacite13;
    [SerializeField] private TextMeshProUGUI Capacite23;
    [SerializeField] private TextMeshProUGUI Capacite33;
    [SerializeField] private TextMeshProUGUI Capacite14;
    [SerializeField] private TextMeshProUGUI Capacite24;
    [SerializeField] private TextMeshProUGUI Capacite34;
    [SerializeField] private TextMeshProUGUI Bourse;
    [SerializeField] private TextMeshProUGUI Inventaire1;
    [SerializeField] private TextMeshProUGUI Inventaire2;
    [SerializeField] private TextMeshProUGUI Inventaire3;
    [SerializeField] private TextMeshProUGUI Inventaire4;
    [SerializeField] private TextMeshProUGUI Inventaire5;
    [SerializeField] private TextMeshProUGUI Inventaire6;
    [SerializeField] private TextMeshProUGUI Inventaire7;
    [SerializeField] private TextMeshProUGUI Inventaire8;
    [SerializeField] private TextMeshProUGUI Inventaire9;
    [SerializeField] private TextMeshProUGUI Inventaire10;

    public void Update()
    {
        if (_personnage._complete == true)
        {
            Niveau.text = "1";
            DÈDeVie.text = _personnage._deDePointDeVie.ToString();
            AttaqueContact.text = _personnage._modAttaqueContact.ToString();
            AttaqueDistance.text = _personnage._modAttaqueDistance.ToString();
            AttaqueMagique.text = _personnage._modAttaqueMagique.ToString();
            Pv.text = _personnage._pointDeVie.ToString();
            Defense.text = _personnage._pointDeDefense.ToString();
            Bourse.text = _personnage._bourse.ToString();

            Voie1.text = _personnage.Voie1?.name;
            Voie2.text = _personnage.Voie2?.name;
            Voie3.text = _personnage.Voie3?.name;

            List<TextMeshProUGUI> voieTexts = new() { Capacite11, Capacite12, Capacite13, Capacite14 };
            for (int i = 0; i < _personnage.Voie1?.Capacites.Count && i < 4; i++)
                voieTexts[i].text = _personnage.Voie1.Capacites[i].name;

            voieTexts = new() { Capacite21, Capacite22, Capacite23, Capacite24 };
            for (int i = 0; i < _personnage.Voie2?.Capacites.Count && i < 4; i++)
                voieTexts[i].text = _personnage.Voie2.Capacites[i].name;

            voieTexts = new() { Capacite31, Capacite32, Capacite33, Capacite34 };
            for (int i = 0; i < _personnage.Voie3?.Capacites.Count && i < 4; i++)
                voieTexts[i].text = _personnage.Voie3.Capacites[i].name;

            List<Equipement> equipements = _personnage._equipements;
            List<Arme> armes = new();
            List<Protection> protections = new();

            foreach (var e in equipements)
            {
                if (e is Arme arme) armes.Add(arme);
                else if (e is Protection protection) protections.Add(protection);
            }

            if (armes.Count > 0)
            {
                Arme1.text = armes[0].name;
                DegatsArme1.text = armes[0]._typeDeDeDegats.ToString();
            }
            if (armes.Count > 1)
            {
                Arme2.text = armes[1].name;
                DegatsArme2.text = armes[1]._typeDeDeDegats.ToString();
            }

            if (protections.Count > 0)
            {
                Armure1.text = protections[0].name;
                ModArmure1.text = protections[0].ModificateurDArmure.ToString();
            }
            if (protections.Count > 1)
            {
                Armure2.text = protections[1].name;
                ModArmure2.text = protections[1].ModificateurDArmure.ToString();
            }

            List<TextMeshProUGUI> inventaireFields = new()
            {
                Inventaire1, Inventaire2, Inventaire3, Inventaire4, Inventaire5,
                Inventaire6, Inventaire7, Inventaire8, Inventaire9, Inventaire10
            };

            List<Equipement> objetsInventaire = new();
            foreach (var e in equipements)
            {
                if (e is not Arme && e is not Protection)
                    objetsInventaire.Add(e);
            }

            for (int i = 0; i < objetsInventaire.Count && i < inventaireFields.Count; i++)
            {
                inventaireFields[i].text = objetsInventaire[i].name;
            }
        }
    }
}

