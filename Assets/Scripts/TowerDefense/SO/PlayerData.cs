using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "SO/Entity/PlayerData", order = 6)]
    public class PlayerData : ScriptableObject
    {
        public float hp = 100f;
        public float movementSpeed = 1f;
        public float shotCooldown = 1f;
        public ShotData shotData = null;
    }
}