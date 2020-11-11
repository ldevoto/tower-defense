using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO.Behaviour.Enemy
{
    public abstract class EnemyBehaviour : ScriptableObject
    {
        public virtual void HandleNearEnter(EnemyController enemyController, AliveEntityController aliveEntityController){}
        public virtual void HandleNearLeave(EnemyController enemyController, AliveEntityController aliveEntityController){}
        public virtual void HandleFarEnter(EnemyController enemyController, AliveEntityController aliveEntityController){}
        public virtual void HandleFarLeave(EnemyController enemyController, AliveEntityController aliveEntityController){}
    }
}