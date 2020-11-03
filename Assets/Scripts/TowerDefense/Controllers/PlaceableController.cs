using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class PlaceableController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> placeableObjects = null;
        public void Place(Transform placeTransform)
        {
            foreach (var placeableObject in placeableObjects)
            {
                placeableObject.transform.rotation = placeTransform.rotation;
            }
        }
    }
}