using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class WallController : MonoBehaviour
    {
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

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}