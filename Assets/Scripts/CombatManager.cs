using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Koboct.Data;
using UnityEngine.SceneManagement;
using System.Linq;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public TMP_Text actionText;
    public TMP_Text playerHPText, enemyHPText;
    public Button attackButton, inventoryButton, fleeButton;
    public TMP_Text playerDefenseText;
    public TMP_Text ennemiDefenseText;

    public GameObject inventoryPanel; // UI de l'inventaire
    public Transform inventoryContainer; // Parent des boutons d'inventaire
    public Button itemButtonPrefab; // Préfabriqué de bouton pour les objets

    public Personnage joueurPersonnage;
    public PNJ ennemiPersonnage;

    public Dialogue dialogueRetour;

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
        joueurPersonnage.CalculPointDeVie(); 
        ennemiPersonnage.CalculPointDeVie();

        playerHP = joueurPersonnage._pointDeVie;
        enemyHP = ennemiPersonnage._pointDeVie;

        UpdateUI();
        actionText.text = "Un ennemi apparaît !";

        attackButton.onClick.AddListener(PlayerAttack);
        fleeButton.onClick.AddListener(FleeBattle);
        //inventoryButton.onClick.AddListener(OpenInventory);
    }

    void PlayerAttack()
    {
        if (playerTurn)
        {
            if (playerMissedTurn)
            {
                actionText.text = "Vous êtes encore déséquilibré et perdez votre tour !";
                playerMissedTurn = false;
                playerTurn = false;
                StartCoroutine(EnemyTurn());
                return;
            }

            Arme armeAttaquant = joueurPersonnage.Equipements.OfType<Arme>().FirstOrDefault();
            Debug.Log(armeAttaquant);
            int diceType = armeAttaquant != null ? (int)armeAttaquant._typeDeDeDegats : 4; // Par défaut, un dé à 6 faces
            int modAttaque = armeAttaquant._contact ? joueurPersonnage._modAttaqueContact : joueurPersonnage._modAttaqueDistance;
            int attackRoll = RollDice(20) + modAttaque;

            if (attackRoll - modAttaque == 20) // Coup critique
            {
                int critDamage = (RollDice(diceType) + modAttaque) * 2;
                enemyHP -= Mathf.Max(critDamage, 1);
                actionText.text = $"Coup critique ! Vous infligez {critDamage} points de dégâts !";
            }
            else if (attackRoll - modAttaque == 1) // Échec critique
            {
                int outcome = Random.Range(0, 3);
                switch (outcome)
                {
                    case 0:
                        actionText.text = "Échec critique ! Votre attaque échoue complètement.";
                        break;
                    case 1:
                        actionText.text = "Échec critique ! Vous perdez votre prochain tour.";
                        playerMissedTurn = true;
                        break;
                    case 2:
                        actionText.text = "Échec critique ! Vous vous blessez légèrement.";
                        playerHP -= 1;
                        break;
                }
            }
            else if (attackRoll >= ennemiPersonnage._pointDeDefense) // Attaque réussie
            {
                int damage = RollDice(diceType) + modAttaque;
                enemyHP -= Mathf.Max(damage, 1);
                actionText.text = $"Vous attaquez et infligez {damage} points de dégâts.";
            }
            else // Attaque échouée
            {
                actionText.text = "Votre attaque échoue.";
            }

            
            if (enemyHP <= 0)
            {
                actionText.text += "\nVous avez été vaincu... ";
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

        Arme armeAttaquant = ennemiPersonnage.Equipements.OfType<Arme>().FirstOrDefault();
        int diceType = armeAttaquant != null ? (int)armeAttaquant._typeDeDeDegats : 4; // Par défaut, un dé à 6 faces
        int modAttaque = armeAttaquant._contact ? ennemiPersonnage._modAttaqueContact : ennemiPersonnage._modAttaqueDistance;
        int attackRoll = RollDice(20) + modAttaque;

        if (attackRoll - modAttaque == 20) // attaque critique
        {
            int critDamage = (RollDice(diceType) + modAttaque) * 2;
            playerHP -= Mathf.Max(critDamage, 1);
            actionText.text = $" L'ennemi fait un COUP CRITIQUE ! (Jet: 20)\nIl vous inflige {critDamage} dégâts !";
        }
        else if (attackRoll - modAttaque == 1) // echec critique
        {
            HandleCriticalFailure(false);
        }
        else if (attackRoll >= joueurPersonnage._pointDeDefense) // Comparer le jet d'attaque à la défense
        {
            int damage = RollDice(diceType) + modAttaque;
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
    
    //void OpenInventory()
    //{
    //    actionText.text = " Choisissez un équipement à utiliser !";

    //    foreach (Transform child in inventoryContainer)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    foreach (Equipement equipement in joueurPersonnage.GetInventaire())
    //    {
    //        Button itemButton = Instantiate(itemButtonPrefab, inventoryContainer);
    //        itemButton.GetComponentInChildren<TMP_Text>().text = equipement.name;

    //        if (equipement is Arme arme)
    //        {
    //            itemButton.onClick.AddListener(() => EquiperArme(arme));
    //        }
    //        else if (equipement is Protection protection)
    //        {
    //            itemButton.onClick.AddListener(() => EquiperProtection(protection));
    //        }
    //    }

    //    inventoryPanel.SetActive(true);
    //}

    //void EquiperArme(Arme arme)
    //{
    //    joueurPersonnage.EquiperArme(arme);
    //    actionText.text = $" Vous équipez {arme.name} !";
    //    inventoryPanel.SetActive(false);
    //}

    //void EquiperProtection(Protection protection)
    //{
    //    joueurPersonnage.EquiperProtection(protection);
    //    actionText.text = $" Vous équipez {protection.name} !";
    //    inventoryPanel.SetActive(false);
    //}

    void FleeBattle()
    {
        actionText.text = "Vous prenez la fuite ! ";
        attackButton.interactable = false;
        fleeButton.interactable = false;
        EndBattle();
    }

    void UpdateUI()
    {
        playerHPText.text = $"PV Joueur: {playerHP}";
        enemyHPText.text = $"PV Ennemi: {enemyHP}";
        playerDefenseText.text = $"Défense : {joueurPersonnage._pointDeDefense}";
        ennemiDefenseText.text = $"Défense : {ennemiPersonnage._pointDeDefense}";
    }

    void EndBattle()
    {
        attackButton.interactable = false;
        fleeButton.interactable = false;
        GameManager.Instance.LancerDialogueAuRetour(dialogueRetour);
        StartCoroutine(RetourALaSceneDialogue());
    }

    IEnumerator RetourALaSceneDialogue()
    {
        yield return new WaitForSeconds(2f); // Attendre un peu pour que le joueur voie le résultat
        SceneManager.LoadScene("Dialogue");
    }

    int RollDice(int sides)
    {
        return Random.Range(1, sides + 1);
    }
}