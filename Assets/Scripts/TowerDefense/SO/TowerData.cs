using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "SO/Entity/TowerData", order = 0)]
    public class TowerData : WallData
    {
        public float shotCooldown = 1f;
        public ShotData shotData = null;
    }
}