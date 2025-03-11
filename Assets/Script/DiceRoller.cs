using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    public GameObject[] diceObjects; // Référence aux 4 dés
    public TMP_Text[] diceTexts; // Texte des valeurs des dés (TextMeshPro)
    public TMP_Text sommeText; // Texte affichant la somme des 3 meilleurs résultats
    public Button rollButton;

    private int[] diceResults = new int[4];

    void Start()
    {
        foreach (GameObject die in diceObjects) die.SetActive(false);
        foreach (TMP_Text text in diceTexts) text.gameObject.SetActive(false);
        sommeText.gameObject.SetActive(false); // Cacher aussi la somme au début

        rollButton.onClick.AddListener(RollDice);
    }

    void RollDice()
    {
        StartCoroutine(AnimateDice());
    }

    private IEnumerator AnimateDice()
    {
        foreach (GameObject die in diceObjects) die.SetActive(true);
        foreach (TMP_Text text in diceTexts) text.gameObject.SetActive(false);
        sommeText.gameObject.SetActive(false); // Cacher la somme pendant le lancer

        float rollDuration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < rollDuration)
        {
            foreach (GameObject die in diceObjects)
            {
                die.transform.Rotate(Random.Range(200, 500) * Time.deltaTime,
                                     Random.Range(200, 500) * Time.deltaTime,
                                     Random.Range(200, 500) * Time.deltaTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Arrêter les dés et afficher les résultats
        for (int i = 0; i < diceObjects.Length; i++)
        {
            diceObjects[i].transform.rotation = Quaternion.identity; // Stopper la rotation
            diceResults[i] = Random.Range(1, 7); // Générer un résultat entre 1 et 6
            Debug.Log("Dé " + (i + 1) + " : " + diceResults[i]);
            diceTexts[i].text = diceResults[i].ToString();
            diceTexts[i].gameObject.SetActive(true);
        }

        // Calculer la somme des 3 meilleurs résultats
        int somme = diceResults.OrderByDescending(x => x).Take(3).Sum();
        Debug.Log("Resultat : " + somme);
        sommeText.text = "Resultat : " + somme;
        sommeText.gameObject.SetActive(true); // Afficher la somme

        rollButton.interactable = true; // Réactiver le bouton
    }
}