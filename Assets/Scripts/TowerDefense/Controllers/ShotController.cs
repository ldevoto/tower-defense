using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefense.Controllers
{
    public class ShotController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D shotRigidbody = null;
        [SerializeField] private float impulse = 5f;
        [SerializeField] private float damage = 5;
        [SerializeField] private string[] targets = null;
        [SerializeField] private int maxDamagedTargets = 1;

        private int _damagedTargets = 0;
        private void Start()
        {
            shotRigidbody.AddRelativeForce(Vector2.right * impulse, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (targets.Any(t => other.gameObject.CompareTag(t)))
            {
                other.gameObject.GetComponent<AliveEntityController>().Damage(damage);
                _damagedTargets++;
            }

            if (_damagedTargets >= maxDamagedTargets)
            {
                Destroy(gameObject);
            }
        }
    }
}