using System;
using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "LootManager", menuName = "SO/LootManger", order = 5)]
    public class LootManager : ScriptableObject
    {
        private int _currentLoot = 0;
        public Action OnLootChange = null;

        public void Awake()
        {
            Restart();
        }

        public void Restart()
        {
            SetCurrentLoot(0);
        }

        public int GetCurrentLoot()
        {
            return _currentLoot;
        }

        public void SetCurrentLoot(int loot)
        {
            _currentLoot = loot;
            OnLootChange?.Invoke();
        }

        public void AddLoot(int loot)
        {
            _currentLoot += loot;
            OnLootChange?.Invoke();
        }

        public void RemoveLoot(int loot)
        {
            _currentLoot -= loot;
            OnLootChange?.Invoke();
        }
    }
}