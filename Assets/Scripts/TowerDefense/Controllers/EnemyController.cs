using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private Rigidbody2D enemyRigidbody = null;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float damageCooldown = 0.5f;
        public Action OnKill = null;
        
        private Transform _target;
        private IAstarAI _ai;

        private void Awake()
        {
            _ai = GetComponentInChildren<IAstarAI>();
        }
        
        private void Start()
        {
            aliveEntityController.SetHP(200f);
            Follow(_target);
        }

        private void OnEnable () {
            watcherController.OnAliveEntityEnter += OnTargetEnter;
            watcherController.OnAliveEntityLeave += OnTargetLeave;
            aliveEntityController.OnKill += Kill;
        }

        private void OnDisable () {
            watcherController.OnAliveEntityEnter -= OnTargetEnter;
            watcherController.OnAliveEntityLeave -= OnTargetLeave;
            aliveEntityController.OnKill -= Kill;
        }

        private void Follow(Transform currentTarget)
        {
            if (currentTarget)
            {
                _ai.destination = currentTarget.position;
            }
        }

        public void SetTarget(Transform target)
        {
            this._target = target;
        }

        private void OnTargetEnter(AliveEntityController aliveEntity)
        {
            StartCoroutine(DamageEntity(aliveEntity));
            StartCoroutine(FollowTemporalTarget(aliveEntity.transform));
        }
        
        private void OnTargetLeave(AliveEntityController aliveEntity)
        {
            StopAllCoroutines();
            Follow(_target);
        }

        private IEnumerator DamageEntity(AliveEntityController aliveEntity)
        {
            while (true)
            {
                if (!aliveEntity) yield break;
                
                aliveEntity.Damage(10f);
                yield return new WaitForSeconds(damageCooldown);
            }
        }

        private IEnumerator FollowTemporalTarget(Transform temporalTarget)
        {
            while (true)
            {
                if (!temporalTarget) yield break;
                
                Follow(temporalTarget);
                yield return null;
            }
        }

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}