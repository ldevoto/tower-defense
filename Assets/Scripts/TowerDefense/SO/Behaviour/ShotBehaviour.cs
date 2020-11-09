using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO.Behaviour
{
    public abstract class ShotBehaviour : ScriptableObject
    {
        public void HandleCollision(ShotController shooter, AliveEntityController aliveEntity)
        {
            if (!aliveEntity) return;
            
            aliveEntity.Damage(shooter.GetDamage());
            shooter.AddCollision(aliveEntity);
            DoHandleCollision(shooter, aliveEntity);
        }

        protected abstract void DoHandleCollision(ShotController shooter, AliveEntityController aliveEntity);
    }
}