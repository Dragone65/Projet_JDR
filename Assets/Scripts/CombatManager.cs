using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Koboct.Data;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public TMP_Text actionText;
    public TMP_Text playerHPText, enemyHPText;
    public Button attackButton, inventoryButton, fleeButton;

    public GameObject inventoryPanel; // UI de l'inventaire
    public Transform inventoryContainer; // Parent des boutons d'inventaire
    public Button itemButtonPrefab; // Préfabriqué de bouton pour les objets

    public CharacterStats playerStats;
    public CharacterStats enemyStats;

    private int playerHP;
    private int enemyHP;
    private bool playerTurn = true;
    private bool playerMissedTurn = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerHP = playerStats.GetValeur(TypeCharacteristique.Constitution) + 10;
        enemyHP = enemyStats.GetValeur(TypeCharacteristique.Constitution) + 10;

        UpdateUI();
        actionText.text = "Un ennemi apparaît !";

        attackButton.onClick.AddListener(PlayerAttack);
        fleeButton.onClick.AddListener(FleeBattle);
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    void PlayerAttack()
    {
        if (playerTurn)
        {
            if (playerMissedTurn)
            {
                actionText.text = " Vous êtes encore déséquilibré et perdez votre tour !";
                playerMissedTurn = false;
                playerTurn = false;
                StartCoroutine(EnemyTurn());
                return;
            }

            // Déterminer le type de dés selon l'arme équipée
            int diceType = playerStats.GetArmeEquipee() != null ? (int)playerStats.GetArmeEquipee().TypeDeDeDegats : 6;
            int attackRoll = RollDice(20) + playerStats.GetModificateur(TypeCharacteristique.Force);
            int damage = 0;

            if (attackRoll - playerStats.GetModificateur(TypeCharacteristique.Force) == 20)
            {
                int critDamage = (RollDice(diceType) + playerStats.GetModificateur(TypeCharacteristique.Force)) * 2;
                enemyHP -= Mathf.Max(critDamage, 1);
                actionText.text = $" COUP CRITIQUE ! (Jet: 20) \nVous infligez {critDamage} dégâts !";
            }
            else if (attackRoll - playerStats.GetModificateur(TypeCharacteristique.Force) == 1)
            {
                HandleCriticalFailure(true);
            }
            else if (attackRoll >= 10)
            {
                damage = RollDice(diceType) + playerStats.GetModificateur(TypeCharacteristique.Force);
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

        int attackRoll = RollDice(20) + enemyStats.GetModificateur(TypeCharacteristique.Force);
        int damage = 0;

        if (attackRoll - enemyStats.GetModificateur(TypeCharacteristique.Force) == 20)
        {
            int critDamage = (RollDice(4) + enemyStats.GetModificateur(TypeCharacteristique.Force)) * 2;
            playerHP -= Mathf.Max(critDamage, 1);
            actionText.text = $" L'ennemi fait un COUP CRITIQUE ! (Jet: 20)\nIl vous inflige {critDamage} dégâts !";
        }
        else if (attackRoll - enemyStats.GetModificateur(TypeCharacteristique.Force) == 1)
        {
            HandleCriticalFailure(false);
        }
        else if (attackRoll >= 10)
        {
            damage = RollDice(4) + enemyStats.GetModificateur(TypeCharacteristique.Force);
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
        int failEffect = Random.Range(0, 3);
        string target = isPlayer ? "Vous" : "L'ennemi";

        switch (failEffect)
        {
            case 0:
                actionText.text = $" {target} rate complètement son attaque !";
                break;

            case 1:
                actionText.text = $" {target} perd l'équilibre et ne pourra pas attaquer au prochain tour !";
                if (isPlayer) playerMissedTurn = true;
                else playerTurn = true;
                break;

            case 2:
                int selfDamage = Random.Range(1, 3);
                if (isPlayer) playerHP -= selfDamage;
                else enemyHP -= selfDamage;
                actionText.text = $" {target} se blesse tout seul et perd {selfDamage} PV !";
                break;
        }

        UpdateUI();
    }

    void OpenInventory()
    {
        actionText.text = " Choisissez un équipement à utiliser !";

        foreach (Transform child in inventoryContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Equipement equipement in playerStats.GetInventaire())
        {
            Button itemButton = Instantiate(itemButtonPrefab, inventoryContainer);
            itemButton.GetComponentInChildren<TMP_Text>().text = equipement.name;

            if (equipement is Arme arme)
            {
                itemButton.onClick.AddListener(() => EquiperArme(arme));
            }
            else if (equipement is Protection protection)
            {
                itemButton.onClick.AddListener(() => EquiperProtection(protection));
            }
        }

        inventoryPanel.SetActive(true);
    }

    void EquiperArme(Arme arme)
    {
        playerStats.EquiperArme(arme);
        actionText.text = $" Vous équipez {arme.name} !";
        inventoryPanel.SetActive(false);
    }

    void EquiperProtection(Protection protection)
    {
        playerStats.EquiperProtection(protection);
        actionText.text = $" Vous équipez {protection.name} !";
        inventoryPanel.SetActive(false);
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