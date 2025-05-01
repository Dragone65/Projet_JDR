using System;
using UnityEngine;


namespace Koboct.Services
{
    [CreateAssetMenu(fileName = "ServiceLancerDeDeUI", menuName = "ServiceLancerDeDeUI", order = 0)]
    public class ServiceLancerDeDeUI : ServiceLancerDeDe
    {
        private Action<int[]> resultRelay;

        public override void LancerDesCaracteristiques(Action<int[]> result)
        {
            resultRelay = result;
        }

        public override void LancerDesTestCaractéristiques(Action<int> result)
        {
            throw new NotImplementedException();
        }

        public void LancerDesCaracteristiquesUIResult(int[] result)
        {
            resultRelay.Invoke(result);
        }
    }
}