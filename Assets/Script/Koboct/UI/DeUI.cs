using Koboct.Services;
using UnityEngine;

namespace Koboct.UI
{
    public class DeUI : MonoBehaviour
    {
        public ServiceLancerDeDeUI MyService;

        public void LancerDeResult(int[] result)
        {
            MyService.LancerDesCaracteristiquesUIResult(result);
        }
    }
}