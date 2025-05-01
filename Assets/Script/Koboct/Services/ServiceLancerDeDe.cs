using System;
using UnityEngine;

namespace Koboct.Services
{
    
    public abstract class ServiceLancerDeDe:ScriptableObject
    {
        public abstract void LancerDesCaracteristiques(Action<int[]> result);
        public abstract void LancerDesTestCaractéristiques(Action<int> result);
    }
}