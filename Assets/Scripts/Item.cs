using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public int healAmount; // Soins donnés par l'objet

    public Item(string name, string desc, int heal)
    {
        itemName = name;
        description = desc;
        healAmount = heal;
    }
}