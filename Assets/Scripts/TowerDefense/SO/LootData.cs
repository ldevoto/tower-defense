using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "LootData", menuName = "SO/LootData", order = 0)]
    public class LootData : ScriptableObject
    {
        public int quantity = 10;
    }
}