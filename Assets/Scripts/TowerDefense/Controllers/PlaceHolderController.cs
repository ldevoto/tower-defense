using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Controllers
{
    [RequireComponent(typeof(Collider2D))]
    public class PlaceHolderController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer ringRenderer = null;
        [SerializeField] private string[] blockerTags = null;
        [SerializeField] private ModelController[] models = null;
        [SerializeField] private Color allowedColor = Color.white;
        [SerializeField] private Color notAllowedColor = Color.white;

        private readonly List<GameObject> _collisions = new List<GameObject>();
        private int _currentPlaceable = 0;

        private void Awake()
        {
            foreach (var model in models)
            {
                model.gameObject.SetActive(false);
            }
            SetCurrentPlaceable(0);
            ringRenderer.color = allowedColor;
        }

        public void Next()
        {
            var newPlaceable = _currentPlaceable + 1;
            if (newPlaceable >= models.Length)
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
                newPlaceable = models.Length - 1;
            }
            SetCurrentPlaceable(newPlaceable);
        }

        private void SetCurrentPlaceable(int i)
        {
            models[_currentPlaceable].gameObject.SetActive(false);
            models[i].gameObject.SetActive(true);
            _currentPlaceable = i;
        }

        public bool Place(out WallController placedObject)
        {
            placedObject = null;
            if (_collisions.Count != 0) return false;
            
            placedObject = Instantiate(models[_currentPlaceable].GetPrefab(), transform.position, Quaternion.identity);
            placedObject.Place(transform);
            return true;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (blockerTags.Any(blockerTag => other.gameObject.CompareTag(blockerTag)))
            {
                _collisions.Add(other.gameObject);
                ringRenderer.color = notAllowedColor;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (blockerTags.Any(blockerTag => other.gameObject.CompareTag(blockerTag)))
            {
                _collisions.Remove(other.gameObject);
                if (_collisions.Count == 0)
                {
                    ringRenderer.color = allowedColor;
                }
            }
        }
    }
}