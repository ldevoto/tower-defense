using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class TowerController : WallController
    {
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private ShooterController shooterController = null;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Shot = Animator.StringToHash("Shot");
        private static readonly int Waiting = Animator.StringToHash("Waiting");

        protected override void Start()
        {
            base.Start();
            watcherController.OnAliveEntityEnter += OnTargetEnter;
            watcherController.OnAliveEntityLeave += OnTargetLeave;
        }

        protected override void UpgradeToLevel(int currentLevel)
        {
            base.UpgradeToLevel(currentLevel);
            shooterController.SetShotData(CurrentLevelData.shotData);
            shooterController.SetCooldown(CurrentLevelData.shotCooldown);
        }

        private void OnTargetLeave(AliveEntityController obj)
        {
            StopAllCoroutines();
            Animator.SetTrigger(Idle);
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
                
                yield return new WaitForSeconds(shooterController.GetWaitToNextShot());
                Animator.SetTrigger(Shot);
                shooterController.ImmediateShot();
                yield return new WaitForSeconds(CurrentLevelData.shotCooldown);
            }
        }

        private IEnumerator FollowEnemy(AliveEntityController aliveEntity)
        {
            Animator.SetTrigger(Waiting);
            while (true)
            {
                if (!aliveEntity) break;
                
                var direction = aliveEntity.transform.position - objectRigidbody.transform.position;
                direction.z = 0;
                var rotation = Vector2.SignedAngle(Vector2.right, direction);
                objectRigidbody.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
                yield return null;
            }
        }
    }
}