using TowerDefense.SO;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class ShooterController : MonoBehaviour
    {
        [SerializeField] private Transform shotOrigin = null;

        private ShotData _shotData = null;
        private float _shotCooldown = 0f;
        private float _lastShot = -9999999f;

        public void Shot()
        {
            if (Time.time - _lastShot < _shotCooldown) return;
            
            _lastShot = Time.time;
            ImmediateShot();
        }

        public void ImmediateShot()
        {
            var shotInstance = Instantiate(_shotData.shotPrefab, shotOrigin.position, shotOrigin.rotation);
            shotInstance.ShotWith(_shotData.damage);
        }

        public void SetCooldown(float shotCooldown)
        {
            _shotCooldown = shotCooldown;
        }

        public void SetShotData(ShotData shotData)
        {
            _shotData = shotData;
        }
    }
}