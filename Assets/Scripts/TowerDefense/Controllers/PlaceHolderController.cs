using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class PlaceHolderController : MonoBehaviour
    {
        [SerializeField] private string[] blockerTags = null;
        [SerializeField] private PlaceableController[] placeablePrefabs = null;
        [SerializeField] private GameObject[] models = null;
        
        private readonly List<GameObject> _collisions = new List<GameObject>();
        private int _currentPlaceable = 0;

        private void Awake()
        {
            foreach (var model in models)
            {
                model.SetActive(false);
            }

            SetCurrentPlaceable(0);
        }

        public void Next()
        {
            var newPlaceable = _currentPlaceable + 1;
            if (newPlaceable >= placeablePrefabs.Length)
            {
                newPlaceable = 0;
            }
            SetCurrentPlaceable(newPlaceable);
        }

        public void Previous()
        {
            var newPlaceable = _currentPlaceable - 1;
            if (newPlaceable < 0)
            {
                newPlaceable = placeablePrefabs.Length - 1;
            }
            SetCurrentPlaceable(newPlaceable);
        }
        
        private void SetCurrentPlaceable(int i)
        {
            models[_currentPlaceable].SetActive(false);
            models[i].SetActive(true);
            _currentPlaceable = i;
        }

        public bool Place()
        {
            if (_collisions.Count != 0) return false;

            var placeableInstance = Instantiate(placeablePrefabs[_currentPlaceable], transform.position, Quaternion.identity);
            placeableInstance.Place(transform);
            return true;

        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (blockerTags.Any(blockerTag => other.gameObject.CompareTag(blockerTag)))
            {
                _collisions.Add(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (blockerTags.Any(blockerTag => other.gameObject.CompareTag(blockerTag)))
            {
                _collisions.Remove(other.gameObject);
            }
        }
    }
}