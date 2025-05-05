using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollFichePerso : MonoBehaviour
{
    public RectTransform fichePerso;
    public float animationDuration = 0.5f;
    public Vector2 positionHaute;
    public Vector2 positionBasse;

    public RectTransform flecheBouton; // <-- référence à l’image à faire tourner

    private bool estEnBas = false;

    public void ToggleScroll()
    {
        StopAllCoroutines();
        if (estEnBas)
        {
            StartCoroutine(ScrollVers(positionHaute));
            RotationFleche(0f);
        }
        else
        {
            StartCoroutine(ScrollVers(positionBasse));
            RotationFleche(180f);
        }
        estEnBas = !estEnBas;
    }

    private IEnumerator ScrollVers(Vector2 cible)
    {
        Vector2 start = fichePerso.anchoredPosition;
        float temps = 0f;

        while (temps < animationDuration)
        {
            temps += Time.deltaTime;
            float t = temps / animationDuration;
            fichePerso.anchoredPosition = Vector2.Lerp(start, cible, t);
            yield return null;
        }

        fichePerso.anchoredPosition = cible;
    }

    private void RotationFleche(float angle)
    {
        if (flecheBouton != null)
        {
            flecheBouton.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}