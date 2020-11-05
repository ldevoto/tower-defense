using UnityEngine;

namespace TowerDefense.SO
{
    public abstract class EnemySpawnerStrategy : ScriptableObject
    {
        public abstract void Start();
        public abstract void Stop();
    }
}