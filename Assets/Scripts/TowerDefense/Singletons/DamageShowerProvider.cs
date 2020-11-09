using TowerDefense.Controllers;
using TowerDefense.Utils;

namespace TowerDefense.Singletons
{
    public class DamageShowerProvider : InfinitePool<DamageShowerController>
    {
        public static DamageShowerProvider instance;

        private void Awake()
        {
            instance = this;
        }
    }
}