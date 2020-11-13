using System;
using System.Collections;
using Pathfinding;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private EnemyData enemyData = null;
        
        public Action OnKill = null;
        
        private Transform _defaultTarget;
        private IAstarAI _ai;
        private Coroutine _damagingCoroutine;
        private Coroutine _followingCoroutine;
        private Coroutine _checkingProximityCoroutine;

        private void Awake()
        {
            _ai = GetComponentInChildren<IAstarAI>();
        }
        
        private void Start()
        {
            _ai.maxSpeed = enemyData.speed;
            aliveEntityController.SetHP(enemyData.hp);
            Follow(_defaultTarget);
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

        public void SetTarget(Transform target)
        {
            _defaultTarget = target;
            FollowDefaultTarget();
        }
        
        private void FollowDefaultTarget()
        {
            Follow(_defaultTarget);
        }

        private void Follow(Transform currentTarget)
        {
            if (currentTarget)
            {
                _ai.destination = currentTarget.position;
            }
        }

        private void OnTargetEnter(AliveEntityController aliveEntity)
        {
            enemyData.enemyBehaviour.HandleFarEnter(this, aliveEntity);
        }
        
        private void OnTargetLeave(AliveEntityController aliveEntity)
        {
            enemyData.enemyBehaviour.HandleFarLeave(this, aliveEntity);
        }

        public void StartDamagingEntity(AliveEntityController aliveEntity)
        {
            _damagingCoroutine = StartCoroutine(DamageEntity(aliveEntity));
        }
        
        public void StartFollowingTarget(AliveEntityController aliveEntity)
        {
            _followingCoroutine = StartCoroutine(FollowTemporalTarget(aliveEntity));
        }

        public void StartCheckingProximity(AliveEntityController aliveEntity)
        {
            _checkingProximityCoroutine = StartCoroutine(CheckProximity(aliveEntity));
        }
        
        public void StopDamagingEntity()
        {
            StopCoroutine(_damagingCoroutine);
        }
        
        public void StopFollowingTarget()
        {
            StopCoroutine(_followingCoroutine);
            FollowDefaultTarget();
        }

        public void StopCheckingProximity()
        {
            StopCoroutine(_checkingProximityCoroutine);
        }
        
        private IEnumerator DamageEntity(AliveEntityController aliveEntity)
        {
            while (true)
            {
                if (!aliveEntity) yield break;
                
                aliveEntity.Damage(enemyData.damage);
                yield return new WaitForSeconds(enemyData.damageCooldown);
            }
        }

        private IEnumerator FollowTemporalTarget(AliveEntityController aliveEntity)
        {
            while (true)
            {
                if (!aliveEntity) yield break;
                
                Follow(aliveEntity.transform);
                yield return null;
            }
        }

        private IEnumerator CheckProximity(AliveEntityController aliveEntity)
        {
            var targetIsNear = false;
            while (true)
            {
                if (!aliveEntity) yield break;

                var isNear = IsNear(aliveEntity);
                if (isNear && !targetIsNear)
                {
                    enemyData.enemyBehaviour.HandleNearEnter(this, aliveEntity);
                    targetIsNear = true;
                }
                else if (!isNear && targetIsNear)
                {
                    enemyData.enemyBehaviour.HandleNearLeave(this, aliveEntity);
                    targetIsNear = false;
                }

                yield return null;
            }
        }

        private bool IsNear(AliveEntityController aliveEntity)
        {
            if (aliveEntity.gameObject.CompareTag("RelicField"))
            {
                return Vector2.Distance(aliveEntity.transform.position, aliveEntityController.transform.position) <= enemyData.damageRelicMinDistance;
            }
            return Vector2.Distance(aliveEntity.transform.position, aliveEntityController.transform.position) <= enemyData.damageMinDistance;
        }
        
        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}