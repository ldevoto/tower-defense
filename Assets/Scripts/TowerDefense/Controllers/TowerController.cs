using System;
using System.Collections;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private ShooterController shooterController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D towerRigidbody = null;
        [SerializeField] private float shotCooldown = 1f;

        private void Awake()
        {
            watcherController.OnTargetEnter += OnTargetEnter;
            watcherController.OnTargetLeave += OnTargetLeave;
        }

        private void OnTargetLeave(GameObject obj)
        {
            StopAllCoroutines();
        }

        private void OnTargetEnter(GameObject target)
        {
            StartCoroutine(FollowEnemy(target));
            StartCoroutine(ShotEnemy(target));
        }

        private IEnumerator ShotEnemy(GameObject target)
        {
            while (true)
            {
                shooterController.Shot();
                yield return new WaitForSeconds(shotCooldown);
            }
        }

        private IEnumerator FollowEnemy(GameObject target)
        {
            while (true)
            {
                var direction = target.transform.position - towerRigidbody.transform.position;
                direction.z = 0;
                var rotation = Vector2.SignedAngle(Vector2.right, direction);
                //towerRigidbody.MoveRotation(rotation);
                towerRigidbody.transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
                yield return null;
            }
        }

        private void Start()
        {
            watcherController.SetSize(4f);
            watcherController.ShowGizmo();
            aliveEntityController.SetHP(100f);
        }
    }
}