using Koboct.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Koboct.Services
{
    [CreateAssetMenu(fileName = "Test resolver service", menuName = "Test resolver service", order = 0)]
    public class TestResolver : ScriptableObject
    {
        public TypeCaracteristique TypeCaracteristique;
        [Range(5, 25)] public int ValeurDifficulté;
        public Personnage Personnage;
        public bool CanRepeat;
        public bool DoneOnce;
        public ServiceLancerDeDe ServiceLancerDeDe;
        public int result;
        public UnityEvent ReussiteCritique=new UnityEvent();    
        public UnityEvent EchecCritique=new UnityEvent();   
        public UnityEvent<bool> Reussite=new UnityEvent<bool>();   
        
        private void OnEnable()
        {
            DoneOnce = false;
            result = 0;
        }

        [ContextMenu("Test")]
        public void Test()
        {
            if (DoneOnce && !CanRepeat)
            {
                Resultat(result);
            }
            else
                ServiceLancerDeDe.LancerDesTestCaractéristiques(Resultat);
            DoneOnce = true;
        }

        private void Resultat(int obj)
        {
#if UNITY_EDITOR
            Debug.Log(obj);
#endif
            result = obj;
            switch (result)
            {
                case 1:
                    EchecCritique.Invoke();
                    break;
                case 20:
                    ReussiteCritique.Invoke();
                    break;
            }
            Reussite.Invoke(result + Personnage.GetCaracteristiqueModificateur(TypeCaracteristique) >= ValeurDifficulté);
        }
    }
}