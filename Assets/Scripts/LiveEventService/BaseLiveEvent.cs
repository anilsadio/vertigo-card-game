using Gameplay.Data;
using UnityEngine;

namespace LiveEventService
{
    [System.Serializable]
    public abstract class BaseLiveEvent: ScriptableObject
    {
        [field: SerializeField] public abstract BaseLiveEventData LiveEventData { get; set; }
        
        public abstract void Initialize();
    }
}
