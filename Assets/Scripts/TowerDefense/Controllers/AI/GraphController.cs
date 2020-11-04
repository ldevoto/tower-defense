using System.Collections;
using UnityEngine;

namespace TowerDefense.Controllers.AI
{
    public class GraphController : MonoBehaviour
    {
        public void UpdateGraph(Vector3 updatePoint)
        {
            AstarPath.active.Scan();
        }
        
        public void UpdateCompleteGraph()
        {
            StartCoroutine(UpdateGraphCoroutine());
        }

        private IEnumerator UpdateGraphCoroutine()
        {
            yield return null;
            AstarPath.active.Scan();
        }
    }
}