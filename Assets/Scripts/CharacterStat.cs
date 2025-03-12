using UnityEditor.PackageManager.UI;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;

    public CharacterStats(int str, int dex, int con, int intel, int wis, int cha)
    {
        strength = str;
        dexterity = dex;
        constitution = con;
        intelligence = intel;
        wisdom = wis;
        charisma = cha;
    }

    public int GetModifier(int stat)
    {
        return (stat - 10) / 2; // Calcul du modificateur
    }
}
