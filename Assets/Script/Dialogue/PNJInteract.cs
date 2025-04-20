using Koboct.Data;
using UnityEngine;

public class PNJInteract : MonoBehaviour
{
    public Dialogue dialogue;
    public CharacterStats playerStats;
    private Collider pnjCollider;

    void Start()
    {
        pnjCollider = GetComponent<Collider>();
        DialogueManager.Instance.OnDialogueStart += DesactiverPNJ;
        DialogueManager.Instance.OnDialogueEnd += ActiverPNJ;
    }

    void OnDestroy()
    {
        DialogueManager.Instance.OnDialogueStart -= DesactiverPNJ;
        DialogueManager.Instance.OnDialogueEnd -= ActiverPNJ;
    }

    void OnMouseDown()
    {
        if (!DialogueManager.Instance.EstDialogueOuvert())
        {
            DialogueManager.Instance.CommencerDialogue(dialogue, playerStats);
        }
    }

    void DesactiverPNJ()
    {
        pnjCollider.enabled = false;
    }

    void ActiverPNJ()
    {
        pnjCollider.enabled = true;
    }
}