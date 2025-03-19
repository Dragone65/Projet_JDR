using Koboct.Data;
using UnityEngine;

public class PNJInteract : MonoBehaviour
{
    public Dialogue dialogue;
    public CharacterStats playerStats;

    void OnMouseDown()
    {
        DialogueManager.Instance.CommencerDialogue(dialogue, playerStats);
    }
}