using Koboct.Data;
using UnityEngine;

public class PNJInteract : MonoBehaviour
{
    public Dialogue dialogue;
    private Collider pnjCollider;
    public Personnage joueurPersonnage;

    void Start()
    {
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
            DialogueManager.Instance.CommencerDialogue(dialogue, joueurPersonnage);
        }
    }

    void DesactiverPNJ()
    {
        if (pnjCollider == null)
            pnjCollider = GetComponent<Collider>();

        if (pnjCollider != null)
            pnjCollider.enabled = false;
    }

    void ActiverPNJ()
    {
        if (pnjCollider == null)
            pnjCollider = GetComponent<Collider>();

        if (pnjCollider != null)
            pnjCollider.enabled = true;
    }
}