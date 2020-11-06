using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class TowerController : WallController
    {
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private Rigidbody2D towerRigidbody = null;
        [SerializeField] private float shotCooldown = 1f;

        protected override void Start()
        {
            base.Start();
            Debug.Log("Start from Tower");
            aliveEntityController.SetHP(1000f);
            watcherController.OnAliveEntityEnter += OnTargetEnter;
            watcherController.OnAliveEntityLeave += OnTargetLeave;
        }

        private void OnTargetLeave(AliveEntityController obj)
        {
            StopAllCoroutines();
        }

        private void OnTargetEnter(AliveEntityController aliveEntity)
        {
            StartCoroutine(FollowEnemy(aliveEntity));
            StartCoroutine(ShotEnemy(aliveEntity));
        }

        private IEnumerator ShotEnemy(AliveEntityController aliveEntity)
        {
            while (true)
            {
                if (!aliveEntity) break;
                
                shooterController.Shot();
                yield return new WaitForSeconds(shotCooldown);
            }
        }

        private IEnumerator FollowEnemy(AliveEntityController aliveEntity)
        {
            while (true)
            {
                if (!aliveEntity) break;
                
                var direction = aliveEntity.transform.position - towerRigidbody.transform.position;
                direction.z = 0;
                var rotation = Vector2.SignedAngle(Vector2.right, direction);
                towerRigidbody.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
                yield return null;
            }
        }
    }
}