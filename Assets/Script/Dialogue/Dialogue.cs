using Koboct.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public string textePNJ;
    public List<DialogueOption> options = new List<DialogueOption>(); // Liste des options de réponse
    public List<ReactionsOption> reactions = new List<ReactionsOption>();
}

[Serializable]
public class DialogueOption
{
    public string texteJoueur;
    public TypeCharacteristique testCaracteristique;
    public int difficulteTest;
    public int seuilMoyen; 
    public Dialogue reponseReussite;
    public Dialogue reponseMoyen; 
    public Dialogue reponseEchec;
    public string sceneSuivante;
}
[Serializable]
public class ReactionsOption
{
    public Race raceCible;
    public Dialogue reactionRace;
}