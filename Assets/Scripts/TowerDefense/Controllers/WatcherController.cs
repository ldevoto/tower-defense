using System;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Controllers
{
    [RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
    public class WatcherController : MonoBehaviour
    {
        [SerializeField] private string[] targets = null;
        public Action<GameObject> OnTargetEnter = null;
        public Action<GameObject> OnTargetLeave = null;

        private SpriteRenderer _spriteRenderer = null;

        private Color _hideColor = Color.white;
        private Color _showColor = Color.white;
        private GameObject _target = null;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _showColor = _spriteRenderer.color;
            _hideColor = new Color(1f, 1f, 1f, 0f);
            HideGizmo();
        }

        public GameObject GetTarget()
        {
            return _target;
        }

        public void SetSize(float size)
        {
            transform.localScale = new Vector3(size, size, 0f);
        }

        public void ShowGizmo()
        {
            _spriteRenderer.color = _showColor;
        }

        public void HideGizmo()
        {
            _spriteRenderer.color = _hideColor;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_target) return;
            if (!targets.Any(t => other.gameObject.CompareTag(t))) return;
            
            _target = other.gameObject;
            OnTargetEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_target != other.gameObject) return;

            _target = null;
            OnTargetLeave?.Invoke(other.gameObject);
        }
    }
}