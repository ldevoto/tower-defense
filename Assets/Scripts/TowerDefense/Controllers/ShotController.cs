using System.Collections.Generic;
using System.Linq;
using TowerDefense.SO.Behaviour;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class ShotController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D shotRigidbody = null;
        [SerializeField] private float impulse = 5f;
        [SerializeField] private string[] targets = null;
        [SerializeField] private ShotBehaviour shotBehaviour = null;

        private float _damage = 0f;
        private int _collisions = 0;
        private List<AliveEntityController> _damagedEntities = new List<AliveEntityController>();

        public void ShotWith(float damage)
        {
            _damage = damage;
            Shot();
        }

        public void Shot()
        {
            shotRigidbody.AddRelativeForce(Vector2.right * impulse, ForceMode2D.Impulse);
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
            if (IsTarget(other))
            {
                shotBehaviour.HandleCollision(this,other.gameObject.GetComponent<AliveEntityController>());
            }
        }

        public bool IsTarget(Collider2D other)
        {
            return targets.Any(t => other.gameObject.CompareTag(t));
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}