using UnityEngine;

namespace TowerDefense.Controllers
{
    public class DamageDealerController : MonoBehaviour
    {
        [SerializeField] private float damageCooldown = 0f;

        private float _lastDamageTime = -9999999f;

        public void Damage(AliveEntityController aliveEntityController)
        {
            if (Time.time - _lastDamageTime < damageCooldown) return;
            
            _lastDamageTime = Time.time;
            aliveEntityController.Damage(10f);
        }
    }
}