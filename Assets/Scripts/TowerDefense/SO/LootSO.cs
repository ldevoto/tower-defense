using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "Loot", menuName = "SO/Loot", order = 0)]
    public class LootSO : ScriptableObject
    {
        public float quantity = 10f;
    }
}