using UnityEngine;

namespace Koboct.Data
{
    [CreateAssetMenu(fileName = "Equipement", menuName = "Equipement", order = 0)]
    public class Equipement : NamedScriptableObject
    {
        [SerializeField] private float _prix;
        [SerializeField] private Sprite _icon;
        [SerializeField] private GameObject _prefab;
    }
}