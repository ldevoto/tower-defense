using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO.Behaviour.Enemy
{
    [CreateAssetMenu(fileName = "Follower", menuName = "SO/EnemyBehaviour/Follower", order = 1)]
    public class Follower : EnemyBehaviour
    {
        public override void HandleNearEnter(EnemyController enemyController, AliveEntityController aliveEntityController)
        {
            enemyController.StartDamagingEntity(aliveEntityController);
        }

        public override void HandleFarEnter(EnemyController enemyController, AliveEntityController aliveEntityController)
        {
            enemyController.StartFollowingTarget(aliveEntityController);
            enemyController.StartCheckingProximity(aliveEntityController);
        }
        
        public override void HandleNearLeave(EnemyController enemyController, AliveEntityController aliveEntityController)
        {
            enemyController.StopDamagingEntity();
        }

        public override void HandleFarLeave(EnemyController enemyController, AliveEntityController aliveEntityController)
        {
            enemyController.StopFollowingTarget();
            enemyController.StopAllCoroutines();
        }
    }
}