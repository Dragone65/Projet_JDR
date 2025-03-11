using UnityEngine;

[CreateAssetMenu(fileName = "Personnage", menuName = "Scriptable Objects/Personnage")]
public class Personnage : ScriptableObject
{
    public Race race;
    public Skill skill;
    public Weapon Weapon;
}
