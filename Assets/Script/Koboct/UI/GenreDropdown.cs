using System.Collections.Generic;
using Koboct.Services;
using TMPro;
using UnityEngine;


namespace Koboct.UI
{
    public class GenreDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _genreDropdown;

        public ServiceCreationPersonnage Service;

        private void Start()
        {
            if (_genreDropdown == null)
                _genreDropdown = GetComponent<TMP_Dropdown>();
            RefreshDropdonwValues();

            _genreDropdown.onValueChanged.AddListener(OnValueSelected);
        }

        private void OnValueSelected(int arg0)
        {
#if UNITY_EDITOR
            Debug.Log(_genreDropdown.options[arg0].text);
#endif

            if (arg0 == 0)
                Service.ChangeProfil(null);
            else
                Service.ChangeProfil(Service.ListeProfilsDisponible[arg0 - 1]);
        }

        private void RefreshDropdonwValues()
        {
            _genreDropdown.ClearOptions();


            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            options.Add(new TMP_Dropdown.OptionData("--")); // Default placeholder

            foreach (var value in Service.ListeProfilsDisponible)
            {
                options.Add(new TMP_Dropdown.OptionData(value.name));
            }

            // Update dropdown with new options
            _genreDropdown.AddOptions(options);
            _genreDropdown.value = 0; // Set to default value
        }
    }
}