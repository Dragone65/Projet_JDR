using System.Collections.Generic;
using System.Linq;
using Koboct.Data;
using Koboct.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Koboct.UI
{
    public class CaracDropDown : MonoBehaviour
    {
        public TMP_Dropdown _caracDropdown;

        public ServiceCreationPersonnage Service;

        // A static list representing the remaining values for all dropdowns
        public static List<int> remainingValues = new List<int>();
        public TMP_Text _modText;
        public TypeCaracteristique MyCarac;
        [SerializeField] private Caracteristique caracteristique;
        public UnityEvent CaracteristiqueChoiceFinished = new();


        private void Start()
        {
            if (_caracDropdown == null)
                _caracDropdown = GetComponent<TMP_Dropdown>();
            remainingValues = Service.MonResultatJetCaracteristique.ToList();

            // Populate the dropdown
            RefreshDropdown();

            // Add a listener for when the value is selected
            _caracDropdown.onValueChanged.AddListener(OnValueSelected);
            _modText.text = string.Empty;
            caracteristique = Service.GetCaracteristique(MyCarac);
            caracteristique.OnValeurChange.AddListener(CaracChange);
        }

        private void CaracChange(int arg0)
        {
            var modificateur = caracteristique.Modificateur;
            _modText.text = modificateur >= 0 ? $"+{modificateur}" : modificateur.ToString();
            _caracDropdown.captionText.text = caracteristique.Valeur.ToString();
        }

        // Populates the dropdown with remaining values
        private void RefreshDropdown()
        {
            // Clear previous options
            _caracDropdown.ClearOptions();

            // Convert the remaining values to dropdown options
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            options.Add(new TMP_Dropdown.OptionData("--")); // Default placeholder

            foreach (int value in remainingValues)
            {
                options.Add(new TMP_Dropdown.OptionData(value.ToString()));
            }

            // Update dropdown with new options
            _caracDropdown.AddOptions(options);
            _caracDropdown.value = 0; // Set to default value
        }

        // Called when a value is selected from the dropdown
        private void OnValueSelected(int index)
        {
            // Ignore the default option
            if (index == 0) return;

            // Disable this dropdown
            _caracDropdown.interactable = false;

            // Get the selected value
            int selectedValue = remainingValues[index - 1];
            Service.SetCaracteristique(MyCarac, selectedValue);

            // Remove the selected value from the list of remaining values
            remainingValues.Remove(selectedValue);

            // Refresh dropdowns in other instances of this script
            RefreshOtherDropdowns();
        }

        // Updates all dropdowns in other instances of this script
        private void RefreshOtherDropdowns()
        {
            // Find all instances of the CaracDropDown script
            CaracDropDown[] dropdowns = FindObjectsOfType<CaracDropDown>();
            bool atLeastOneDropdownFound = false;
            foreach (CaracDropDown dropdown in dropdowns)
            {
                // Skip disabled dropdowns
                if (dropdown._caracDropdown.interactable)
                {
                    dropdown.RefreshDropdown();
                    atLeastOneDropdownFound = true;
                }
            }

            if (!atLeastOneDropdownFound)
            {
#if UNITY_EDITOR
                Debug.Log("No dropdown found. Cara choice finished");
#endif
                CaracteristiqueChoiceFinished.Invoke();
            }
        }
    }
}