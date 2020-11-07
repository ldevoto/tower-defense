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

        public float GetWaitToNextShot()
        {
            var nextShot = _shotCooldown - (Time.time - _lastShot);
            return nextShot >= 0f ? nextShot : 0f;
        }
        
        public void Shot()
        {
            if (Time.time - _lastShot < _shotCooldown) return;
            
            ImmediateShot();
        }

        public void ImmediateShot()
        {
            _lastShot = Time.time;
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