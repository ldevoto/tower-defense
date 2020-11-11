using TowerDefense.SO.Behaviour.Enemy;
using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Entity/EnemyData", order = 0)]
    public class EnemyData : ScriptableObject
    {
        public float hp = 100f;
        public float speed = 1f;
        public float damage = 10f;
        public float damageCooldown = 1f;
        public float damageMinDistance = 1f;
        public float damageRelicMinDistance = 1f;
        public EnemyBehaviour enemyBehaviour = null;
    }
}