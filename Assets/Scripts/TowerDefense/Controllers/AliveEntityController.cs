using System;
using UnityEngine;

namespace TowerDefense.Controllers
{
    public class AliveEntityController : MonoBehaviour
    {
        public Action OnHpChange = null;
        public Action OnKill = null;
        
        private float _maxHp = 0f;
        private float _currentHp = 0f;
        private bool _isKilled = false;

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

        private void Kill()
        {
            _currentHp = 0f;
            if (_isKilled) return;
            
            _isKilled = true;
            OnKill?.Invoke();
        }
    }
}