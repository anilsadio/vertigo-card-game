using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities.Pool
{
    public class ObjectPool
    {
        private readonly Stack<IPoolable> objects = new ();
        private readonly HashSet<IPoolable> activeObjects = new ();
        private readonly IPoolable poolObject;

        private int Count => objects.Count;
        private Transform PoolParent { get; set; }

        public ObjectPool(IPoolable poolObject, int numToSpawn = 0)
        {
            this.poolObject = poolObject;
            
            if (numToSpawn > 0)
            {
                Spawn(numToSpawn);
            }
        }

        private void Spawn(int number)
        {
            for (int i = 0; i < number; i++)
            {
                IPoolable poolableObj = CreateObject();
                objects.Push(poolableObj);
                poolableObj.GameObject.SetActive(false);
            }
        }

        public T GetReferenceObject<T>() where T : IPoolable
        {
            return (T)poolObject;
        }

        public IPoolable GetReferenceObject()
        {
            return poolObject;
        }

        private IPoolable CreateObject()
        {
            SetPoolParent();
            IPoolable poolableObj = (IPoolable)Object.Instantiate((Object)poolObject, PoolParent);
            return poolableObj;
            
            void SetPoolParent()
            {
                if (!PoolParent)
                {
                    Transform parent = new GameObject(poolObject.Type.ToString()).transform;
                    parent.SetParent(PoolFactory.Instance.transform);
                    PoolParent = parent;
                }
            }
        }
        
        public IPoolable Pull(Transform parent = null, Vector3? position = null, Vector3? scale = null, Vector3? rotation = null)
        {
            IPoolable poolableObj = Count > 0 ? objects.Pop() : CreateObject();
            poolableObj.IsInThePool = false;
            poolableObj.GameObject.SetActive(true);
            if (parent)
            {
                poolableObj.GameObject.transform.SetParent(parent);
            }
            poolableObj.GameObject.transform.localScale = scale ?? Vector3.one;
            poolableObj.GameObject.transform.position = position ?? Vector3.zero;
            poolableObj.GameObject.transform.localRotation =
                rotation.HasValue ? Quaternion.Euler(rotation.Value) : Quaternion.identity;
            poolableObj.OnSpawn();
            
            if (activeObjects.Add(poolableObj) == false)
            {
                Debug.LogError(("BUG: Trying to add the same object ->", poolableObj.Type));
            }
            return poolableObj;
        }

        public void Push(IPoolable obj,bool removeFromActiveObjects=true)
        {
            if (removeFromActiveObjects)
            {
                activeObjects.Remove(obj);
            }

            if (obj.IsInThePool)
            {
                return;
            }
            else if (objects.Contains(obj))
            {
                Debug.LogError(("BUG: Trying to add the same object ->", obj.Type));
                return;
            }
            objects.Push(obj);
            
            obj.OnReset();
            obj.GameObject.SetActive(false);
            obj.GameObject.transform.SetParent(PoolParent);
            obj.GameObject.transform.localPosition = Vector3.zero;
            obj.GameObject.transform.eulerAngles = Vector3.zero;
            obj.IsInThePool = true;
        }

        public void Reset()
        {
            foreach (IPoolable item in activeObjects)
            {
                item.ResetObject(false);
            }
            activeObjects.Clear();
        }
    }
}