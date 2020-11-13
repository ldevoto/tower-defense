using TowerDefense.Singletons;
using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class LootHolderController : MonoBehaviour
    {
        [SerializeField] private AliveEntityController aliveEntityController = null;
        [SerializeField] private LootData loot = null;

        private void OnEnable()
        {
            aliveEntityController.OnKill += DropLoot;
        }

        private void OnDisable()
        {
            aliveEntityController.OnKill -= DropLoot;
        }

        private void DropLoot()
        {
            var lootController = LootProvider.instance.GetOne();
            lootController.SpawnWith(loot, transform);
        }
    }
}