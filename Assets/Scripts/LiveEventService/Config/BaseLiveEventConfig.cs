using Gameplay.Core;
using UI.Menu;
using UnityEngine;
using Utilities.Pool;

namespace LiveEventService.Config
{
    public abstract class BaseLiveEventConfig : ScriptableObject
    {
        //public abstract LiveEventType Type { get; }
        [field:SerializeField] public bool IsActive { get; set; }
        [field:SerializeField] public ObjectType MenuButtonType { get; set; }
        public abstract BaseLiveEvent LiveEvent { get; }
        //These variables written for to show how to use event config in a real project
        //[field:SerializeField] public DateTime Period { get; set; }
        //[field:SerializeField] public int MinLevel { get; set; }
        //[field:SerializeField] public string AppVersion { get; set; }
    }
}
