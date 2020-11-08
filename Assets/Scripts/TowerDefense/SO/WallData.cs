using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "WallData", menuName = "SO/Entity/WallData", order = 0)]
    public class WallData : ScriptableObject
    {
        public Animator animator = null;
        public float hp = 100f;
        public int cost = 50;
    }
}