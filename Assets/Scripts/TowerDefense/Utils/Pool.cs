using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Utils
{
    public abstract class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab = default;
        [SerializeField] private float rateLimit = 0f;

        private float _lastRequestTime = -999999f;
        private readonly List<T> _instances = new List<T>();

        public T GetOne()
        {
            if (Time.time - _lastRequestTime < rateLimit) return null;
            
            _lastRequestTime = Time.time;
            return DoGetOne();
        }

        protected abstract T DoGetOne();

        protected T GenerateNewInstance()
        {
            var instance = Instantiate(prefab);
            _instances.Add(instance);
            return instance;
        }

        protected T GetFirstInactiveInstance()
        {
            return _instances.FirstOrDefault(instance => !instance.gameObject.activeInHierarchy);
        }

        protected int GetPoolSize()
        {
            return _instances.Count;
        }
    }
}