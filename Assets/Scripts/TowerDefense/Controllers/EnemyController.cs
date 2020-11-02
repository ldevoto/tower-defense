using System;
using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D enemyRigidbody = null;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float shotCooldown = 0.5f;

        private void Start()
        {
            aliveEntityController.SetHP(200f);
            aliveEntityController.OnKill += Kill;
            watcherController.ShowGizmo();
            watcherController.OnTargetEnter += OnTargetEnter;
            watcherController.OnTargetLeave += OnTargetLeave;
        }
        
        private void OnTargetEnter(GameObject target)
        {
            StartCoroutine(FollowEnemy(target));
            StartCoroutine(ShotEnemy(target));
        }
        
        private void OnTargetLeave(GameObject obj)
        {
            StopAllCoroutines();
        }

        private IEnumerator ShotEnemy(GameObject target)
        {
            while (true)
            {
                if (!target) break;
                
                shooterController.Shot();
                yield return new WaitForSeconds(shotCooldown);
            }
        }

        private IEnumerator FollowEnemy(GameObject target)
        {
            while (true)
            {
                if (!target) break;
                
                var direction = target.transform.position - enemyRigidbody.transform.position;
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