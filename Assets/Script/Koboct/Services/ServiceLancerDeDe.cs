using System;
using Koboct.Data;
using UnityEngine;
using System.Linq;


namespace Koboct.Services
{
    [CreateAssetMenu(fileName = "ServiceLancerDeDe", menuName = "ServiceLancerDeDe", order = 0)]
    public class ServiceLancerDeDe : ScriptableObject
    {
        public void LancerDesCharacteristiques(Action<int[]> result)
        {
            int[] rolls = new int[6];
            for (int i = 0; i < 6; i++)
            {
                var rolls4 = LancerDe(TypeDeDe.D6, 4);

                rolls[i] = rolls4.OrderByDescending(r => r).Take(3).Sum();
            }

            result.Invoke(rolls);
        }

        [ContextMenu("Test Lancer")]
        private void LancerTest()
        {
            for (int i = 0; i < 1000; i++)
            {
                Debug.Log(LancerDe(TypeDeDe.D20)[0]);
            }
        }

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