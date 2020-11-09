using System;
using TowerDefense.Singletons;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class AliveEntityController : MonoBehaviour
    {
        public Action<float> OnHit = null;
        public Action OnHpChange = null;
        public Action OnKill = null;
        
        private float _maxHp = 0f;
        private float _currentHp = 0f;
        private bool _isKilled = false;

        private void Awake()
        {
            OnHit += ShowDamage;
        }

        public void SetHP(float amount)
        {
            _maxHp = amount;
            _currentHp = amount;
            OnHpChange?.Invoke();
        }

        public void Damage(float amount)
        {
            if (_currentHp <= 0f) return;
            
            _currentHp -= amount;
            if (_currentHp <= 0f)
            {
                Kill();
            }
            OnHit?.Invoke(amount);
            OnHpChange?.Invoke();
        }

        public void Heal(float amount)
        {
            if (_currentHp >= _maxHp) return;
            
            _currentHp += amount;
            if (_currentHp >= _maxHp)
            {
                _currentHp = _maxHp;
            }
            OnHpChange?.Invoke();
        }

        public float GetHpPercentage()
        {
            return _currentHp / _maxHp;
        }
        
        private void ShowDamage(float damage)
        {
            var damageShower = DamageShowerProvider.instance.GetOne();
            damageShower.transform.position = transform.position;
            damageShower.Show(damage);
        }

        private void Kill()
        {
            _currentHp = 0f;
            if (_isKilled) return;
            
            _isKilled = true;
            OnKill?.Invoke();
        }
    }
}