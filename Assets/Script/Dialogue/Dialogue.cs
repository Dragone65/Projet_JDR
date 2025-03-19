using Koboct.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public string textePNJ; // Texte affiché par le PNJ

    public List<DialogueOption> options = new List<DialogueOption>(); // Liste des options de réponse
}

[Serializable]
public class DialogueOption
{
    public string texteJoueur;
    public TypeCharacteristique testCaracteristique;
    public int difficulteTest;
    public int seuilMoyen; // Nouveau seuil pour le résultat moyen
    public Dialogue reponseReussite;
    public Dialogue reponseMoyen; // Nouveau champ
    public Dialogue reponseEchec;
}