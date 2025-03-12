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
            int attackRoll = RollDice(20) + playerStats.GetModifier(playerStats.strength); // Jet d'attaque + modificateur
            int damage = 0;

            if (attackRoll >= 10) // Seuil de réussite
            {
                damage = RollDice(6) + playerStats.GetModifier(playerStats.strength); // Dégâts + modificateur
                enemyHP -= Mathf.Max(damage, 1); // Empêche les dégâts négatifs
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

        int attackRoll = RollDice(20) + playerStats.GetModifier(playerStats.strength);
        int damage = 0;

        if (attackRoll >= 10)
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