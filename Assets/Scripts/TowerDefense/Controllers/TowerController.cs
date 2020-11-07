using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class TowerController : WallController
    {
        [SerializeField] private Animator towerAnimator = null;
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private Rigidbody2D towerRigidbody = null;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Shot = Animator.StringToHash("Shot");

        protected override void Start()
        {
            base.Start();
            watcherController.OnAliveEntityEnter += OnTargetEnter;
            watcherController.OnAliveEntityLeave += OnTargetLeave;
        }

        protected override void UpgradeToLevel(int currentLevel)
        {
            base.UpgradeToLevel(currentLevel);
            shooterController.SetShotData(_currentLevelData.shotData);
        }

        private void OnTargetLeave(AliveEntityController obj)
        {
            StopAllCoroutines();
            towerAnimator.SetTrigger(Idle);
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
                
                towerAnimator.SetTrigger(Shot);
                yield return new WaitForSeconds(0.05f);
                shooterController.ImmediateShot();
                yield return new WaitForSeconds(_currentLevelData.shotCooldown);
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