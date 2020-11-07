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
        public Action OnKill = null;
        
        protected int _currentLevel = 0;
        protected TowerData _currentLevelData = null;

        protected virtual void Start()
        {
            UpgradeToLevel(1);
            aliveEntityController.OnKill += Kill;
        }

        protected virtual void UpgradeToLevel(int currentLevel)
        {
            _currentLevel = currentLevel;
            _currentLevelData = GetCurrentLevelData();
            aliveEntityController.SetHP(_currentLevelData.hp);
        }

        public int GetUpgradeCost()
        {
            return GetLevelCost(_currentLevel + 1);
        }

        public bool ShowUpgrade()
        {
            if (_currentLevel >= levelsData.Length)
            {
                return false;
            }

            upgradeShower.ShowCost(GetLevelCost(_currentLevel + 1));
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
            return levelsData[_currentLevel - 1];
        }

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}