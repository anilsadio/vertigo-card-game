using UnityEngine;
using UnityEngine.UI;
using Utilities.Pool;

namespace UI.Menu
{
    public class BaseMenuButton : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public ObjectType Type { get; set; }
        public bool IsInThePool { get; set; }
        
        public Button button;

        public virtual void Initialize()
        {
            
        }

        public void OnSpawn()
        {
        }

        public void OnReset()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}
