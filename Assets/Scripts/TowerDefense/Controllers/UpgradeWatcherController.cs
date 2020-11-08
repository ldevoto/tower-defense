using System;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Controllers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class UpgradeWatcherController : MonoBehaviour
    {
        [SerializeField] private string[] targets = null;
        public Action<WallController> OnTowerEnter = null;
        public Action<WallController> OnTowerLeave = null;
        
        private WallController _wallController = null;

        public void SetSize(float size)
        {
            transform.localScale = new Vector3(size, size, 0f);
        }

        public void ClearWatcher()
        {
            _wallController = null;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_wallController) return;
            
            var wallController = GetWallController(other);
            if (!wallController) return;
            if (!wallController.HasUpgrade()) return;

            _wallController = wallController;
            OnTowerEnter?.Invoke(_wallController);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_wallController) return;
            
            var wallController = GetWallController(other);
            if (!wallController) return;
            if (_wallController != wallController) return;

            _wallController = null;
            OnTowerLeave?.Invoke(wallController);
        }

        private WallController GetWallController(Component other)
        {
            if (!targets.Any(t => other.gameObject.CompareTag(t))) return null;

            var aliveEntity = other.gameObject.GetComponent<AliveEntityController>();
            return aliveEntity ? aliveEntity.GetComponentInParent<WallController>() : null;
        }
    }
}