using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class ModelController : MonoBehaviour
    {
        [SerializeField] private TowerData towerData = null;
        [SerializeField] private WallController towerPrefab = null;

        public int GetCost()
        {
            return towerData.cost;
        }

        public WallController GetPrefab()
        {
            return towerPrefab;
        }
    }
}