using UnityEngine;

namespace TowerDefense.Controllers
{
    public class FollowerController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private Vector3 offset = Vector3.zero;

        private void Update()
        {
            transform.position = target.position + offset;
        }
    }
}