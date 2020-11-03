using UnityEngine;

namespace TowerDefense.Controllers.AI
{
    public class GraphController : MonoBehaviour
    {
        private void UpdateGraph(Vector3 updatePoint)
        {
            AstarPath.active.Scan();
        }
    }
}