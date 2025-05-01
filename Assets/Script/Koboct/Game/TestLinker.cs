using Koboct.Services;
using UnityEngine;
using UnityEngine.Events;

namespace Koboct.Game
{
    public class TestLinker : MonoBehaviour
    {
        public TestResolver MonTest;
        
        public UnityEvent ReussiteCritique=new UnityEvent();    
        public UnityEvent EchecCritique=new UnityEvent();   
        public UnityEvent Reussite=new();
        public UnityEvent Echec=new();

        
        
        private void Start()
        {
            MonTest.ReussiteCritique.AddListener(ReussiteCritique.Invoke);
            MonTest.EchecCritique.AddListener(EchecCritique.Invoke);
            MonTest.Reussite.AddListener(Result);
        }

        private void Result(bool arg0)
        {
            if (arg0)
                Reussite.Invoke();
            else
                Echec.Invoke();
        }

        public void Test()
        {
            MonTest.Test();
        }
    }
}