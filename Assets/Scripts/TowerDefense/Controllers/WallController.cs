using System;
using System.Collections.Generic;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class WallController : MonoBehaviour
    {
        [SerializeField] private TowerData[] levelsData = null;
        [SerializeField] protected AliveEntityController aliveEntityController = null;
        [SerializeField] private List<GameObject> objectsToRotate = null;
        public Action OnKill = null;
        
        protected virtual void Start()
        {
            Debug.Log("Start from Wall");
            aliveEntityController.SetHP(2000f);
            aliveEntityController.OnKill += Kill;
        }
        
        public void Place(Transform placeTransform)
        {
            foreach (var placeableObject in objectsToRotate)
            {
                placeableObject.transform.rotation = placeTransform.rotation;
            }
        }

        public int GetPlacingCost()
        {
            return GetLevelCost(1);
        }
        
        private int GetLevelCost(int level)
        {
            if (level > levelsData.Length)
            {
                throw new Exception("level not configured");
            }

            return levelsData[level - 1].cost;
        }

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}