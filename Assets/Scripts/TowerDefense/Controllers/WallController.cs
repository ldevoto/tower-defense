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
        
        public void Place(Transform placeTransform)
        {
            foreach (var placeableObject in objectsToRotate)
            {
                placeableObject.transform.rotation = placeTransform.rotation;
            }
        }

        protected virtual void UpgradeToLevel(int level)
        {
            if (Animator)
            {
                Destroy(Animator.gameObject);
            }
            CurrentLevel = level;
            CurrentLevelData = GetCurrentLevelData();
            Animator = Instantiate(CurrentLevelData.animator, objectRigidbody.gameObject.transform);
            aliveEntityController.SetHP(CurrentLevelData.hp);
        }

        public void ShowUpgrade()
        {
            upgradeShower.ShowCost(GetUpgradeCost());
        }
        
        public bool TryBuyUpgrade(LootManager lootManager)
        {
            if (!HasUpgrade()) return false;
            if (lootManager.GetCurrentLoot() < GetUpgradeCost()) return false;
            
            BuyUpgrade(lootManager);
            return true;
        }

        public void HideUpgrade()
        {
            upgradeShower.Hide();
        }

        public void BuyUpgrade(LootManager lootManager)
        {
            lootManager.RemoveLoot(GetUpgradeCost());
            upgradeShower.Buy();
            UpgradeToLevel(CurrentLevel + 1);
        }
        
        public int GetUpgradeCost()
        {
            return GetUpgradeLevelData().cost;
        }

        private TowerData GetCurrentLevelData()
        {
            return GetLevelData(CurrentLevel);
        }

        private TowerData GetUpgradeLevelData()
        {
            return GetLevelData(CurrentLevel + 1);
        }

        private TowerData GetLevelData(int level)
        {
            return HasLevel(level) ? levelsData[level - 1] : null;
        }
        
        public bool HasUpgrade()
        {
            return HasLevel(CurrentLevel + 1);
        }

        private bool HasLevel(int level)
        {
            return level <= levelsData.Length;
        }

        private void Kill()
        {
            Destroy(gameObject);
            OnKill?.Invoke();
        }
    }
}