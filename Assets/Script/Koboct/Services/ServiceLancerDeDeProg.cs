using System;
using Koboct.Data;
using UnityEngine;
using System.Linq;


namespace Koboct.Services
{
    [CreateAssetMenu(fileName = "ServiceLancerDeDeProg", menuName = "ServiceLancerDeDeProg", order = 0)]
    public class ServiceLancerDeDeProg : ServiceLancerDeDe
    {
        public override void LancerDesCaracteristiques(Action<int[]> result)
        {
            int[] rolls = new int[6];
            for (int i = 0; i < 6; i++)
            {
                var rolls4 = LancerDe(TypeDeDe.D6, 4);

                rolls[i] = rolls4.OrderByDescending(r => r).Take(3).Sum();
            }

            result.Invoke(rolls);
        }

        public override void LancerDesTestCaractéristiques(Action<int> result)
        {
            result.Invoke( LancerDe(TypeDeDe.D20,1)[0]); 
        }
#if UNITY_EDITOR
        public TypeDeDe MonTestTypesDeDe= TypeDeDe.D6;
        public int NbLancer = 1000;
        [ContextMenu("Test Lancer")]
        private void LancerTest()
        {
            for (int i = 0; i < NbLancer; i++)
            {
                Debug.Log(LancerDe(MonTestTypesDeDe)[0]);
            }
        }
#endif
        private int[] LancerDe(TypeDeDe monTypeDeDe = TypeDeDe.D6, int nbDe = 1)
        {
            int[] results = new int[nbDe];
            System.Random random = new System.Random();

            for (int i = 0; i < nbDe; i++)
            {
                results[i] = random.Next(1, (int)monTypeDeDe + 1);
            }


            return results;
        }
    }
}
