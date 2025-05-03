using TMPro;
using UnityEngine;
using Koboct.Services;
using Koboct.Data;

public class SetAdditionalPersoValues : MonoBehaviour
{
    public ServiceCreationPersonnage service;

    public TMP_InputField inputNomPersonnage;
    public TMP_InputField inputTaille;
    public TMP_InputField inputPoids;
    public TMP_InputField inputAge;
    public TMP_InputField inputDescription;

    public TMP_Dropdown sexeDropdown;

    void Start()
    {
        sexeDropdown.ClearOptions();
        sexeDropdown.AddOptions(new System.Collections.Generic.List<string> { "Neutre", "Masculin", "Féminin"  });

        sexeDropdown.onValueChanged.AddListener(index =>
        {
            service.SetSexe((Genre)index);
        });

        inputNomPersonnage.onValueChanged.AddListener(nom => service.SetNomPersonnage(nom));
        inputTaille.onValueChanged.AddListener(val => { if (int.TryParse(val, out var t)) service.SetTaille(t); });
        inputPoids.onValueChanged.AddListener(val => { if (int.TryParse(val, out var p)) service.SetPoids(p); });
        inputAge.onValueChanged.AddListener(val => { if (int.TryParse(val, out var a)) service.SetAge(a); });
        inputDescription.onValueChanged.AddListener(val => service.SetDescription(val));
    }
}