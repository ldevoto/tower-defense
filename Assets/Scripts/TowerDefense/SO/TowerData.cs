using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "SO/TowerData", order = 4)]
    public class TowerData : ScriptableObject
    {
        public int cost = 50;
    }
}