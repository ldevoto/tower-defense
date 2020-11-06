using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO.Behaviour
{
    [CreateAssetMenu(fileName = "StandardShotBehaviour", menuName = "SO/Behaviours/StandardShotBehaviour", order = 0)]
    public class StandardShotBehaviour : ShotBehaviour
    {
        protected override void DoHandleCollision(ShotController shooter, AliveEntityController aliveEntity)
        {
            shooter.Destroy();
        }
    }
}