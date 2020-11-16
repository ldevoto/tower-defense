using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.Controllers
{
    public class RelicController : MonoBehaviour
    {
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private WatcherController watcherController = null;
        [SerializeField] private Transform[] spawnPoints = null;
        public Action OnRelicTouched = null;
        public Action OnForceFieldBroken = null;

        private void Start()
        {
            aliveEntityController.SetHP(10000f);
            aliveEntityController.OnKill += ForceFieldBroken;
            watcherController.OnAliveEntityEnter += OnAliveEntityEnter;
        }

        public Vector2 GetSpawnPoint()
        {
            return spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }

        private void OnAliveEntityEnter(AliveEntityController obj)
        {
            OnRelicTouched?.Invoke();
        }

        private void ForceFieldBroken()
        {
            Destroy(aliveEntityController.gameObject);
            OnForceFieldBroken?.Invoke();
        }
    }
}