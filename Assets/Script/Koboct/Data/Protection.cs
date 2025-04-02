using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Protection", menuName = "Protection", order = 0)]
    public class Protection : Equipement
    {
        [SerializeField] private bool _armure;
        [SerializeField] private bool _bouclier;

        [Range(1, 10)] [SerializeField] private int _modificateurDArmure;

        public int ModificateurDArmure
        {
            get => _modificateurDArmure;
        }
    }
}