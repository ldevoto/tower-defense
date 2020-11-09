using UnityEngine;

namespace TowerDefense.Utils
{
    public class InfinitePool<T> : Pool<T> where T : MonoBehaviour
    {
        protected override T DoGetOne()
        {
            var instance = GetFirstInactiveInstance();

            if (instance == null)
            {
                instance = GenerateNewInstance();
            }
            instance.gameObject.SetActive(true);
        
            return instance;
        }
    }
}