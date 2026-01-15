using UnityEngine;

namespace Utilities.Pool
{
    public class PoolObject : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public ObjectType Type { get; set; }
        public bool IsInThePool { get; set; }

        public virtual void OnSpawn() { }

        public virtual void OnReset() { }

        public void OnParticleSystemStopped()
        {
            if (gameObject.activeInHierarchy)
            {
                this.ResetObject();
            }
        }
    }
}