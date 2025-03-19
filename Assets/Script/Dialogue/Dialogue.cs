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
    public string texteJoueur; // Texte du choix du joueur
    public TypeCharacteristique testCaracteristique; // Stat utilisée pour le test (ex: Charisme)
    public int difficulteTest; // Difficulté du jet
    public Dialogue reponseReussite; // Réponse si le test réussit
    public Dialogue reponseEchec; // Réponse si le test échoue
}