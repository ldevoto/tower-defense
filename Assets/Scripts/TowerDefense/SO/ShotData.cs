using TowerDefense.Controllers;
using UnityEngine;

namespace TowerDefense.SO
{
    [CreateAssetMenu(fileName = "ShotData", menuName = "SO/Entity/ShotData", order = 6)]
    public class ShotData : ScriptableObject
    {
        public ShotController shotPrefab;
        public float damage;
    }
}