
using Koboct.Services;
using UnityEngine;

namespace Koboct.UI
{
    public class SetPersoValues : MonoBehaviour
    {
        [SerializeField] private ServiceCreationPersonnage _monService;

        public void SetJoueurName(string name)
        {
            _monService.SetNomJoueur(name);
        }
    }
}