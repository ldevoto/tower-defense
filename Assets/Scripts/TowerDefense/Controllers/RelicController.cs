using System;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class RelicController : MonoBehaviour
    {
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private WatcherController watcherController = null;
        public Action OnRelicTouched = null;
        public Action OnForceFieldBroken = null;

        private void Start()
        {
            aliveEntityController.SetHP(1000f);
            aliveEntityController.OnKill += ForceFieldBroken;
            watcherController.OnAliveEntityEnter += OnAliveEntityEnter;
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