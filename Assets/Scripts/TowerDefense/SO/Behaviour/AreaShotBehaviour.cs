using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO.Behaviour
{
    [CreateAssetMenu(fileName = "AreaShotBehaviour", menuName = "SO/Behaviours/AreaShotBehaviour", order = 2)]
    public class AreaShotBehaviour : ShotBehaviour
    {
        public int maxDamagedEnemies = 3;
        public float areaDamagePercentage = 0.5f;
        public float damageRadius = 1f;
        
        protected override void DoHandleCollision(ShotController shooter, AliveEntityController aliveEntity)
        {
            var damagedEnemies = 0;
            var raycastHit2Ds = Physics2D.CircleCastAll(shooter.transform.position, damageRadius, Vector2.zero);
            foreach (var raycastHit2D in raycastHit2Ds)
            {
                if (!shooter.IsTarget(raycastHit2D.collider)) continue;
                
                var hitEntity = raycastHit2D.collider.gameObject.GetComponent<AliveEntityController>();
                if (shooter.AlreadyDamage(hitEntity)) continue;
                
                hitEntity.Damage(shooter.GetDamage() * areaDamagePercentage);
                damagedEnemies++;
                if (damagedEnemies >= maxDamagedEnemies)
                {
                    break;
                }
            }
            shooter.Destroy();
        }
    }
}