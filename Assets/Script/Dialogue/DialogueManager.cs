using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using Koboct.Data;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TMP_Text pnjDialogueText;
    public Transform choixContainer;
    public Button choixPrefab;
    public GameObject dialoguePanel;

    private Dialogue dialogueActuel;
    private CharacterStats playerStats;

    void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false); // Désactiver l'UI au début
    }

    public void CommencerDialogue(Dialogue dialogue, CharacterStats stats)
    {
        dialogueActuel = dialogue;
        playerStats = stats;
        dialoguePanel.SetActive(true);
        AfficherDialogue();
    }

    void AfficherDialogue()
    {
        pnjDialogueText.text = dialogueActuel.textePNJ;

        foreach (Transform child in choixContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (DialogueOption option in dialogueActuel.options)
        {
            Button choixBouton = Instantiate(choixPrefab, choixContainer);
            choixBouton.GetComponentInChildren<TMP_Text>().text = option.texteJoueur;
            choixBouton.onClick.AddListener(() => FaireChoix(option));
        }
    }

    void FaireChoix(DialogueOption option)
    {
        if (option.testCaracteristique != TypeCharacteristique.Aucune)
        {
            int jet = Random.Range(1, 20) + playerStats.GetModificateur(option.testCaracteristique);
            if (jet >= option.difficulteTest)
            {
                dialogueActuel = option.reponseReussite;
            }
            else
            {
                dialogueActuel = option.reponseEchec;
            }
        }

        if (dialogueActuel != null)
        {
            AfficherDialogue();
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}