using System.Collections.Generic;
using Koboct.Services;
using TMPro;
using UnityEngine;


namespace Koboct.UI
{
    public class RaceDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _raceDropdown;

        public ServiceCreationPersonnage Service;

        private void Start()
        {
            if (_raceDropdown == null)
                _raceDropdown = GetComponent<TMP_Dropdown>();
            RefreshDropdonwValues();

            _raceDropdown.onValueChanged.AddListener(OnValueSelected);
        }

        private void OnValueSelected(int arg0)
        {
#if UNITY_EDITOR
            Debug.Log(_raceDropdown.options[arg0].text);
#endif

            if (arg0 == 0)
                Service.ChangeRace(null);
            else
                Service.ChangeRace(Service.ListeRacesDisponible[arg0 - 1]);
        }

        private void RefreshDropdonwValues()
        {
            _raceDropdown.ClearOptions();


            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            options.Add(new TMP_Dropdown.OptionData("--")); // Default placeholder

            foreach (var value in Service.ListeRacesDisponible)
            {
                options.Add(new TMP_Dropdown.OptionData(value.name));
            }

            // Update dropdown with new options
            _raceDropdown.AddOptions(options);
            _raceDropdown.value = 0; // Set to default value
        }
    }
}