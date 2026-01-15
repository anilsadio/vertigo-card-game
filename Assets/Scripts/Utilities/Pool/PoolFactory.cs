using System;
using System.Collections.Generic;
using Game.Scripts.Utils;
using UnityEngine;

namespace Utilities.Pool
{
    public class PoolFactory : SingletonBehaviour<PoolFactory>
    {
        private Dictionary<ObjectType, ObjectPool> pools;
        
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private List<PoolPreset> presets;
        
#if UNITY_EDITOR
        [SerializeField, NaughtyAttributes.ReadOnly] private List<ObjectPoolOnInspector> poolsOnInspector;
#endif
        
        protected override void Awake()
        {
            base.Awake();

            if (Instance == this)
            {
                Initialize();
            }
        }
        
        private void Initialize()
        {
            if (pools == null)
            {
                pools = new Dictionary<ObjectType, ObjectPool>(new ObjectTypeComparer());
#if UNITY_EDITOR
                poolsOnInspector = new List<ObjectPoolOnInspector>();
#endif

                foreach (GameObject obj in prefabs)
                {
                    CreateObjectPool(obj);
                }
            }
        }
        
        private void CreateObjectPool(GameObject obj)
        {
            IPoolable poolableObj = TryToGetIPoolable(obj);
            if (poolableObj != null && !pools.ContainsKey(poolableObj.Type))
            {
                PoolPreset preset = presets.Find(x =>
                {
                    IPoolable targetIPoolable = TryToGetIPoolable(x.obj);
                    return targetIPoolable != null && targetIPoolable.Type == poolableObj.Type;
                });
                ObjectPool objectPool = new ObjectPool(poolableObj, preset?.initialSize ?? 0);
                pools.Add(poolableObj.Type, objectPool);
                
#if UNITY_EDITOR
                poolsOnInspector.Add(new ObjectPoolOnInspector(poolableObj.Type, objectPool));
#endif
            }
            
            IPoolable TryToGetIPoolable(GameObject targetObj)
            {
                foreach (MonoBehaviour monoBehaviour in targetObj.GetComponents<MonoBehaviour>())
                {
                    if (monoBehaviour is IPoolable iPoolable)
                    {
                        return iPoolable;
                    }
                }
                Debug.LogError((targetObj.name, " is not an IPoolable object."));
                return null;
            }
        }
        
        #region Get Methods

        private ObjectPool GetPool(ObjectType type)
        {
            return pools.GetValueOrDefault(type);
        }
        
        public T GetReferenceObject<T>(ObjectType type) where T : IPoolable
        {
            return GetPool(type).GetReferenceObject<T>();
        }
        
        public IPoolable GetReferenceObject(ObjectType type)
        {
            return GetPool(type).GetReferenceObject();
        }
        
        public T GetObject<T>(ObjectType type, Transform parent = null, Vector3? position = null, Vector3? scale = null)
            where T : IPoolable
        {
            return (T)GetObject(type, parent, position, scale);
        }
        
        public IPoolable GetObject(ObjectType type, Transform parent = null, Vector3? position = null, Vector3? scale = null)
        {
            ObjectPool pool = GetPool(type);
            return pool?.Pull(parent, position, scale);
        }

        public ParticleSystem GetParticleEffect(ObjectType type, Vector2 position, Transform parent = null)
        {
            ParticleSystem particle = GetObject(type, parent, position, Vector3.one).GameObject.GetComponent<ParticleSystem>();
            particle.Play(true);

            var main = particle.main;
            main.stopAction = ParticleSystemStopAction.Callback;

            return particle;
        }
        
        // public UIParticleSystem GetUIParticleEffect(ObjectType type, Vector2 position, Transform parent = null)
        // {
        //     UIParticleSystem uiParticleSystem = GetObject(type, parent, position, Vector3.one).GameObject.GetComponent<UIParticleSystem>();
        //     uiParticleSystem.StartParticleEmission();
        //     return uiParticleSystem;
        // }
        
        #endregion

        #region Reset Methods
        public void ResetPool(ObjectType objectType)
        {
            GetPool(objectType).Reset();
        }
        
        public bool ResetObject(IPoolable obj, bool removeFromActiveObjects=true)
        {
            ObjectPool pool = GetPool(obj.Type);
            if (pool != null)
            {
                pool.Push(obj,removeFromActiveObjects);
                return true;
            }
            Debug.LogError(("The object you are trying to reset doesn't exist in any pool ->", obj.Type));
            return false;
        }

        public void ResetAll(params ObjectType[] excludedTypes)
        {
            foreach (KeyValuePair<ObjectType, ObjectPool> pool in pools)
            {
                if (excludedTypes?.Length > 0 && Array.IndexOf(excludedTypes, pool.Key) > -1)
                {
                    continue;
                }
                pool.Value.Reset();
            }
        }
        
        #endregion
    }

    [Serializable]
    public class PoolPreset
    {
        public GameObject obj;
        public int initialSize;
    }
}