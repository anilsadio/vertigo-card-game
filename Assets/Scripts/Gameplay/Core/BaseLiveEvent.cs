using Gameplay.Data;
using UnityEngine;

namespace Gameplay.Core
{
    [System.Serializable]
    public abstract class BaseLiveEvent: ScriptableObject
    {
        [field: SerializeField] public abstract BaseLiveEventData LiveEventData { get; set; }
        
        public abstract void Initialize();
    }
}
