using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceRoller : MonoBehaviour
{
    public GameObject[] diceObjects; // R�f�rence aux 4 d�s
    public TMP_Text[] diceTexts; // Texte des valeurs des d�s (TextMeshPro)
    public TMP_Text sommeText; // Texte affichant la somme des 3 meilleurs r�sultats
    public Button rollButton;

    private int[] diceResults = new int[4];

    void Start()
    {
        foreach (GameObject die in diceObjects) die.SetActive(false);
        foreach (TMP_Text text in diceTexts) text.gameObject.SetActive(false);
        sommeText.gameObject.SetActive(false); // Cacher aussi la somme au d�but

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

        // Arr�ter les d�s et afficher les r�sultats
        for (int i = 0; i < diceObjects.Length; i++)
        {
            diceObjects[i].transform.rotation = Quaternion.identity; // Stopper la rotation
            diceResults[i] = Random.Range(1, 7); // G�n�rer un r�sultat entre 1 et 6
            Debug.Log("D� " + (i + 1) + " : " + diceResults[i]);
            diceTexts[i].text = diceResults[i].ToString();
            diceTexts[i].gameObject.SetActive(true);
        }

        // Calculer la somme des 3 meilleurs r�sultats
        int somme = diceResults.OrderByDescending(x => x).Take(3).Sum();
        Debug.Log("Resultat : " + somme);
        sommeText.text = "Resultat : " + somme;
        sommeText.gameObject.SetActive(true); // Afficher la somme

        rollButton.interactable = true; // R�activer le bouton
    }
}