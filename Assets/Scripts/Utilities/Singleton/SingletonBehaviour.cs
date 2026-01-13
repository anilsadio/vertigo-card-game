using System;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DontSearchOnResourcesAttribute : Attribute { }
    public abstract class SingletonBehaviour<T> : SerializedMonoBehaviour, ISingleton where T : SerializedMonoBehaviour
    {
        [ShowInInspector]
        [ReadOnly]
        [PropertySpace(0, 12)]
        [NonSerialized]
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null && typeof(T).GetCustomAttribute(typeof(DontSearchOnResourcesAttribute), false) == null)
                {
                    _instance = SceneManager.GetActiveScene().GetRootGameObjects().Select(go => go.GetComponentInChildren<T>(true)).FirstOrDefault(t => t != null);
                }
                return _instance;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_instance != this)
                return;
            if (!gameObject.scene.IsValid())
            {
                return;
            }
            if (UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long id) && id > 0)
            {
                // Debug.Log("Instance is an Asset", this);
                _instance = null;
                return;
            }
        }
#endif

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long id) && id > 0)
            {
                return;
            }
#endif
            if (!gameObject.scene.IsValid())
            {
                return;
            }

            if (_instance != null && _instance != this)
            {
#if UNITY_EDITOR
                Debug.LogError(typeof(T).Name + ": There is Another Instance :" + _instance.name + ":" + _instance.gameObject.scene.name + ":" + _instance.gameObject.scene.isLoaded + ":" + _instance.gameObject.scene.IsValid(), _instance);
#endif
            }
            else
            {
                _instance = this as T;
            }
        }
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}

