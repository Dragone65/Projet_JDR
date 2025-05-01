using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Accueil")
        {
            btn.onClick.AddListener(LancerCreationPersonnage);
        }
        if (scene.name == "SampleScene")
        {
            btn.onClick.AddListener(LancerDialogue);
        }
        if (scene.name == "Dialogue")
        {
            btn.onClick.AddListener(LancerCombat);
        }
        if (scene.name == "Combat")
        {
            btn.onClick.AddListener(LancerDialogue);
        }
    }
    public void LancerCreationPersonnage()
    {
        SceneManager.LoadScene("SampleScene"); 
    }
    public void LancerDialogue()
    {
        SceneManager.LoadScene("Dialogue"); 
    }
    public void LancerCombat()
    {
        SceneManager.LoadScene("Combat"); 
    }
}
