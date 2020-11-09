using UnityEngine;

namespace TowerDefense.Utils
{
    public class LimitedPool<T> : Pool<T> where T : MonoBehaviour
    {
        [SerializeField] private int maxPool = 10;

        protected override T DoGetOne()
        {
            var instance = GetFirstInactiveInstance();

            if (instance == null)
            {
                if (GetPoolSize() >= maxPool)
                {
                    return null;
                }
                instance = GenerateNewInstance();
            }
            instance.gameObject.SetActive(true);

            return instance;
        }
    }
}