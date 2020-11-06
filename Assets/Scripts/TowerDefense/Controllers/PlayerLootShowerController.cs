using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class PlayerLootShowerController : LootShowerController
    {
        [SerializeField] private LootManager lootManager = null;

        protected override void Start()
        {
            base.Start();
            UpdateLoot();
        }

        private void OnEnable()
        {
            lootManager.OnLootChange += UpdateLoot;
        }

        private void OnDisable()
        {
            lootManager.OnLootChange -= UpdateLoot;
        }

        private void UpdateLoot()
        {
            UpdateText(lootManager.GetCurrentLoot());
        }
    }
}