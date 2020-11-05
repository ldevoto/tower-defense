using UnityEngine;

namespace TowerDefense.Controllers
{
    public class FollowerController : MonoBehaviour
    {
        [SerializeField] private Transform target = null;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private float smoothTime = 0.1F;
        
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            var targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }
}