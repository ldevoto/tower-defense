using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO.Behaviour
{
    [CreateAssetMenu(fileName = "PiercingShotBehaviour", menuName = "SO/Behaviours/PiercingShotBehaviour", order = 1)]
    public class PiercingShotBehaviour : ShotBehaviour
    {
        public int piercingCount = 2;
        protected override void DoHandleCollision(ShotController shooter, AliveEntityController aliveEntity)
        {
            if (shooter.GetDamagedEntitiesCount() >= piercingCount)
            {
                shooter.Destroy();
            }
        }
    }
}