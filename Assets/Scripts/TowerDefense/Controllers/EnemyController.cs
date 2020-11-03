using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private DamageDealerController damageDealerController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D enemyRigidbody = null;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float damageCooldown = 0.5f;

        private void Start()
        {
            aliveEntityController.SetHP(200f);
            aliveEntityController.OnKill += Kill;
            watcherController.OnAliveEntityEnter += OnTargetEnter;
            watcherController.OnAliveEntityLeave += OnTargetLeave;
        }
        
        private void OnTargetEnter(AliveEntityController aliveEntity)
        {
            StartCoroutine(FollowEnemy(aliveEntity));
            StartCoroutine(DamageEntity(aliveEntity));
        }
        
        private void OnTargetLeave(AliveEntityController aliveEntity)
        {
            StopAllCoroutines();
        }

        private IEnumerator DamageEntity(AliveEntityController aliveEntity)
        {
            while (true)
            {
                if (!aliveEntity) break;
                
                aliveEntity.Damage(10f);
                yield return new WaitForSeconds(damageCooldown);
            }
        }

        private IEnumerator FollowEnemy(AliveEntityController aliveEntity)
        {
            while (true)
            {
                if (!aliveEntity) break;
                
                var direction = aliveEntity.transform.position - enemyRigidbody.transform.position;
                direction.z = 0;
                var rotation = Vector2.SignedAngle(Vector2.right, direction);
                enemyRigidbody.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
                yield return null;
            }
        }

        private void Kill()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            enemyRigidbody.MovePosition(enemyRigidbody.position + Vector2.right * (Time.fixedDeltaTime * speed));
        }
    }
}