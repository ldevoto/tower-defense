using TowerDefense.Controllers;
using TowerDefense.Utils;

namespace TowerDefense.Singletons
{
    public class LootProvider : InfinitePool<LootController>
    {
        public static LootProvider instance;

        private void Awake()
        {
            instance = this;
        }
    }
}