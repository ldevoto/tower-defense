using UnityEngine;

namespace TowerDefense.Controllers
{
    public class ShooterController : MonoBehaviour
    {
        [SerializeField] private GameObject shotPrefab = null;
        [SerializeField] private Transform shotOrigin = null;
        [SerializeField] private float shotCooldown = 0f;

        private float _lastShot = -9999999f;

        public void Shot()
        {
            if (Time.time - _lastShot < shotCooldown) return;
            
            _lastShot = Time.time;
            Instantiate(shotPrefab, shotOrigin.position, shotOrigin.rotation);
        }
    }
}