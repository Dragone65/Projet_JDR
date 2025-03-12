using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    public TMP_Text actionText;
    public TMP_Text playerHPText, enemyHPText;
    public Button attackButton, inventoryButton, fleeButton;

    public CharacterStats playerStats; // Stats du joueur
    public CharacterStats enemyStats;  // Stats de l'ennemi

    private int playerHP;
    private int enemyHP;
    private bool playerTurn = true;
    private bool playerMissedTurn = false; // Pour gérer la perte de tour après un échec critique

    void Start()
    {
        // Initialisation des PV en fonction de la Constitution
        playerHP = 10 + playerStats.GetModifier(playerStats.constitution);
        enemyHP = 10 + enemyStats.GetModifier(enemyStats.constitution);

        UpdateUI();
        actionText.text = "Un ennemi apparaît !";

        attackButton.onClick.AddListener(PlayerAttack);
        fleeButton.onClick.AddListener(FleeBattle);
        inventoryButton.interactable = false;
    }

    void PlayerAttack()
    {
        if (playerTurn)
        {
            if (playerMissedTurn) // Si le joueur a perdu son tour à cause d'un échec critique
            {
                actionText.text = " Vous êtes encore déséquilibré et perdez votre tour !";
                playerMissedTurn = false;
                playerTurn = false;
                StartCoroutine(EnemyTurn());
                return;
            }

            int attackRoll = RollDice(20) + playerStats.GetModifier(playerStats.strength); // Jet d'attaque
            int damage = 0;

            if (attackRoll - playerStats.GetModifier(playerStats.strength) == 20) // Coup critique (20 naturel)
            {
                int critDamage = (RollDice(6) + playerStats.GetModifier(playerStats.strength)) * 2; // Double les dés
                enemyHP -= Mathf.Max(critDamage, 1);
                actionText.text = $" COUP CRITIQUE ! (Jet: 20) \nVous infligez {critDamage} dégâts !";
            }
            else if (attackRoll - playerStats.GetModifier(playerStats.strength) == 1) // Échec critique (1 naturel)
            {
                HandleCriticalFailure(true);
            }
            else if (attackRoll >= 10) // Attaque normale
            {
                damage = RollDice(6) + playerStats.GetModifier(playerStats.strength);
                enemyHP -= Mathf.Max(damage, 1);
                actionText.text = $"Vous attaquez ! (Jet: {attackRoll})\nDégâts : {damage}";
            }
            else
            {
                actionText.text = $"Vous attaquez ! (Jet: {attackRoll})\nL'attaque échoue...";
            }

            UpdateUI();

            if (enemyHP <= 0)
            {
                actionText.text += "\nL'ennemi est vaincu ! ";
                EndBattle();
            }
            else
            {
                playerTurn = false;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1.5f);

        int attackRoll = RollDice(20) + enemyStats.GetModifier(enemyStats.strength);
        int damage = 0;

        if (attackRoll - enemyStats.GetModifier(enemyStats.strength) == 20) // Coup critique
        {
            int critDamage = (RollDice(4) + enemyStats.GetModifier(enemyStats.strength)) * 2;
            playerHP -= Mathf.Max(critDamage, 1);
            actionText.text = $" L'ennemi fait un COUP CRITIQUE ! (Jet: 20)\nIl vous inflige {critDamage} dégâts !";
        }
        else if (attackRoll - enemyStats.GetModifier(enemyStats.strength) == 1) // Échec critique
        {
            HandleCriticalFailure(false);
        }
        else if (attackRoll >= 10) // Attaque normale
        {
            damage = RollDice(4) + enemyStats.GetModifier(enemyStats.strength);
            playerHP -= Mathf.Max(damage, 1);
            actionText.text = $"L'ennemi attaque ! (Jet: {attackRoll})\nDégâts : {damage}";
        }
        else
        {
            actionText.text = $"L'ennemi attaque ! (Jet: {attackRoll})\nL'attaque échoue.";
        }

        UpdateUI();

        if (playerHP <= 0)
        {
            actionText.text += "\nVous avez été vaincu... ";
            EndBattle();
        }
        else
        {
            playerTurn = true;
        }
    }

    void HandleCriticalFailure(bool isPlayer)
    {
        int failEffect = Random.Range(0, 3); // 3 options possibles
        string target = isPlayer ? "Vous" : "L'ennemi";

        switch (failEffect)
        {
            case 0: // Échec total
                actionText.text = $" {target} rate complètement son attaque !";
                break;

            case 1: // Perte de tour
                actionText.text = $" {target} perd l'équilibre et ne pourra pas attaquer au prochain tour !";
                if (isPlayer) playerMissedTurn = true;
                else playerTurn = true; // Le joueur attaque directement au prochain tour
                break;

            case 2: // Se blesse légèrement
                int selfDamage = Random.Range(1, 3); // 1 ou 2 points de dégâts
                if (isPlayer) playerHP -= selfDamage;
                else enemyHP -= selfDamage;
                actionText.text = $" {target} se blesse tout seul et perd {selfDamage} PV !";
                break;
        }

        UpdateUI();
    }

    void FleeBattle()
    {
        actionText.text = "Vous prenez la fuite ! ";
        attackButton.interactable = false;
        fleeButton.interactable = false;
    }

    void UpdateUI()
    {
        playerHPText.text = $"PV Joueur: {playerHP}";
        enemyHPText.text = $"PV Ennemi: {enemyHP}";
    }

    void EndBattle()
    {
        attackButton.interactable = false;
        fleeButton.interactable = false;
    }

    int RollDice(int sides)
    {
        return Random.Range(1, sides + 1);
    }
}
