using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Koboct.Data;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TMP_Text pnjDialogueText;
    public Transform choixContainer;
    public Button choixPrefab;
    public GameObject dialoguePanel;

    public event Action OnDialogueStart; // Événement pour désactiver les PNJ
    public event Action OnDialogueEnd;   // Événement pour réactiver les PNJ

    private Dialogue dialogueActuel;
    private CharacterStats playerStats;
    private bool peutQuitter = false;

    void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }
    void Start()
    {
        
        Dialogue dialogue = GameManager.Instance.RecupererDialogueEtReset();
        Debug.Log("Dialogue récupéré : " + dialogue);
        if (dialogue != null)
        {
            CommencerDialogue(dialogue, GameManager.Instance.playerStats);
        }
    }

    public void CommencerDialogue(Dialogue dialogue, CharacterStats stats)
    {
        if (dialoguePanel.activeSelf) return; // Empêche d'ouvrir un dialogue si un autre est actif

        dialogueActuel = dialogue;
        playerStats = stats;
        dialoguePanel.SetActive(true);
        peutQuitter = false;
        OnDialogueStart?.Invoke(); // Désactive les PNJ pendant le dialogue
        Invoke(nameof(ActiverFermeture), 0.5f);
        AfficherDialogue();
    }

    void ActiverFermeture()
    {
        peutQuitter = true;
    }
    void AppliquerReaction()
    {
        foreach (ReactionsOption reaction in dialogueActuel.reactions)
        {
            if (playerStats.GetRace() == reaction.raceCible)
            {
                dialogueActuel = reaction.reactionRace;
                break;
            }
        }
    }
    void AfficherDialogue()
    {
        AppliquerReaction();
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
            int jet = UnityEngine.Random.Range(1, 20) + playerStats.GetModificateur(option.testCaracteristique);

            if (jet >= option.difficulteTest)
            {
                dialogueActuel = option.reponseReussite;
            }
            else if (jet >= option.seuilMoyen) 
            {
                dialogueActuel = option.reponseMoyen;
            }
            else
            {
                dialogueActuel = option.reponseEchec;
            }
        }
        if (!string.IsNullOrEmpty(option.sceneSuivante))
        {
            SceneManager.LoadScene(option.sceneSuivante);
            return; 
        }
        if (dialogueActuel != null)
        {
            AfficherDialogue();
        }
        else
        {
            QuitterDialogue();
        }
    }

    public void QuitterDialogue()
    {
        if (peutQuitter)
        {
            dialoguePanel.SetActive(false);
            OnDialogueEnd?.Invoke(); // Réactive les PNJ après le dialogue
        }
    }

    public bool EstDialogueOuvert()
    {
        return dialoguePanel.activeSelf;
    }
}