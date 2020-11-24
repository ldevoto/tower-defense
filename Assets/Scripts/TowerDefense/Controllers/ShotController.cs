using System.Collections.Generic;
using System.Linq;
using TowerDefense.SO;
using TowerDefense.SO.Behaviour;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class ShotController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D shotRigidbody = null;

        private float _damage = 0f;
        private int _collisions = 0;
        private float _impulse = 5f;
        private string[] _targets = null;
        private string[] _blockers = null;
        private bool _hasImpactOnTarget = false;
        private ShotBehaviour _shotBehaviour = null;
        private readonly List<AliveEntityController> _damagedEntities = new List<AliveEntityController>();

        public void ShotWith(ShotData shotData, Transform shotOrigin)
        {
            InitializeWith(shotData);
            Place(shotOrigin);
            Shot();
        }

        private void Place(Transform shotOrigin)
        {
            var transform1 = shotRigidbody.transform;
            transform1.position = shotOrigin.position;
            transform1.rotation = shotOrigin.rotation;
        }

        private void InitializeWith(ShotData shotData)
        {
            _damage = shotData.damage;
            _impulse = shotData.impulse;
            _targets = shotData.targets;
            _blockers = shotData.blockers;
            _shotBehaviour = shotData.shotBehaviour;
            transform.localScale = shotData.size;
            _collisions = 0;
            _hasImpactOnTarget = false;
            _damagedEntities.Clear();
            shotRigidbody.velocity = Vector2.zero;
            shotRigidbody.angularVelocity = 0;
        }

        private void Shot()
        {
            shotRigidbody.AddForce(transform.TransformDirection(Vector2.right) * _impulse, ForceMode2D.Impulse);
        }

        public float GetDamage()
        {
            return _damage;
        }
        
        public void AddCollision()
        {
            _collisions++;
        }

        public void AddCollision(AliveEntityController aliveEntityController)
        {
            _damagedEntities.Add(aliveEntityController);
        }

        public int GetCollisions()
        {
            return _collisions;
        }

        public int GetDamagedEntitiesCount()
        {
            return _damagedEntities.Count;
        }

        public bool AlreadyDamage(AliveEntityController aliveEntityController)
        {
            return _damagedEntities.Contains(aliveEntityController);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_hasImpactOnTarget) return;
            
            if (IsBlocker(other))
            {
                Destroy();
                return;
            }

            if (!IsTarget(other)) return;
            
            _hasImpactOnTarget = true;
            _shotBehaviour.HandleCollision(this,other.gameObject.GetComponent<AliveEntityController>());
        }

        public bool IsTarget(Collider2D other)
        {
            return _targets.Any(t => other.gameObject.CompareTag(t));
        }
        
        private bool IsBlocker(Collider2D other)
        {
            return _blockers.Any(t => other.gameObject.CompareTag(t));
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}