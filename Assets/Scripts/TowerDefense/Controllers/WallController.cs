using System;
using System.Collections.Generic;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class WallController : MonoBehaviour
    {
        [SerializeField] private TowerData[] levelsData = null;
        [SerializeField] protected AliveEntityController aliveEntityController = null;
        [SerializeField] private UpgradeShowerController upgradeShower = null;
        [SerializeField] private List<GameObject> objectsToRotate = null;
        [SerializeField] protected Rigidbody2D objectRigidbody = null;
        public Action OnKill = null;
        
        protected int CurrentLevel = 1;
        protected TowerData CurrentLevelData = null;
        protected Animator Animator = null;

        protected virtual void Start()
        {
            UpgradeToLevel(1);
            aliveEntityController.OnKill += Kill;
        }

        protected virtual void UpgradeToLevel(int currentLevel)
        {
            if (Animator)
            {
                Destroy(Animator);
            }
            CurrentLevel = currentLevel;
            CurrentLevelData = GetCurrentLevelData();
            Animator = Instantiate(CurrentLevelData.animator, objectRigidbody.gameObject.transform);
            aliveEntityController.SetHP(CurrentLevelData.hp);
        }

        public int GetUpgradeCost()
        {
            return GetLevelCost(CurrentLevel + 1);
        }

        public bool ShowUpgrade()
        {
            if (CurrentLevel >= levelsData.Length)
            {
                return false;
            }

            upgradeShower.ShowCost(GetLevelCost(CurrentLevel + 1));
            return true;
        }

        public void HideUpgrade()
        {
            upgradeShower.Hide();
        }

        public void BuyUpgrade()
        {
            upgradeShower.Buy();
        }

        public void Place(Transform placeTransform)
        {
            foreach (var placeableObject in objectsToRotate)
            {
                placeableObject.transform.rotation = placeTransform.rotation;
            }
        }

        private int GetLevelCost(int level)
        {
            if (level > levelsData.Length)
            {
                throw new Exception("level not configured");
            }

            return levelsData[level - 1].cost;
        }
        
        private TowerData GetCurrentLevelData()
        {
            return levelsData[CurrentLevel - 1];
        }

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}