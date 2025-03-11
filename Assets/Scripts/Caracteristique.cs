using UnityEngine;

[CreateAssetMenu(fileName = "Caracteristique", menuName = "Scriptable Objects/Caracteristique")]
public class Caracteristique : ScriptableObject
{
    public Typescaracteristique montype;
}

public enum Typescaracteristique
{
    Force = 1, 
    Dexterite = 2,
    Constitution = 3,
    Intelligence = 4,
    Sagesse = 5,
    Charisme = 6,
}