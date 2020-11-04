using System;
using System.Collections;
using Pathfinding;
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
        public Action OnKill = null;
        
        /// <summary>The object that the AI should move to</summary>
        public Transform target;
        private Transform temporalTarget;
        IAstarAI ai;

        void OnEnable () {
            ai = GetComponentInChildren<IAstarAI>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.
            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable () {
            if (ai != null) ai.onSearchPath -= Update;
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update () {
            if (temporalTarget)
            {
                if (target && ai != null) ai.destination = temporalTarget.position;
            }
            else
            {
                if (target && ai != null) ai.destination = target.position;
            }
        }
        
        

        private void Start()
        {
            aliveEntityController.SetHP(200f);
            aliveEntityController.OnKill += Kill;
            watcherController.OnAliveEntityEnter += OnTargetEnter;
            watcherController.OnAliveEntityLeave += OnTargetLeave;
        }
        
        private void OnTargetEnter(AliveEntityController aliveEntity)
        {
            temporalTarget = aliveEntity.transform;
            //StartCoroutine(FollowEnemy(aliveEntity));
            StartCoroutine(DamageEntity(aliveEntity));
        }
        
        private void OnTargetLeave(AliveEntityController aliveEntity)
        {
            temporalTarget = null;
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
            OnKill?.Invoke();
            Destroy(gameObject);
        }
    }
}