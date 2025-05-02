using UnityEngine;
using Koboct.Data;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Dialogue dialogueAPresenter;
    public CharacterStats playerStats;
    public CharacterStats Joueur => GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterStats>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            dialogueAPresenter = null;
        }
    }

    public void LancerDialogueAuRetour(Dialogue dialogue)
    {
        dialogueAPresenter = dialogue;
    }

    public Dialogue RecupererDialogueEtReset()
    {
        Dialogue d = dialogueAPresenter;
        dialogueAPresenter = null;
        return d;
    }
}