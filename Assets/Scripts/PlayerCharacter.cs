using UnityEngine;

public class PlayerCharacter
{
    public CharacterStats stats;

    void Start()
    {
        stats = new CharacterStats(10, 10, 10, 10, 10, 10); // Valeurs par défaut
    }
}
