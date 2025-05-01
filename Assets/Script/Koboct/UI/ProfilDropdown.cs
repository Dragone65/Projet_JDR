using System.Collections.Generic;
using Koboct.Services;
using TMPro;
using UnityEngine;


namespace Koboct.UI
{
    public class ProfilDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _profilDropdown;

        public ServiceCreationPersonnage Service;

        private void Start()
        {
            if (_profilDropdown == null)
                _profilDropdown = GetComponent<TMP_Dropdown>();
            RefreshDropdonwValues();

            _profilDropdown.onValueChanged.AddListener(OnValueSelected);
        }

        private void OnValueSelected(int arg0)
        {
#if UNITY_EDITOR
            Debug.Log(_profilDropdown.options[arg0].text);
#endif

            if (arg0 == 0)
                Service.ChangeProfil(null);
            else
                Service.ChangeProfil(Service.ListeProfilsDisponible[arg0 - 1]);
        }

        private void RefreshDropdonwValues()
        {
            _profilDropdown.ClearOptions();


            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            options.Add(new TMP_Dropdown.OptionData("--")); // Default placeholder

            foreach (var value in Service.ListeProfilsDisponible)
            {
                options.Add(new TMP_Dropdown.OptionData(value.name));
            }

            // Update dropdown with new options
            _profilDropdown.AddOptions(options);
            _profilDropdown.value = 0; // Set to default value
        }
    }
}